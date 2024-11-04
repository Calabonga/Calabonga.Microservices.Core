using System;
using System.Diagnostics;

namespace Calabonga.Microservices.Core.Extensions
{
    public static class GuidExtensions
    {
        /// <summary>
        /// Indicates whether the specified Guid is null or an empty
        /// </summary>
        /// <param name="source">Value to check</param>
        /// <returns>true if the source parameter is null or an empty Guid; otherwise, false</returns>
        [DebuggerStepThrough]
        public static bool IsEmpty(this Guid? source) => source == null || source.Value == Guid.Empty;

        /// <summary>
        /// Indicates whether the specified Guid is an empty
        /// </summary>
        /// <param name="source">Value to check</param>
        /// <returns>true if the source parameter is an empty Guid; otherwise, false</returns>
        [DebuggerStepThrough]
        public static bool IsEmpty(this Guid source) => source == Guid.Empty;
    }
}