using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CYBER_API.BLL.CustomExceptions
{
    public class NotSingleException : Exception
    {
        public NotSingleException(string? message) : base(message)
        {

        }
    }
}
