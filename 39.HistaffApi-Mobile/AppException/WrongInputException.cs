using System;

namespace HiStaffAPI.AppException
{
    public class WrongInputException : Exception
    {
        public const string DefaultMessage = "WrongInputData";
        public WrongInputException() : base(DefaultMessage)
        {

        }

        public WrongInputException(string message) : base(message)
        {

        }

    }
}