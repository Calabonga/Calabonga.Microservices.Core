﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;

using Calabonga.Microservices.Core.Extensions;

namespace Calabonga.Microservices.Core
{
    /// <summary>
    /// Enum Helper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class EnumHelper<T> where T : struct
    {
        /// <summary>
        /// Returns Enum with DisplayNames
        /// </summary>
        /// <returns></returns>
        public static Dictionary<T, string> GetValuesWithDisplayNames()
        {
            var type = typeof(T);
            var r = type.GetEnumValues();
            var list = new Dictionary<T, string>();
            foreach (var element in r)
            {
                list.Add((T)element, GetDisplayValue((T)element));
            }
            return list;
        }

        /// <summary>
        /// Returns values from enum
        /// </summary>
        /// <returns></returns>
        public static IList<T> GetValues()
        {
            return typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => (T)Enum.Parse(typeof(T), fi.Name, false)).ToList();
        }

        /// <summary>
        /// Parse displayValue by string from Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Parse(string value)
        {
            var displayName = TryParseDisplayValue(value);
            if (displayName != null)
            {
                return (T)displayName;
            }
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Parse displayValue by string from Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T? TryParse(string value)
        {
            if (Enum.TryParse(value, true, out T result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Return attribute to extract data from it
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TAttribute TryGetFromAttribute<TAttribute>(string value)
            where TAttribute : Attribute
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .FirstOrDefault(x => x.Name == value)?
                .GetCustomAttribute<TAttribute>(false);
        }

        /// <summary>
        /// Return attribute to extract data from it
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TAttribute TryGetFromAttribute<TAttribute>(T value)
            where TAttribute : Attribute
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .FirstOrDefault(x => x.Name == value.ToString())?
                .GetCustomAttribute<TAttribute>(false);
        }

        /// <summary>
        /// Parse displayValue by string from Enum
        /// </summary>
        /// <param name="displayValue"></param>
        /// <returns></returns>
        public static T? TryParseDisplayValue(string displayValue)
        {
            var fieldInfos = typeof(T).GetFields();

            foreach (var field in fieldInfos)
            {
                var valuesAttributes = field.GetCustomAttributes(typeof(DisplayNamesAttribute), false) as DisplayNamesAttribute[];
                if (valuesAttributes?.Length > 0)
                {
                    if (valuesAttributes[0].Names.Any())
                    {
                        var exists = valuesAttributes[0].Names.Any(x => x.Equals(displayValue));
                        if (exists)
                        {
                            if (Enum.TryParse(field.Name, true, out T result1))
                            {
                                return result1;
                            }
                        }
                    }
                }

                var descriptionAttributes = field.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
                if (descriptionAttributes?.Length > 0)
                {
                    if (descriptionAttributes[0].ResourceType != null)
                    {
                        // Calabonga: Implement search in resources (resx) (2020-06-26 02:48 EnumHelper)
                        // var stringValue = LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);
                        return default(T);
                    }

                    if (descriptionAttributes[0].Name.Equals(displayValue, StringComparison.OrdinalIgnoreCase))
                    {
                        if (Enum.TryParse(field.Name, true, out T result1))
                        {
                            return result1;
                        }
                    }

                    if (Enum.TryParse(displayValue, true, out T result2))
                    {
                        return result2;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns values from Enum
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetNames()
        {
            return typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        /// <summary>
        /// Returns values from Enum or Resource file if exists
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetDisplayValues()
        {
            return typeof(T).HasAttribute<FlagsAttribute>() ? default(IList<string>) : GetNames().Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (var staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    var resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }

        /// <summary>
        /// Returns display name for Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), true) as DisplayAttribute[];
            if (descriptionAttributes?.Length > 0 && descriptionAttributes[0].ResourceType != null)
            {
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);
            }

            if (descriptionAttributes == null)
            {
                return string.Empty;
            }

            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        public static IEnumerable<T> GetUniqueFlags(Enum flags)
        {
            if (!flags.HasAttribute<FlagsAttribute>())
            {
                yield break;
            }

            foreach (var value in Enum.GetValues(flags.GetType()))
            {
                if (!flags.HasFlag((Enum)value))
                {
                    continue;
                }

                yield return (T)value;
            }
        }

    }

    /// <summary>
    /// EnumHelper Attribute
    /// Provides a general-purpose attribute that lets you specify localizable strings
    /// for types and members of entity partial classes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DisplayNamesAttribute : Attribute
    {
        public DisplayNamesAttribute(params string[] values)
        {
            Names = values;
        }

        public IEnumerable<string> Names { get; }
    }
}
