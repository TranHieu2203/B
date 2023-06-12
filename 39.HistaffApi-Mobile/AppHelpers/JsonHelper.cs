using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace HiStaffAPI.AppHelpers
{
    public static class JsonHelper
    {
        /// <summary>
        /// Convert  to XML string
        /// </summary>
        /// <param name="Record"></param>
        /// <param name="RecordType"></param>
        /// <returns></returns>
        public static string XML(this object Record, Type RecordType)
        {
            using (var sw = new StringWriter())
            {
                using (XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true }))
                {
                    var x = new XmlSerializer(RecordType);
                    x.Serialize(xw, Record);
                    return sw.ToString();
                }
            }
        }

        /// <summary>
        /// Convert DataTable to JSON string
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string JSON(this DataTable dt)
        {
            try
            {
                if (dt == null)
                    return "";
                var l = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dt.Rows)
                {
                    var dic = new Dictionary<string, object>();
                    foreach (DataColumn dc in dt.Columns)
                        dic.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                    l.Add(dic);
                }

                var js = new JavaScriptSerializer();
                return js.Serialize(l);
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog(ex);
            }

            return "";
        }

        /// <summary>
        /// Convert Array datarow to JSON string
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string JSON(this DataRow[] dt)
        {
            try
            {
                if (dt == null)
                    return "";
                var l = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dt)
                {
                    var dic = new Dictionary<string, object>();
                    foreach (DataColumn dc in dr.Table.Columns)
                        dic.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                    l.Add(dic);
                }

                var js = new JavaScriptSerializer();
                return js.Serialize(l);
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog(ex);
            }

            return "";
        }

        public static string JSON(this DataRow dt)
        {
            try
            {
                if (dt == null)
                    return "";
                var l = new List<Dictionary<string, object>>();
                var dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Table.Columns)
                    dic.Add(dc.ColumnName, dt[dc.ColumnName].ToString());
                l.Add(dic);
                var js = new JavaScriptSerializer();
                return js.Serialize(l);
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog(ex);
            }

            return "";
        }

        public static string JSON(this List<DataRow> dt)
        {
            try
            {
                if (dt == null)
                    return "";
                var l = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dt)
                {
                    var dic = new Dictionary<string, object>();
                    foreach (DataColumn dc in dr.Table.Columns)
                        dic.Add(dc.ColumnName, dr[dc.ColumnName].ToString());
                    l.Add(dic);
                }

                var js = new JavaScriptSerializer();
                return js.Serialize(l);
            }
            catch (Exception ex)
            {
                LogHelper.WriteExceptionToLog(ex);
            }

            return "";
        }
    }
}