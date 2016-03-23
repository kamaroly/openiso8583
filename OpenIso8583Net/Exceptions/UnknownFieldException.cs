using System;

namespace OpenIso8583Net.Exceptions
{
    /// <summary>
    ///   This exception is thrown when the field that is being created is unknown
    /// </summary>
    public class UnknownFieldException : ApplicationException
    {
        /// <summary>
        ///   Create a new instance of the UnknownFieldException class
        /// </summary>
        /// <param name = "fieldNumber">Field number that was created</param>
        public UnknownFieldException(string fieldNumber)
            : base("Field " + fieldNumber + " is unknown")
        {
            FieldNumber = fieldNumber;
        }

        /// <summary>
        ///   Field number that was attemted to be created
        /// </summary>
        public string FieldNumber { get; set; }
    }
}