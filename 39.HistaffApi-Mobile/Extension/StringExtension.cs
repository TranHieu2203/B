using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HiStaffAPI.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// Convert date from format date
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">Default value = null</param>
        /// <param name="formatDate">Default format ""</param>
        /// <returns></returns>
        public static DateTime? ToDate(this string str, DateTime? defaultValue = null, string formatDate = "dd/MM/yyyy")
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    return defaultValue;
                }

                if (string.IsNullOrEmpty(formatDate))
                {
                    return DateTime.Parse(str.ToString());
                }
                else
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    DateTime date = DateTime.Now;
                    var kq = DateTime.TryParseExact(str.ToString(), formatDate, provider, System.Globalization.DateTimeStyles.None, out date);
                    if (kq)
                    {
                        return date;
                    }
                    else
                    {
                        return defaultValue;
                    }
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Return decimal value
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">Default value</param>
        /// <returns></returns>
        public static decimal? ToDecimal(this string str, decimal? defaultValue = null)
        {
            try
            {
                var kq = decimal.TryParse(str, out decimal returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Return integer value
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">Default value = null</param>
        /// <returns></returns>
        public static int? ToInt(this string str, int? defaultValue = null)
        {
            try
            {
                var kq = int.TryParse(str, out int returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Return double value
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">Default value = null</param>
        /// <returns></returns>
        public static double? ToDouble(this string str, double? defaultValue = null)
        {
            try
            {
                var kq = double.TryParse(str, out double returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static float? ToSingle(this string str, float? defaultValue = null)
        {
            try
            {
                var kq = float.TryParse(str, out float returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        public static long? ToLong(this string str, long? defaultValue = null)
        {
            try
            {
                var kq = long.TryParse(str, out long returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        public static short? ToShort(this string str, short? defaultValue = null)
        {
            try
            {
                var kq = short.TryParse(str, out short returnValue);
                if (kq)
                {
                    return returnValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

    }
}