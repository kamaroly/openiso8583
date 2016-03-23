using System.Collections.Generic;
using System.Text;

namespace OpenIso8583Net
{
    /// <summary>
    ///   The HashtableMessage class allows a Hashtable containing String key and 
    ///   String value pairs to be serialised and deserialised so that a Hashtable 
    ///   may be included in an ISO message
    /// </summary>
    public class HashtableMessage : Dictionary<string, string>
    {
        /// <summary>
        ///   Initialiases the object from a string value
        /// </summary>
        /// <param name = "message">The string containing the data</param>
        public void FromMessageString(string message)
        {
            var offset = 0;

            if (message == null)
                return;

            while (offset < message.Length)
            {
                var keyLengthIndicator = int.Parse(message.Substring(offset, 1));
                offset++;
                var keyLength = int.Parse(message.Substring(offset, keyLengthIndicator));
                offset += keyLengthIndicator;
                var key = message.Substring(offset, keyLength);
                offset += keyLength;

                var valueLengthIndicator = int.Parse(message.Substring(offset, 1));
                offset++;
                var valueLength = int.Parse(message.Substring(offset, valueLengthIndicator));
                offset += valueLengthIndicator;
                var value = message.Substring(offset, valueLength);
                offset += valueLength;

                Add(key, value);
            }
        }

        /// <summary>
        ///   Serialises the hashtable message
        /// </summary>
        /// <returns>Serialised form of the hashtable message</returns>
        public string ToMessageString()
        {
            var sb = new StringBuilder();
            foreach (var key in Keys)
            {
                var value = this[key];
                if (string.IsNullOrEmpty(value))
                    continue;
                var keyLength = key.Length;
                var keyLengthIndicator = keyLength.ToString().Length;
                sb.Append(keyLengthIndicator);
                sb.Append(keyLength);
                sb.Append(key);


                var valueLength = value.Length;
                var valueLengthIndicator = valueLength.ToString().Length;
                sb.Append(valueLengthIndicator);
                sb.Append(valueLength);
                sb.Append(value);
            }
            return sb.ToString();
        }
    }
}