using System;
using System.Text;
using System.Security.Cryptography;
using HiStaffAPI.AppCommon.PortalModel;

namespace HiStaffAPI.AppHelpers
{

    public class TokenHelper
    {
        //public static Dictionary<string, TokenDTO> OnlineToken { get; set; }
        private static readonly string saltValue = "w8yIhE";
        //private static readonly int TokenParam = 3; // Count Param for token
        private static readonly int TokenTimeLive = 3; // 1 Minute
        private static readonly int TokenTimeAPI = 43200; // 1 Minute

        private static string GenerateMd5Hash(params object[] args)
        {
            var x = new MD5CryptoServiceProvider();
            string input = saltValue + string.Join(".", args);
            var computeHash = Encoding.UTF8.GetBytes(input.ToLower());
            computeHash = x.ComputeHash(computeHash);
            var stringBuilder = new StringBuilder();
            foreach (byte var in computeHash)
                stringBuilder.Append(var.ToString("x2").ToLower());
            return stringBuilder.ToString();
        }

        private static TokenApiDTO CreateToken(params object[] args)
        {
            if ((args.Length > 9))
                return new TokenApiDTO()
                {
                    SessionID = args[0].ToString(),
                    TimeCreated = long.Parse(args[1].ToString()),
                    Username = args[2].ToString(),
                    EMPLOYEE_ID = decimal.Parse(args[3].ToString()),
                    EMPLOYEE_CODE = args[4].ToString(),
                    TimeExpired = long.Parse(args[5].ToString()),
                    FunctionName = args[6].ToString(),
                    ActionName = args[7].ToString(),
                    IsNotSameFunctionName = bool.Parse(args[8].ToString()),
                    DeviceID = args[9].ToString()
                };
            else
                return new TokenApiDTO()
                {
                    SessionID = args[0].ToString(),
                    TimeCreated = long.Parse(args[1].ToString()),
                    Username = args[2].ToString(),
                    EMPLOYEE_ID = decimal.Parse(args[3].ToString()),
                    EMPLOYEE_CODE = args[4].ToString(),
                    TimeExpired = long.Parse(args[5].ToString()),
                    FunctionName = args[6].ToString(),
                    ActionName = args[7].ToString(),
                    IsNotSameFunctionName = bool.Parse(args[8].ToString())
                };
        }

        public static TokenApiDTO GetToken(params object[] args)
        {
            string SesstionID = args[0].ToString(); // // Sesstion ID .No = 0
            string Username = args[1].ToString(); // // Username .No = 1
            decimal Employee_ID = (decimal)args[2]; // // Employee_ID .No = 2
            string Employee_Code = args[3].ToString(); // // Employee_Code .No = 3
            string functionName = args[4].ToString(); // // Function .No = 4
            string actionName = args[5].ToString(); // // Action .No = 5
            bool IsCheckFunction = false;
            string DeviceID = "WEBPC";
            if ((args.Length > 6))
                IsCheckFunction = bool.Parse(args[6].ToString());
            if ((args.Length > 7))
                DeviceID = args[7].ToString();
            // // Time Expired
            string timeExpired;
            if ((DeviceID == "WEBPC"))
                timeExpired = DateTime.Now.AddMinutes(TokenTimeLive).ToString("yyyyMMddHHmmss");
            else
                timeExpired = DateTime.Now.AddMinutes(TokenTimeAPI).ToString("yyyyMMddHHmmss");

            long timeNow = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));

            try
            {
                return CreateToken(SesstionID, timeNow, Username, Employee_ID, Employee_Code, timeExpired, functionName, actionName, IsCheckFunction, DeviceID);
            }
            catch //(Exception ex)
            {
                return CreateToken(SesstionID, timeNow, Username, Employee_ID, Employee_Code, timeExpired, functionName, actionName, IsCheckFunction, DeviceID);
            }
        }

        public static TokenApiDTO GenerateToken(params object[] args)
        {
            string SesstionID = args[0].ToString(); // // Sesstion ID .No = 0
            string Username = args[1].ToString(); // // Username .No = 1
            decimal Employee_ID = (decimal)args[2]; // // Employee_ID .No = 2
            string Employee_Code = args[3].ToString(); // // Employee_Code .No = 3
            string functionName = args[4].ToString(); // // Function .No = 4
            string actionName = args[5].ToString(); // // Action .No = 5
            bool IsCheckFunction = false;
            if ((args.Length > 6))
                IsCheckFunction = bool.Parse(args[6].ToString());
            string DeviceID = "WEBPC";
            if ((args.Length > 7))
                DeviceID = args[7].ToString();

            TokenApiDTO _token = GetToken(SesstionID, Username, Employee_ID, Employee_Code, functionName, actionName, IsCheckFunction, DeviceID);
            if ((_token.Token == null))
            {
                _token.Token = GenerateMd5Hash(SesstionID, _token.TimeCreated, Username, Employee_ID, Employee_Code, _token.TimeExpired, functionName, actionName, IsCheckFunction.ToString(), DeviceID);
            }

            return _token;
        }

        /// <summary>
        /// Check lại token cho portal
        /// </summary>
        /// <param name="_token"></param>
        /// <param name="sFunctionName"></param>
        /// <returns></returns>
        public static bool CheckToken(TokenApiDTO _token, string sFunctionName)
        {
            try
            {
                string Token = GenerateMd5Hash(_token.SessionID, _token.TimeCreated.ToString(), _token.Username, _token.EMPLOYEE_ID.Value.ToString("#"), _token.EMPLOYEE_CODE, _token.TimeExpired.ToString(), _token.FunctionName, _token.ActionName, _token.IsNotSameFunctionName.ToString(), _token.DeviceID);
                if ((Token != _token.Token))
                    return false;

                long timeNow = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                // // Time is false
                if ((_token.TimeExpired < timeNow))
                {
                    return false;
                }

                //Check lại các tham số
                
                return true;
            }
            catch //(Exception ex)
            {
                return false;
            }
        }
    }

}
