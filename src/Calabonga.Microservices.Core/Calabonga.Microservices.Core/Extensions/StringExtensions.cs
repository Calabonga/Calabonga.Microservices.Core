using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calabonga.Microservices.Core.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex WebUrlExpression = new Regex(@"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+(:[0-9]+)?|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex EmailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex StripHtmlExpression = new Regex("<\\S[^><]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        [DebuggerStepThrough]
        public static Guid ToGuid(this string value)
        {
            Guid.TryParse(value, out var result);
            return result;
        }

        [DebuggerStepThrough]
        public static bool IsWebUrl(this string target)
        {
            return !string.IsNullOrEmpty(target) && WebUrlExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static bool IsEmail(this string target)
        {
            return !string.IsNullOrEmpty(target) && EmailExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static string NullSafe(this string target)
        {
            return (target ?? string.Empty).Trim();
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string target, params object[] args)
        {
            if (target == null) { return string.Empty; }
            return string.Format(CultureInfo.CurrentCulture, target, args);
        }

        [DebuggerStepThrough]
        public static string StripHtml(this string target)
        {
            return StripHtmlExpression.Replace(target, string.Empty);
        }

        [DebuggerStepThrough]
        public static T ToEnum<T>(this string target, T defaultValue) where T : IComparable, IFormattable
        {
            T convertedValue = defaultValue;

            if (string.IsNullOrEmpty(target)) return convertedValue;
            try
            {
                convertedValue = (T)Enum.Parse(typeof(T), target.Trim(), true);
            }
            catch (ArgumentException)
            {
            }

            return convertedValue;
        }

        [DebuggerStepThrough]
        public static bool HasAttribute<T>(this object @this, bool inherit = false) where T : Attribute
        {
            return GetAttributes<T>(@this, inherit).Any();
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> GetAttributes<T>(this object @this, bool inherit = false) where T : Attribute
        {
            return @this.GetType().GetCustomAttributes(typeof(T), inherit).Cast<T>();
        }

        /// <summary>
        /// Indicates whether the specified string is null or an empty string ("")
        /// </summary>
        /// <param name="source">Value to check</param>
        /// <returns>
        /// true if the source parameter is null or an empty string (""); otherwise, false
        /// </returns>
        [DebuggerStepThrough]
        public static bool IsEmpty(this string source) => string.IsNullOrEmpty(source);

        /// <summary>
        /// Checks whether <paramref name="enumerable"/> is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="enumerable"/>.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/> to be checked.</param>
        /// <returns>True if <paramref name="enumerable"/> is null or empty, false otherwise.</returns>
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        /// Indicates whether the specified string is null or an empty string ("")
        /// </summary>
        /// <param name="source">Value to check</param>
        /// <returns>
        /// true if the source parameter is null or an empty string (""); otherwise, false
        /// </returns>
        [DebuggerStepThrough]
        public static bool IsNotNullOrEmpty(this string source) => !IsNullOrEmpty(source);

        /// <summary>
        /// Indicates whether the specified strings is equal (case insensitive)
        /// </summary>
        /// <param name="source">Value to check</param>
        /// <param name="target">Target value</param>
        /// <returns>
        /// true if the both parameters is empty (null or an empty string ("")) or equal (case insensitive);
        /// otherwise, false
        /// </returns>
        [DebuggerStepThrough]
        public static bool SameAs(this string source, string target) =>
            string.IsNullOrEmpty(source) && string.IsNullOrEmpty(target) ||
            string.Equals(source, target, StringComparison.OrdinalIgnoreCase);
    }
}
