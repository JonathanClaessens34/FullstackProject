using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Domain
{
    public static class Contracts
    {
        /// <summary>
        /// Checks (asserts) a certain condition. If the condition is not met a <see cref="ContractException"/> is thrown.
        /// </summary>
        /// <param name="precondition">A condition that must be true.</param>
        /// <param name="message">The message that is used in the <see cref="ContractException"/> when the condition is not met.</param>
        /// <exception cref="ContractException">Thrown when the <paramref name="precondition"/> is not met.</exception>
        [DebuggerStepThrough]
        public static void Require(bool precondition, string message = "")
        {
            if (!precondition)
            {
                throw new ContractException(message);
            }
        }
    }
}
