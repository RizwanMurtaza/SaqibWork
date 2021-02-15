using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serials.Core
{
    [Serializable]
    public static class ExtensionMethods
    {
        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Converts normal dateTime to Unix timestamp in milli-seconds
        /// </summary>
        /// <param name="dateTime">Normal datetime</param>
        /// <returns>Unix timestamp in milli-seconds</returns>
        public static long ToUnixTime(this DateTime dateTime)
        {
            //if (!dateTime.HasValue)
            //    return null;

            var timeSpan = (dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds * 1000;
        }

        /// <summary>
        /// Converts milli-seconds to readable DateTime value
        /// </summary>
        /// <param name="unixTimeStamp">Milli-seconds as long number</param>
        /// <returns>Readable datetime</returns>
        public static DateTime? UnixTimeStampToDateTime(this long? unixTimeStamp)
        {
            if (!unixTimeStamp.HasValue)
                return null;

            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp.Value / 1000).ToLocalTime();
            return dtDateTime;
        }
        public static bool TryConvert<T>(int value, out T result)
        {
            result = default(T);
            bool success = Enum.IsDefined(typeof(T), value);
            if (success)
            {
                result = (T)Enum.ToObject(typeof(T), value);
            }
            return success;
        }
    }
}
