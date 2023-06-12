using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppException
{
    public class UnauthorizeException : Exception
    {
        public const string DefaultMessage = "Unauthorized";
        public UnauthorizeException() : base(DefaultMessage)
        {

        }

        public UnauthorizeException(string message) : base(message)
        {

        }

    }
}