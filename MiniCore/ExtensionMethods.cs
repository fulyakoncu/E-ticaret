using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Data;

namespace MiniCore
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Gönderilen Object Value'u Decimal'e çevirir.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object str)
        {
            return Convert.ToDecimal(str);
        }
        /// <summary>
        /// Eğer geri dönen değer null ise ve siz null olması durumunda bir değer döndürmesini istiyorsanız bu metodu kullanabilirsiniz.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <param name="returnValue">Null olduğunda geri döndüreceğiniz değer.</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object str, decimal returnValue)
        {
            if (str.IsNull())
                return returnValue;
            else
                return Convert.ToDecimal(str);
        }
        /// <summary>
        /// Gönderilen Object value'yu nullable değer olarak decimal'e convert eder. Bu değer null ise geri decimal? tipinde null döner.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <returns></returns>
        public static decimal? ToNDecimal(this object str)
        {
            if (str.IsNull()) return null;
            else return Convert.ToDecimal(str);
        }
        /// <summary>
        /// Nesneyi Double'a Convert eder.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <returns></returns>
        public static double ToDouble(this object str)
        {
            return Convert.ToDouble(str);
        }
        /// <summary>
        /// Nesneyi Double'a Convert eder.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <param name="returnValue">Null olduğunda geri döndüreceğiniz değer.</param>
        /// <returns></returns>
        public static double ToDouble(this object str, double returnValue)
        {
            if (str.IsNull())
                return returnValue;
            else
                return Convert.ToDouble(str);
        }
        /// <summary>
        /// Nesneyi Nullable olacak şekilde Double'a Convert eder. Dönen değer double?'dır.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <returns></returns>
        public static double? ToNDouble(this object str)
        {
            if (str.IsNull()) return null;
            else return Convert.ToDouble(str);
        }
        /// <summary>
        /// Nesneyi DateTime'e çevirir.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object str)
        {
            return Convert.ToDateTime(str.ToString());
        }
        /// <summary>
        /// Nesneyi TimeSpan'a çevirir.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this object str)
        {
            return TimeSpan.Parse(str.ToString());
        }
        /// <summary>
        /// Nesneyi Int64(long)'e Convert eder.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <returns></returns>
        public static long ToInt64(this object str)
        {
            return Convert.ToInt64(str);
        }
        /// <summary>
        /// Nesneyi Long(Int64)'a Convert eder.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToLong(this object str)
        {
            return str.ToInt64();
        }
        /// <summary>
        /// Nesneyi long(Int64)'a Convert eder.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <param name="returnValue">Null olduğunda geri döndüreceğiniz değer.</param>
        /// <returns></returns>
        public static long ToLong(this object str, short returnValue)
        {
            if (str.IsNull())
                return returnValue;
            else
                return Convert.ToInt64(str);
        }
        /// <summary>
        /// Nesneyi Int'e Convert eder.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this object str)
        {
            return Convert.ToInt32(str);
        }
        /// <summary>
        /// Nesneyi Int'e Convert eder.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <param name="returnValue">Null olduğunda geri döndüreceğiniz değer.</param>
        /// <returns></returns>
        public static int ToInt(this object str, int returnValue)
        {
            if (str.IsNull())
                return returnValue;
            else
                return Convert.ToInt32(str);
        }
        /// <summary>
        /// Nesneyi Nullable olacak şekilde int'e Convert eder. Dönen değer int?'dır.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <returns></returns>
        public static int? ToNInt(this object str)
        {
            if (str.IsNull()) return null;
            else return Convert.ToInt32(str);
        }
        /// <summary>
        /// Nesneyi değeri Short'a Convert eder.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static short ToShort(this object str)
        {
            return Convert.ToInt16(str);
        }
        /// <summary>
        /// Nesneyi short'a Convert eder.
        /// </summary>
        /// <param name="str">Convert edilecek değer.</param>
        /// <param name="returnValue">Null olduğunda geri döndüreceğiniz değer.</param>
        /// <returns></returns>
        public static short ToShort(this object str, short returnValue)
        {
            if (str.IsNull())
                return returnValue;
            else
                return Convert.ToInt16(str);
        }
        /// <summary>
        /// Nesne null ise true değilse false döner. Gönderilen obje DataSet veya DataTable ise ve Row'ları 0 ise yine true döner. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            if (obj != null && obj.GetType() == typeof(DataSet))
                return ((DataSet)obj).Tables[0].Rows.Count == 0;
            else if (obj != null && obj.GetType() == typeof(DataTable))
                return ((DataTable)obj).Rows.Count == 0;
            else if (obj == DBNull.Value)
                return true;
            else if (obj is string && string.IsNullOrEmpty((string)obj))
                return true;
            else
                return obj == null;
        }
        /// <summary>
        /// Eğer obje null ise parantez içinde gönderdiğiniz değeri size geri döner.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object IfIsNull(this object obj, object value)
        {
            if (obj.IsNull())
                return value;
            else
                return obj;
        }
        /// <summary>
        /// Gönderilen değer objeye eşitse 1 değilse 0, obje null ise -1 döner.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short IfIsValue(this object obj, object value)
        {
            if (obj.IsNull())
                return -1;
            else if (obj.Equals(value))
                return 1;
            else
                return 0;
        }
        /// <summary>
        /// DropDownList'in SelectedValue'sini Int tipinde geri döner.
        /// </summary>
        /// <param name="ddl">DropDownList</param>
        /// <returns></returns>
        //public static int ToInt(this DropDownList ddl)
        //{
        //    return int.Parse(ddl.SelectedValue);
        //}
        /// <summary>
        /// DropDownList'in SelectedValue'sini Short tipinde geri döner.
        /// </summary>
        /// <param name="ddl">DropDownList</param>
        /// <returns></returns>
        //public static short ToShort(this DropDownList ddl)
        //{
        //    return short.Parse(ddl.SelectedValue);
        //}
        /// <summary>
        /// String değer MD5 e dönüştürülür.
        /// </summary>
        /// <param name="metin"></param>
        /// <returns></returns>
        public static string ToMD5(this String text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text));
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2").ToLower());
            }

            return builder.ToString();
        }
    }
}
