using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenIso8583Net.Formatter;

namespace OpenIso8583Net.Tests.TestMessages
{
    public class BCDIsoMsg : Iso8583
    {
        #region Constants and Fields

        /// <summary>
        ///   The template.
        /// </summary>
        private static readonly Template MsgTemplate;

        #endregion

        #region Constructors and Destructor

        /// <summary>
        ///   Initializes static members of the <see cref="IsoMsgBinaryMsgTypeFormatter" /> class.
        /// </summary>
        static BCDIsoMsg()
        {
            // Get the default template for the Iso8583 class
            MsgTemplate = GetDefaultIso8583Template();

            MsgTemplate.MsgTypeFormatter = new BinaryFormatter();
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="IsoMsgBinaryMsgTypeFormatter" /> class.
        /// </summary>
        public BCDIsoMsg()
            : base(MsgTemplate)
        {
        }

        #endregion
    }
}