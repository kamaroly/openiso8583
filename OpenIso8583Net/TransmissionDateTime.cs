using System;

namespace OpenIso8583Net
{
    /// <summary>
    ///   Class representing field 7 in an ISO 8583 Message
    /// </summary>
    public class TransmissionDateTime
    {
        private readonly AMessage _message;

        /// <summary>
        ///   Create a new instance of the TransmissionDateTime class
        /// </summary>
        /// <param name = "message">Iso Message to link back to</param>
        public TransmissionDateTime(AMessage message)
        {
            _message = message;
        }

        /// <summary>
        ///   Auto generate the transmission date time to be now.
        /// </summary>
        public void SetNow()
        {
            _message[Iso8583.Bit._007_TRAN_DATE_TIME] = DateTime.Now.ToUniversalTime().ToString("MMddHHmmss");
        }
    }
}