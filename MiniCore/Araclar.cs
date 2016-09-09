using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Data;

namespace MiniCore
{
    public class cAraclar
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<int, string> VerEnumListesi(Type enumType)
        {
            Array Values = Enum.GetValues(enumType);
            string[] names = Enum.GetNames(enumType);
            Dictionary<int, string> dic = new Dictionary<int, string>();
            for (int i = 0; i <= names.GetUpperBound(0); i++)
                dic.Add(Convert.ToInt32(Enum.Parse(enumType, names[i])), GetDescription((Enum)Enum.Parse(enumType, names[i])));
            return dic;
        }
        /// <summary>
        /// Enum değerinin description attribute değerini veren metotdur.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(Enum value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                string strvalue = string.Empty;
                if (string.IsNullOrEmpty(strvalue))
                    strvalue = (attributes.Length > 0) ? attributes[0].Description : value.ToString();
                return strvalue;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>Verilen datatable'ı JSON string olarak döndürür.</summary>
        /// <param name="dt">JSON'a dönüştürülecek DataTable.</param>
        /// <param name="allowCols">İstenilen kolonlar.Virgül ile ayrılarak yazılacak.</param>
        /// <returns>JSON String.</returns>
        public static string GetDataTableToJSon(DataTable ptdt, string allowCols)
        {
            //allowCols string ini indexof kullanabilmek için List e atıyoruz
            List<string> listCols = new List<string>();
            if (allowCols != null)
            {
                foreach (string s in allowCols.Split(','))
                {
                    listCols.Add(s);
                }
            }
            //dönüş string i
            StringBuilder JsonString = new StringBuilder();
            if (ptdt != null && ptdt.Rows.Count > 0)
            {
                JsonString.Append("{ ");
                JsonString.Append("\"Table\":[ ");
                for (int i = 0; i < ptdt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < ptdt.Columns.Count; j++)
                    {
                        if ((allowCols == null) || (listCols.IndexOf(ptdt.Columns[j].ColumnName.ToString()) > -1))
                        {
                            if (j < ptdt.Columns.Count - 1)
                                JsonString.Append("\"" + ptdt.Columns[j].ColumnName.ToString() + "\":" + "\"" + EncodeJsString(ptdt.Rows[i][j].ToString()) + "\",");

                            else if (j == ptdt.Columns.Count - 1)
                                JsonString.Append("\"" + ptdt.Columns[j].ColumnName.ToString() + "\":" + "\"" + EncodeJsString(ptdt.Rows[i][j].ToString()) + "\"");

                        }
                    }
                    if (i == ptdt.Rows.Count - 1)
                    {

                        JsonString.Append("} ");

                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }
        public static string EncodeJsString(string s)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        int i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }

            return sb.ToString();
        }

        public static string URLDuzelt(string psrtKelime)
        {
            //Bu metodumuzlada Türkçe karakterleri temizleyip ingilizceye uyarlıyoruz
            string Temp = psrtKelime;
            Temp = Temp.Replace("-", ""); Temp = Temp.Replace(" ", "-");
            Temp = Temp.Replace("ç", "c"); Temp = Temp.Replace("ğ", "g");
            Temp = Temp.Replace("ı", "i"); Temp = Temp.Replace("ö", "o");
            Temp = Temp.Replace("ş", "s"); Temp = Temp.Replace("ü", "u");
            Temp = Temp.Replace("\"", ""); Temp = Temp.Replace("/", "");
            Temp = Temp.Replace("(", ""); Temp = Temp.Replace(")", "");
            Temp = Temp.Replace("{", ""); Temp = Temp.Replace("}", "");
            Temp = Temp.Replace("%", ""); Temp = Temp.Replace("&", "");
            Temp = Temp.Replace("+", ""); Temp = Temp.Replace(",", "");
            Temp = Temp.Replace("?", ""); Temp = Temp.Replace(".", "_");
            Temp = Temp.Replace("ı", "i");
            return Temp;
        }
    }
}
