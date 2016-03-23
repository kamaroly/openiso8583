using System;

namespace OpenIso8583Net
{
    /// <summary>
    ///   Deals with the Processing Code (Field 3) in an ISO message
    /// </summary>
    public class ProcessingCode
    {
        /// <summary>
        ///   Create a new processing code using the given data
        /// </summary>
        /// <param name = "data">The value to unpack</param>
        public ProcessingCode(String data)
        {
            if (data.Length != 6)
                throw new ArgumentException("Incorrect length for data", "data");
            TranType = data.Substring(0, 2);
            FromAccountType = data.Substring(2, 2);
            ToAccountType = data.Substring(4, 2);
        }

        /// <summary>
        ///   Gets the transaction type
        /// </summary>
        public string TranType { get; set; }

        /// <summary>
        ///   Gets the from account type
        /// </summary>
        public string FromAccountType { get; set; }

        /// <summary>
        ///   Gets the to account type
        /// </summary>
        public string ToAccountType { get; set; }
    }
}