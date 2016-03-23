using System;

namespace OpenIso8583Net
{
    /// <summary>
    ///   This class represents an ISO8583 Additional Amount
    /// </summary>
    public class AdditionalAmount
    {
        private string _amount;

        /// <summary>
        ///   Creates a new AdditionalAmount with null values
        /// </summary>
        public AdditionalAmount()
        {
        }

        /// <summary>
        ///   Constructs an AdditionalAmount object from a string
        /// </summary>
        /// <param name = "value"></param>
        public AdditionalAmount(String value)
        {
            if (value.Length != 20)
                throw new ArgumentException("value incorrect length", "value");

            AccountType = value.Substring(0, 2);
            AmountType = value.Substring(2, 2);
            CurrencyCode = value.Substring(4, 3);
            Sign = value.Substring(7, 1);
            Amount = value.Substring(8);
        }

        /// <summary>
        ///   Gets or sets the Account Type
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        ///   Gets or sets the Amount Type
        /// </summary>
        public string AmountType { get; set; }

        /// <summary>
        ///   Gets or sets the Currency Code
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        ///   Gets or sets the Sign of the amount. C for credit/positive, D for debit/negative
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        ///   Gets or sets the amount
        /// </summary>
        public string Amount
        {
            get { return _amount; }
            set { _amount = value.PadLeft(12, '0'); }
        }

        /// <summary>
        ///   Gets or sets the value of the amount as a long
        /// </summary>
        public long Value
        {
            get
            {
                if (Sign == null)
                    return 0;
                if (Amount == null)
                    return 0;
                var amt = long.Parse(Amount);
                if (Sign == "D")
                    return -amt;
                return amt;
            }
            set
            {
                Sign = value < 0 ? "D" : "C";
                var amt = value < 0 ? -value : value;
                Amount = amt.ToString();
            }
        }

        /// <summary>
        ///   Gets a string representation of the additional amount
        /// </summary>
        /// <returns>A string representation of the additional amount</returns>
        public override string ToString()
        {
            return AccountType + AmountType + CurrencyCode + Sign + Amount;
        }
    }
}