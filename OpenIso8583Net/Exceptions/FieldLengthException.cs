using System;

namespace OpenIso8583Net.Exceptions
{
    /// <summary>
    ///   Exception detailing a field length
    /// </summary>
    public class FieldLengthException : ApplicationException
    {
        /// <summary>
        ///   Create a new instance of the FieldLengthException class
        /// </summary>
        /// <param name = "fieldNumber">Field number that is incorrect</param>
        /// <param name = "message">Message to apply to the exception</param>
        public FieldLengthException(int fieldNumber, string message)
            : base("Field Number : " + fieldNumber + Environment.NewLine + message)
        {
            FieldNumber = fieldNumber;
        }

        /// <summary>
        ///   Field number that this exception applies to
        /// </summary>
        public int FieldNumber { get; private set; }
    }
}