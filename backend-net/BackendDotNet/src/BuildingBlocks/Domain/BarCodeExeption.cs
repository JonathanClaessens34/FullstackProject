using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BarCodeException : Exception
    {

        public BarCodeException()
        {
        }

        public BarCodeException(string message)
            : base(message)
        {
        }
    }
}
