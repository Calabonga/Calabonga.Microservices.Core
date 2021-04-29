using System.Diagnostics;

namespace Calabonga.Microservices.Core.Extensions
{
    /// <summary>
    /// Extensions for integer
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// Indicates whether the specified int is null or an zero
        /// </summary>
        /// <param name="source">Value to check</param>
        /// <returns>
        /// true if the value parameter is null or an zero; otherwise, false
        /// </returns>
        [DebuggerStepThrough]
        public static bool IsEmpty(this int? source) => source == default || source == 0;
    }
}