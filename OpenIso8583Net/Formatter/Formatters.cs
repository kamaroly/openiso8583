namespace OpenIso8583Net.Formatter
{
    /// <summary>
    ///   Convenience class containing formatters
    /// </summary>
    public static class Formatters
    {
        /// <summary>
        ///   Get an ASCII Formatter
        /// </summary>
        public static IFormatter Ascii
        {
            get { return new AsciiFormatter(); }
        }

        /// <summary>
        ///   Get a BCD formatter
        /// </summary>
        public static IFormatter Bcd
        {
            get { return new BcdFormatter(); }
        }

        /// <summary>
        ///   Get a Binary Formatter
        /// </summary>
        public static IFormatter Binary
        {
            get { return new BinaryFormatter(); }
        }
    }
}