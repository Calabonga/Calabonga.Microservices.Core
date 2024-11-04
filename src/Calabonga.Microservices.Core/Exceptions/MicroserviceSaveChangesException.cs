using System;

namespace Calabonga.Microservices.Core.Exceptions
{
    /// <summary>
    /// Represent ArgumentNull Exception
    /// </summary>
    [Serializable]
    public class MicroserviceSaveChangesException : Exception
    {
        public MicroserviceSaveChangesException() : base(AppContracts.Exceptions.ArgumentNullException)
        {

        }

        public MicroserviceSaveChangesException(string message) : base(message)
        {

        }

        public MicroserviceSaveChangesException(string message, Exception exception) : base(message, exception)
        {

        }

        public MicroserviceSaveChangesException(Exception exception) : base(AppContracts.Exceptions.ArgumentNullException, exception)
        {

        }
    }
}
