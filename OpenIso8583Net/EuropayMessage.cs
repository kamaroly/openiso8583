namespace OpenIso8583Net
{
        using FD = FieldDescriptor;

        /// <summary>
        /// Europay Message Type (ISO 8583 Revision 87)
        /// </summary>
        public class EuropayMessage : Iso8583
        {
            private static readonly Template DefaultTemplate;

            static EuropayMessage()
            {
                DefaultTemplate =
                    new Template
                {
                    {Fld.F002PrimaryAccountNumber,              FD.PanMask(FD.AsciiLlNumeric(19))},
                    {Fld.F003ProcessingCode,                    FD.AsciiNumeric(6)},
                    {Fld.F004TransactionAmount,                 FD.AsciiNumeric(12)},
                    {Fld.F005SettlementAmount,                  FD.AsciiNumeric(12)},
                    {Fld.F006CardholderBillingAmount,           FD.AsciiNumeric(12)},
                    {Fld.F007TransmissionDateAndTime,           FD.AsciiNumeric(10)},
                    {Fld.F008CardholderBillingFeeAmount,        FD.AsciiNumeric(8)},
                    {Fld.F009SettlementConversionRate,          FD.AsciiNumeric(8)},
                    {Fld.F010CardholderBillingConversionRate,   FD.AsciiNumeric(8)},
                    {Fld.F011SystemTraceAuditNumber,            FD.AsciiNumeric(6)},
                    {Fld.F012LocalTransactionTime,              FD.AsciiNumeric(6)},
                    {Fld.F013LocalTransactionDate,              FD.AsciiNumeric(4)},
                    {Fld.F014ExpirationDate,                    FD.AsciiNumeric(4)},
                    {Fld.F015SettlementDate,                    FD.AsciiNumeric(4)},
                    {Fld.F016ConversionDate,                    FD.AsciiNumeric(4)},
                    {Fld.F017CaptureDate,                       FD.AsciiNumeric(4)},
                    {Fld.F018MerchantsType,                     FD.AsciiNumeric(4)},
                    {Fld.F019AcquiringInstitutionCountryCode,   FD.AsciiNumeric(3)},
                    {Fld.F020PanExtendedCountryCode,            FD.AsciiNumeric(3)},
                    {Fld.F021ForwardingInstitutionCountryCode,  FD.AsciiNumeric(3)},
                    {Fld.F022PointOfServiceEntryMode,           FD.AsciiNumeric(3)},
                    {Fld.F023CardSequenceNumber,                FD.AsciiNumeric(3)},
                    {Fld.F024NetworkInternationalId,            FD.AsciiNumeric(3)},
                    {Fld.F025PointOfServiceConditionCode,       FD.AsciiNumeric(2)},
                    {Fld.F026PointOfServicePinCaptureCode,      FD.AsciiNumeric(2)},
                    {Fld.F027AuthorizationIdResponseLength,     FD.AsciiNumeric(1)},
                    {Fld.F028TransactionFeeAmount,              FD.AsciiAmount(9)},
                    {Fld.F029SettlementFeeAmount,               FD.AsciiAmount(9)},
                    {Fld.F030TransactionProcessingFeeAmount,    FD.AsciiAmount(9)},
                    {Fld.F031SettlementProcessingFeeAmount,     FD.AsciiAmount(9)},
                    {Fld.F032AcquiringInstitutionIdCode,        FD.AsciiLlNumeric(11)},
                    {Fld.F033ForwardingInstitutionIdCode,       FD.AsciiLlNumeric(11)},
                    {Fld.F034PanExtended,                       FD.AsciiLlCharacter(28)},
                    {Fld.F035Track2Data,                        FD.AsciiLlNumeric(37)},
                    {Fld.F036Track3Data,                        FD.AsciiLllCharacter(104)},
                    {Fld.F037RetrievalReferenceNumber,          FD.AsciiAlphaNumeric(12)},
                    {Fld.F038AuthorizationIdResponse,           FD.AsciiAlphaNumeric(6)},
                    {Fld.F039ResponseCode,                      FD.AsciiAlphaNumeric(2)},
                    {Fld.F040ServiceRestrictionCode,            FD.AsciiAlphaNumeric(3)},
                    {Fld.F041CardAcceptorTerminalId,            FD.AsciiAlphaNumeric(8)},
                    {Fld.F042CardAcceptorIdCode,                FD.AsciiAlphaNumeric(15)},
                    {Fld.F043CardAcceptorNameLocation,          FD.AsciiAlphaNumeric(40)},
                    {Fld.F044AdditionalResponseData,            FD.AsciiLlCharacter(25)},
                    {Fld.F045Track1Data,                        FD.AsciiLlCharacter(76)},
                    {Fld.F046AdditionalDataIso,                 FD.AsciiLllCharacter(999)},
                    {Fld.F047AdditionalDataNational,            FD.AsciiLllCharacter(999)},
                    {Fld.F048EuropayField48,                    FD.AsciiLllBinary(999)},
                    {Fld.F049TransactionCurrencyCode,           FD.AsciiAlphaNumeric(3)},
                    {Fld.F050SettlementCurrencyCode,            FD.AsciiAlphaNumeric(3)},
                    {Fld.F051CardholderBillingCurrencyCode,     FD.AsciiAlphaNumeric(3)},
                    {Fld.F052PinData,                           FD.BinaryFixed(8)},
                    {Fld.F053SecurityRelatedControlInformation, FD.AsciiNumeric(16)},
                    {Fld.F054AdditionalAmounts,                 FD.AsciiLllCharacter(120)},
                    {Fld.F055ReservedIso,                       FD.AsciiLllCharacter(999)},
                    {Fld.F056ReservedIso,                       FD.AsciiLllCharacter(999)},
                    {Fld.F057ReservedNational,                  FD.AsciiLllCharacter(999)},
                    {Fld.F058ReservedNational,                  FD.AsciiLllCharacter(999)},
                    {Fld.F059ReservedNational,                  FD.AsciiLllCharacter(999)},
                    {Fld.F060ReservedPrivate,                   FD.AsciiLllCharacter(999)},
                    {Fld.F061ReservedPrivate,                   FD.AsciiLllCharacter(999)},
                    {Fld.F062ReservedPrivate,                   FD.AsciiLllCharacter(999)},
                    {Fld.F063ReservedPrivate,                   FD.AsciiLllCharacter(999)},
                    {Fld.F064MessageAuthenticationCodeField,    FD.BinaryFixed(8)},
                    {Fld.F065ExtendedBitmap,                    FD.BinaryFixed(8)},
                    {Fld.F066SettlementCode,                    FD.AsciiNumeric(1)},
                    {Fld.F067ExtendedPaymentCode,               FD.AsciiNumeric(2)},
                    {Fld.F068ReceivingInstitutionCountryCode,   FD.AsciiNumeric(3)},
                    {Fld.F069SettlementInstitutionCountryCode,  FD.AsciiNumeric(3)},
                    {Fld.F070NetworkManagementInformationCode,  FD.AsciiNumeric(3)},
                    {Fld.F071MessageNumber,                     FD.AsciiNumeric(4)},
                    {Fld.F072MessageNumberLast,                 FD.AsciiNumeric(4)},
                    {Fld.F073DateAction,                        FD.AsciiNumeric(6)},
                    {Fld.F074CreditsNumber,                     FD.AsciiNumeric(10)},
                    {Fld.F075CreditsReversalNumber,             FD.AsciiNumeric(10)},
                    {Fld.F076DebitsNumber,                      FD.AsciiNumeric(10)},
                    {Fld.F077DebitsReversalNumber,              FD.AsciiNumeric(10)},
                    {Fld.F078TransferNumber,                    FD.AsciiNumeric(10)},
                    {Fld.F079TransferReversalNumber,            FD.AsciiNumeric(10)},
                    {Fld.F080InquiriesNumber,                   FD.AsciiNumeric(10)},
                    {Fld.F081AuthorizationNumber,               FD.AsciiNumeric(10)},
                    {Fld.F082ProcessingFeeAmountCredits,        FD.AsciiNumeric(12)},
                    {Fld.F083TransactionFeeAmountCredits,       FD.AsciiNumeric(12)},
                    {Fld.F084ProcessingFeeAmountDebits,         FD.AsciiNumeric(12)},
                    {Fld.F085TransactionFeeAmountDebits,        FD.AsciiNumeric(12)},
                    {Fld.F086AmountCredits,                     FD.AsciiNumeric(16)},
                    {Fld.F087ReversalAmountCredits,             FD.AsciiNumeric(16)},
                    {Fld.F088AmountDebits,                      FD.AsciiNumeric(16)},
                    {Fld.F089ReversalAmountDebits,              FD.AsciiNumeric(16)},
                    {Fld.F090OriginalDataElements,              FD.AsciiNumeric(42)},
                    {Fld.F091FileUpdateCode,                    FD.AsciiAlphaNumeric(1)},
                    {Fld.F092FileSecurityCode,                  FD.AsciiAlphaNumeric(2)},
                    {Fld.F093ResponseIndicator,                 FD.AsciiAlphaNumeric(5)},
                    {Fld.F094ServiceIndicator,                  FD.AsciiAlphaNumeric(7)},
                    {Fld.F095ReplacementAmounts,                FD.AsciiAlphaNumeric(42)},
                    {Fld.F096MessageSecurityCode,               FD.BinaryFixed(8)},
                    {Fld.F097NetSettlementAmount,               FD.AsciiAmount(17)},
                    {Fld.F098Payee,                             FD.AsciiAlphaNumeric(25)},
                    {Fld.F099SettlementInstitutionIdCode,       FD.AsciiLlNumeric(11)},
                    {Fld.F100ReceivingInstitutionIdCode,        FD.AsciiLlNumeric(11)},
                    {Fld.F101FileName,                          FD.AsciiLlCharacter(17)},
                    {Fld.F102AccountId1,                        FD.AsciiLlCharacter(28)},
                    {Fld.F103AccountId2,                        FD.AsciiLlCharacter(28)},
                    {Fld.F104TransactionDescription,            FD.AsciiLllCharacter(100)},
                    {Fld.F105ReservedIsoUse,                    FD.AsciiLllCharacter(999)},
                    {Fld.F106ReservedIsoUse,                    FD.AsciiLllCharacter(999)},
                    {Fld.F107ReservedIsoUse,                    FD.AsciiLllCharacter(999)},
                    {Fld.F108ReservedIsoUse,                    FD.AsciiLllCharacter(999)},
                    {Fld.F109ReservedIsoUse,                    FD.AsciiLllCharacter(999)},
                    {Fld.F110ReservedIsoUse,                    FD.AsciiLllCharacter(999)},
                    {Fld.F111ReservedIsoUse,                    FD.AsciiLllCharacter(999)},
                    {Fld.F112ReservedNationalUse,               FD.AsciiLllCharacter(999)},
                    {Fld.F113ReservedNationalUse,               FD.AsciiLllCharacter(999)},
                    {Fld.F114ReservedNationalUse,               FD.AsciiLllCharacter(999)},
                    {Fld.F115ReservedNationalUse,               FD.AsciiLllCharacter(999)},
                    {Fld.F116ReservedNationalUse,               FD.AsciiLllCharacter(999)},
                    {Fld.F117ReservedNationalUse,               FD.AsciiLllCharacter(999)},
                    {Fld.F118ReservedNationalUse,               FD.AsciiLllCharacter(999)},
                    {Fld.F119ReservedNationalUse,               FD.AsciiLllCharacter(999)},
                    {Fld.F120ReservedPrivateUse,                FD.AsciiLllCharacter(999)},
                    {Fld.F121ReservedPrivateUse,                FD.AsciiLllCharacter(999)},
                    {Fld.F122ReservedPrivateUse,                FD.AsciiLllCharacter(999)},
                    {Fld.F123ReservedPrivateUse,                FD.AsciiLllCharacter(999)},
                    {Fld.F124ReservedPrivateUse,                FD.AsciiLllCharacter(999)},
                    {Fld.F125ReservedPrivateUse,                FD.AsciiLllCharacter(999)},
                    {Fld.F126ReservedPrivateUse,                FD.AsciiLllCharacter(999)},
                    {Fld.F127ReservedPrivateUse,                FD.AsciiLllCharacter(999)},
                    {Fld.F128Mac2,                              FD.AsciiLllCharacter(999)},
                };
            }

            /// <summary>
            /// Creates an instance of the EuropayMessage class
            /// </summary>
            public EuropayMessage()
                : base(DefaultTemplate)
            {
            }

            /// <summary>
            ///   Unpacks the message from a byte array
            /// </summary>
            /// <param name = "msg">message data to unpack</param>
            /// <returns>the offset in the array representing the start of the next message</returns>
            public int Unpack(byte[] msg)
            {
                return base.Unpack(msg, 0);
            }

            /// <summary>
            /// IntelliSense friendly message type constants
            /// ; Hex notation like 0x0100 may also be used instead of these constants
            /// </summary>
            public static class Type
            {
                // ReSharper disable CSharpWarnings::CS1591
                public const int M0000InvalidMessage = MsgType._0000_INVALID_MSG;
                public const int M0100AuthRequest = 0x0100;
                public const int M0110AuthResponse = 0x0110;
                public const int M0120AuthAdvice = 0x0120;
                public const int M0130AuthAdviceResponse = 0x0130;
                public const int M0200TransactionRequest = 0x0200;
                public const int M0201TransactionRequestRepeat = 0x0201;
                public const int M0202TransactionCompletion = 0x0202;
                public const int M0203TransactionCompletionRepeat = 0x0203;
                public const int M0210TransactionResponse = 0x0210;
                public const int M0212TransactionCompletionResponse = 0x0212;
                public const int M0220TransactionAdvice = 0x0220;
                public const int M0221TransactionAdviceRepeat = 0x0221;
                public const int M0230TransactionAdviceResponse = 0x0230;
                public const int M0300AcquirerFileUpdateRequest = 0x0300;
                public const int M0310AcquirerFileUpdateResponse = 0x0310;
                public const int M0320AcquirerFileUpdateAdvice = 0x0320;
                public const int M0322IssuerFileUpdateAdvice = 0x0322;
                public const int M0330AcquirerFileUpdateAdviceResponse = 0x0330;
                public const int M0332IssuerFileUpdateAdviceResponse = 0x0332;
                public const int M0400AcquirerReversalRequest = 0x0400;
                public const int M0410AcquirerReversalRequestResponse = 0x0410;
                public const int M0420AcquirerReversalAdvice = 0x0420;
                public const int M0421AcquirerReversalAdviceRepeat = 0x0421;
                public const int M0430AcquirerReversalAdviceResponse = 0x0430;
                public const int M0500AcquirerReconRequest = 0x0500;
                public const int M0510AcquirerReconRequestResponse = 0x0510;
                public const int M0520AcquirerReconAdvice = 0x0520;
                public const int M0521AcquirerReconAdviceRepeat = 0x0521;
                public const int M0530AcquirerReconAdviceResponse = 0x0530;
                public const int M0600AdministrativeRequest = 0x0600;
                public const int M0601AdministrativeRequest = 0x0601;
                public const int M0610AdministrativeRequestResponse = 0x0610;
                public const int M0800NetworkManagementRequest = 0x0800;
                public const int M0801NetworkManagementRequestRepeat = 0x0801;
                public const int M0810NetworkManagementResponse = 0x0810;
                // ReSharper restore CSharpWarnings::CS1591

                /// <summary>
                ///   Gets the response message type for the given message type. E.g. 0220 -> 0230, 0421 -> 0430
                /// </summary>
                /// <param name = "messageType">Request Message Type</param>
                /// <returns>Response Message Type</returns>
                public static int GetResponse(int messageType)
                {
                    return MsgType.GetResponse(messageType);
                }
            }

            /// <summary>
            /// IntelliSense friendly field name/number constants
            /// ; Plain integer values may also be used instead of these constants
            /// </summary>
            public static class Fld
            {
                // ReSharper disable CSharpWarnings::CS1591
                public const int F002PrimaryAccountNumber = 2;
                public const int F003ProcessingCode = 3;
                public const int F004TransactionAmount = 4;
                public const int F005SettlementAmount = 5;
                public const int F006CardholderBillingAmount = 6;
                public const int F007TransmissionDateAndTime = 7;
                public const int F008CardholderBillingFeeAmount = 8;
                public const int F009SettlementConversionRate = 9;
                public const int F010CardholderBillingConversionRate = 10;
                public const int F011SystemTraceAuditNumber = 11;
                public const int F012LocalTransactionTime = 12;
                public const int F013LocalTransactionDate = 13;
                public const int F014ExpirationDate = 14;
                public const int F015SettlementDate = 15;
                public const int F016ConversionDate = 16;
                public const int F017CaptureDate = 17;
                public const int F018MerchantsType = 18;
                public const int F019AcquiringInstitutionCountryCode = 19;
                public const int F020PanExtendedCountryCode = 20;
                public const int F021ForwardingInstitutionCountryCode = 21;
                public const int F022PointOfServiceEntryMode = 22;
                public const int F023CardSequenceNumber = 23;
                public const int F024NetworkInternationalId = 24;
                public const int F025PointOfServiceConditionCode = 25;
                public const int F026PointOfServicePinCaptureCode = 26;
                public const int F027AuthorizationIdResponseLength = 27;
                public const int F028TransactionFeeAmount = 28;
                public const int F029SettlementFeeAmount = 29;
                public const int F030TransactionProcessingFeeAmount = 30;
                public const int F031SettlementProcessingFeeAmount = 31;
                public const int F032AcquiringInstitutionIdCode = 32;
                public const int F033ForwardingInstitutionIdCode = 33;
                public const int F034PanExtended = 34;
                public const int F035Track2Data = 35;
                public const int F036Track3Data = 36;
                public const int F037RetrievalReferenceNumber = 37;
                public const int F038AuthorizationIdResponse = 38;
                public const int F039ResponseCode = 39;
                public const int F040ServiceRestrictionCode = 40;
                public const int F041CardAcceptorTerminalId = 41;
                public const int F042CardAcceptorIdCode = 42;
                public const int F043CardAcceptorNameLocation = 43;
                public const int F044AdditionalResponseData = 44;
                public const int F045Track1Data = 45;
                public const int F046AdditionalDataIso = 46;
                public const int F047AdditionalDataNational = 47;
                public const int F048EuropayField48 = 48;
                public const int F049TransactionCurrencyCode = 49;
                public const int F050SettlementCurrencyCode = 50;
                public const int F051CardholderBillingCurrencyCode = 51;
                public const int F052PinData = 52;
                public const int F053SecurityRelatedControlInformation = 53;
                public const int F054AdditionalAmounts = 54;
                public const int F055ReservedIso = 55;
                public const int F056ReservedIso = 56;
                public const int F057ReservedNational = 57;
                public const int F058ReservedNational = 58;
                public const int F059ReservedNational = 59;
                public const int F060ReservedPrivate = 60;
                public const int F061ReservedPrivate = 61;
                public const int F062ReservedPrivate = 62;
                public const int F063ReservedPrivate = 63;
                public const int F064MessageAuthenticationCodeField = 64;
                public const int F065ExtendedBitmap = 65;
                public const int F066SettlementCode = 66;
                public const int F067ExtendedPaymentCode = 67;
                public const int F068ReceivingInstitutionCountryCode = 68;
                public const int F069SettlementInstitutionCountryCode = 69;
                public const int F070NetworkManagementInformationCode = 70;
                public const int F071MessageNumber = 71;
                public const int F072MessageNumberLast = 72;
                public const int F073DateAction = 73;
                public const int F074CreditsNumber = 74;
                public const int F075CreditsReversalNumber = 75;
                public const int F076DebitsNumber = 76;
                public const int F077DebitsReversalNumber = 77;
                public const int F078TransferNumber = 78;
                public const int F079TransferReversalNumber = 79;
                public const int F080InquiriesNumber = 80;
                public const int F081AuthorizationNumber = 81;
                public const int F082ProcessingFeeAmountCredits = 82;
                public const int F083TransactionFeeAmountCredits = 83;
                public const int F084ProcessingFeeAmountDebits = 84;
                public const int F085TransactionFeeAmountDebits = 85;
                public const int F086AmountCredits = 86;
                public const int F087ReversalAmountCredits = 87;
                public const int F088AmountDebits = 88;
                public const int F089ReversalAmountDebits = 89;
                public const int F090OriginalDataElements = 90;
                public const int F091FileUpdateCode = 91;
                public const int F092FileSecurityCode = 92;
                public const int F093ResponseIndicator = 93;
                public const int F094ServiceIndicator = 94;
                public const int F095ReplacementAmounts = 95;
                public const int F096MessageSecurityCode = 96;
                public const int F097NetSettlementAmount = 97;
                public const int F098Payee = 98;
                public const int F099SettlementInstitutionIdCode = 99;
                public const int F100ReceivingInstitutionIdCode = 100;
                public const int F101FileName = 101;
                public const int F102AccountId1 = 102;
                public const int F103AccountId2 = 103;
                public const int F104TransactionDescription = 104;
                public const int F105ReservedIsoUse = 105;
                public const int F106ReservedIsoUse = 106;
                public const int F107ReservedIsoUse = 107;
                public const int F108ReservedIsoUse = 108;
                public const int F109ReservedIsoUse = 109;
                public const int F110ReservedIsoUse = 110;
                public const int F111ReservedIsoUse = 111;
                public const int F112ReservedNationalUse = 112;
                public const int F113ReservedNationalUse = 113;
                public const int F114ReservedNationalUse = 114;
                public const int F115ReservedNationalUse = 115;
                public const int F116ReservedNationalUse = 116;
                public const int F117ReservedNationalUse = 117;
                public const int F118ReservedNationalUse = 118;
                public const int F119ReservedNationalUse = 119;
                public const int F120ReservedPrivateUse = 120;
                public const int F121ReservedPrivateUse = 121;
                public const int F122ReservedPrivateUse = 122;
                public const int F123ReservedPrivateUse = 123;
                public const int F124ReservedPrivateUse = 124;
                public const int F125ReservedPrivateUse = 125;
                public const int F126ReservedPrivateUse = 126;
                public const int F127ReservedPrivateUse = 127;
                public const int F128Mac2 = 128;
                // ReSharper restore CSharpWarnings::CS1591
            }
    }
}