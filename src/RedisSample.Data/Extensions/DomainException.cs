using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSample.DataDomain.Extensions
{
    public class DomainException: Exception
    {
        public DomainException() { }
        public DomainException(string message): base(message) { }
    }
}
