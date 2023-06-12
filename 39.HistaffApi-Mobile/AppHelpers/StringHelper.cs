using System;
using System.Text;

namespace HiStaffAPI.AppHelpers
{
    public static class StringHelper
    {
        public static string Base64Encode(string txt)
        {
            byte[] data;
            data = Encoding.UTF8.GetBytes(txt);
            return Convert.ToBase64String(data);
        }

        public static string Base64Decode(string txtBase64Encode)
        {
            byte[] data;
            data = Convert.FromBase64String(txtBase64Encode);
            return Encoding.UTF8.GetString(data);
        }
        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        public static string ToString(this string str)
        {
            return str == null ? "" : str;
        }

        public static string ToString(this decimal? str, string defaultNull = "")
        {
            if (str.HasValue) return str.ToString();
            return defaultNull;
        }
        public static string ToString(this int? str, string defaultNull = "")
        {
            if (str.HasValue) return str.ToString();
            return defaultNull;
        }
    }
}
