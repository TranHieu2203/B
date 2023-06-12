using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace HiStaffAPI.Extension
{
    public static class ObjectExtension
    {
        /// <summary>
        /// return decimal value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public static decimal? ToDecimal(this object obj, decimal? defaultValue = null)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                else
                {
                    var returnValue = Convert.ToDecimal(obj);
                    return returnValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// return decimal value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public static long? ToLong(this object obj, long? defaultValue = null)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                else
                {
                    var returnValue = Convert.ToInt64(obj);
                    return returnValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// return int value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public static int? ToInt(this object obj, int? defaultValue = null)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                else
                {
                    var returnValue = Convert.ToInt32(obj);
                    return returnValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Convert date from format date
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">Default value = null</param>
        /// <param name="formatDate">Default format = ""</param>
        /// <returns></returns>
        public static DateTime? ToDate(this object str, DateTime? defaultValue = null, string formatDate = "")
        {
            try
            {
                if (formatDate == "")
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
        /// return decimal value
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public static Guid? ToGuid(this object obj, Guid? defaultValue = null)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                else
                {
                    Guid returnValue = Guid.Empty;
                    var kq = Guid.TryParse(obj.ToString(), out returnValue);
                    if (kq)
                    {
                        return returnValue;
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
    }
}