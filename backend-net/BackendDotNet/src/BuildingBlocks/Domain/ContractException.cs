using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Thrown when a certain contract is not met.
    /// Usually thrown by <see cref="Contracts"/> class.
    /// </summary>
    [Serializable]
    public class ContractException : Exception
    {
        public ContractException()
        {
        }

        public ContractException(string message)
            : base(message)
        {
        }

        public ContractException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ContractException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
