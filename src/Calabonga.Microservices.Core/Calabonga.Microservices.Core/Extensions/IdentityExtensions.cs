using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;

namespace Calabonga.Microservices.Core.Extensions
{
    /// <summary>
    /// Entity Validator Extensions
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Gets the subject identifier.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <exception cref="System.InvalidOperationException">sub claim is missing</exception>
        [DebuggerStepThrough]
        public static string GetSubjectId(this IIdentity identity)
        {
            var id = identity as ClaimsIdentity;
            var claim = id?.FindFirst("sub");

            if (claim == null)
            {
                throw new InvalidOperationException("sub claim is missing");
            }

            return claim.Value;
        }
    }
}