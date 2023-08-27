using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Exceptions;

public class BackendAuthenticationException : Exception
{
    public string Content { get; }

    public BackendAuthenticationException()
    {
    }

    public BackendAuthenticationException(string content)
    {
        Content = content;
    }
}