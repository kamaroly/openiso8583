namespace OpenIso8583Net.FieldValidator
{
    /// <summary>
    ///   Static class returning the various field validators
    /// </summary>
    public static class FieldValidators
    {
        /// <summary>
        ///   Get an Alpha validator
        /// </summary>
        public static IFieldValidator A
        {
            get { return Alpha; }
        }

        /// <summary>
        ///   Get an Alpha validator
        /// </summary>
        public static IFieldValidator Alpha
        {
            get { return new AlphaFieldValidator(); }
        }

        /// <summary>
        ///   Get an AlphaNumeric field validator
        /// </summary>
        public static IFieldValidator An
        {
            get { return AlphaNumeric; }
        }

        /// <summary>
        ///   Get an AlphaNumericFieldValidator
        /// </summary>
        public static IFieldValidator AlphaNumeric
        {
            get { return new AlphaNumericFieldValidator(); }
        }

        /// <summary>
        ///   Get an AlphaNumericAndSpace field validator
        /// </summary>
        public static IFieldValidator Ansp
        {
            get { return AlphaNumericAndSpace; }
        }

        /// <summary>
        ///   Get an AlphaNumericAndSpaceFieldValidator
        /// </summary>
        public static IFieldValidator AlphaNumericAndSpace
        {
            get { return new AlphaNumericAndSpaceFieldValidator(); }
        }

        /// <summary>
        ///   Get an Alphanumeric and printable chars validator
        /// </summary>
        public static IFieldValidator Anp
        {
            get { return AlphaNumericPrintable; }
        }

        /// <summary>
        ///   Get an Alphanumeric and printable chars validator
        /// </summary>
        public static IFieldValidator AlphaNumericPrintable
        {
            get { return new AlphaNumericPrintableFieldValidator(); }
        }

        /// <summary>
        ///   Get an Alphanumeric and special chars validator
        /// </summary>
        public static IFieldValidator Ans
        {
            get { return AlphaNumericSpecial; }
        }

        /// <summary>
        ///   Get an Alphanumeric and special chars validator
        /// </summary>
        public static IFieldValidator AlphaNumericSpecial
        {
            get { return new AlphaNumericSpecialFieldValidator(); }
        }

        /// <summary>
        ///   Get an Alpha or Numeric validator
        /// </summary>
        public static IFieldValidator AorN
        {
            get { return AlphaOrNumeric; }
        }

        /// <summary>
        ///   Get an Alpha or Numeric validator
        /// </summary>
        public static IFieldValidator AlphaOrNumeric
        {
            get { return new AlphaOrNumericFieldValidator(); }
        }

        /// <summary>
        ///   Gets a validator for hex data
        /// </summary>
        public static IFieldValidator Hex
        {
            get { return new HexFieldValidator(); }
        }

        /// <summary>
        ///   Gets a validator that allows anything
        /// </summary>
        public static IFieldValidator None
        {
            get { return new NoneFieldValidator(); }
        }

        /// <summary>
        ///   Get a Numeric field validator
        /// </summary>
        public static IFieldValidator N
        {
            get { return Numeric; }
        }

        /// <summary>
        ///   Get a Numeric field validator
        /// </summary>
        public static IFieldValidator Numeric
        {
            get { return new NumericFieldValidator(); }
        }

        /// <summary>
        ///   Get a field validator to validate track 2 data
        /// </summary>
        public static IFieldValidator Track2
        {
            get { return new Track2FieldValidator(); }
        }

        /// <summary>
        ///   Get a validator for ISO8583:87 amount format (x+n8)
        /// </summary>
        /// <remarks>
        ///   For example x+n8 valid would be C00000000 or D00000000
        /// </remarks>
        public static IFieldValidator Rev87AmountValidator
        {
            get { return new Rev87AmountFieldValidator(); }
        }
    }
}