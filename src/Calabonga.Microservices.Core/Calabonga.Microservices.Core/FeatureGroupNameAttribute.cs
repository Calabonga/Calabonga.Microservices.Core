using System;

namespace Calabonga.Microservices.Core
{
    /// <summary>
    /// Swagger controller group attribute for vertical slice architecture implementations
    /// </summary>
    ///
    [AttributeUsage(AttributeTargets.Method)]
    public class FeatureGroupNameAttribute : Attribute
    {
        /// <inheritdoc />
        public FeatureGroupNameAttribute(string groupName) => GroupName = groupName;

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName { get; }
    }
}
