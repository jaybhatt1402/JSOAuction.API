using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSOAuction.Utility.Exceptions
{
    [Serializable]
    public class BadResultException : Exception
    {

        public BadResultException() { }

        public BadResultException(string message) : base(message)
        {
        }

        public BadResultException(string message, Exception inner) : base(message, inner)
        {
        }



    }
}
