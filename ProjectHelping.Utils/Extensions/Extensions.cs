using System.Text;
using System.Security.Cryptography;
using System.Globalization;

namespace ProjectHelping.Utils.Extensions
{

    public static class Extensions
    {
        public static string MD5Sifrele(string sifrelenecekMetin)
        {

            // MD5CryptoServiceProvider sınıfının bir örneğini oluşturduk.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //Parametre olarak gelen veriyi byte dizisine dönüştürdük.
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);
            //dizinin hash'ini hesaplattık.
            dizi = md5.ComputeHash(dizi);
            //Hashlenmiş verileri depolamak için StringBuilder nesnesi oluşturduk.
            StringBuilder sb = new StringBuilder();
            //Her byte'i dizi içerisinden alarak string türüne dönüştürdük.

            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            //hexadecimal(onaltılık) stringi geri döndürdük.
            return sb.ToString();
        }

        /// <summary>
        /// verilen tarih formatını yyyMMdd string formatında dönüş yapar
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToStringYyyyMMdd(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }

        /// <summary>
        /// verilen tarih formatını HHmmss string formatında dönüş yapar
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToStringHhmmss(this DateTime dateTime)
        {
            return dateTime.ToString("HHmmss");
        }

        /// <summary>
        /// verilen tarih formatını hhmmssff string formatında dönüş yapar (SAATDAKIKASANIYESALISE)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToStringHhmmssff(this DateTime dateTime)
        {
            return dateTime.ToString("HHmmssFF");
        }

        /// <summary>
        /// verilen tarih formatını hhmmssff string formatında dönüş yapar (SAATDAKIKASANIYESALISE)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToStringHhmmssfff(this DateTime dateTime)
        {
            return dateTime.ToString("HHmmssFFF").PadRight(9, '0');
        }
        /// <summary>
        /// verilen tarih formatını yyyMMdd string formatında dönüş yapar
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FromStringYyyyMMdd(this string dateTime)
        {
            DateTime dateValue = DateTime.MinValue;

            if (dateTime != null && dateTime.Length != 8)
                return DateTime.MinValue;

            if (DateTime.TryParseExact(dateTime, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                return dateValue;
            return DateTime.MinValue;
        }

        /// <summary>
        /// verilen tarih formatını yyyMMdd string formatında dönüş yapar
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FromStringHhmmss(this string dateTime)
        {
            if (dateTime != null && dateTime.Length != 6)
                return DateTime.MinValue;

            DateTime dateValue;
            if (DateTime.TryParseExact(dateTime, "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                return dateValue;

            return DateTime.MinValue;
        }
        /// <summary>
        /// verilen tarih formatını yyyMMdd string formatında dönüş yapar
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime FromStringHhmm(this string dateTime)
        {
            if (dateTime != null && dateTime.Length != 6)
                return DateTime.MinValue;

            DateTime dateValue;
            if (DateTime.TryParseExact(dateTime, "HHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                return dateValue;

            return DateTime.MinValue;
        }

    }
}

