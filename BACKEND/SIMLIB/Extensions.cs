using System;
using System.Globalization;

namespace PROPOSTA
{
    static class Extensions
    {
        /// <summary>
        /// Get substring of specified number of characters on the right
        /// </summary>
        public static string Right(this string value, int length)
        {
            return value.Substring(value.Length - length);
        }
        /// <summary>
        /// Parse to datetime 
        /// </summary>
        public static string Left(this string value, int length)
        {
            return value.Substring(0, length);
        }



        public static Int32 ConvertToInt32(this string value)
        {
            Int32 dtOut;
            if (Int32.TryParse(value, out dtOut))
            {
                return dtOut;
            }
            else
            {
                return 0;
            }
        }
        public static DateTime ConvertToDatetime(this string value)
        {
            DateTime dtOut;
            if (DateTime.TryParse(value, out dtOut))
            {
                return dtOut;
            }
            else
            {
                return new DateTime();
            }
        }
        public static Double ConvertToDouble(this string value)
        {
            Double dtOut;
            if (Double.TryParse(value, out dtOut))
            {
                return dtOut;
            }
            else
            {
                return 0;
            }
        }
        public static Byte ConvertToByte(this string value)
        {
            Byte dtOut;
            if (Byte.TryParse(value, out dtOut))
            {
                return dtOut;
            }
            else
            {
                return 0;
            }
        }
        public static Boolean ConvertToBoolean(this string value)
        {
            Boolean dtOut;
            if (Boolean.TryParse(value, out dtOut))
            {
                return dtOut;
            }
            else
            {
                return false;
            }
        }
        public static string ConvertToMoney(this string value)
        {
            Double dtOut;
            if (double.TryParse(value, out dtOut))
            {
                return dtOut.ToString("C", CultureInfo.CurrentCulture);
                
            }
            else
            {
                return String.Empty;
            }
        }
        public static string ConvertToPercent(this string value)
        {
            Double dtOut;
            if (double.TryParse(value, out dtOut))
            {
                return (string.Format("{0: 0.0000}", dtOut));

            }
            else
            {
                return String.Empty;
            }
        }
    }

}