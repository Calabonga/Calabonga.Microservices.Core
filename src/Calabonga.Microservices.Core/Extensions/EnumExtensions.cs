using System;
using System.Collections;
using System.Diagnostics;
using Calabonga.Microservices.Core.Exceptions;

namespace Calabonga.Microservices.Core.Extensions
{
    /// <summary>
    /// Enum extensions
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Indicates whether the specified enum value is equal to one of the list.
        /// </summary>
        /// <param name="source">Value to check.</param>
        /// <param name="targets">Target values.</param>
        /// <typeparam name="T">Enum.</typeparam>
        /// <returns><c>true</c> if <paramref name="targets"/> contains <paramref name="source"/>;
        /// otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentException"><paramref name="targets"/> has no elements.</exception>
        [DebuggerStepThrough]
        public static bool In<T>(this T source, params T[] targets) where T : Enum
        {
            if (targets.Length < 1)
            {
                throw new MicroserviceArgumentNullException($"At least one target value is required {nameof(targets)}");
            }

            return ((IList) targets).Contains(source);
        }
    }
}