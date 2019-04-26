using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work23
{
    // 1) declare enumeration CurrencyTypes, values UAH, USD, EU
    enum CurrencyTypes
    {
        UAH,
        USD,
        EU,
    }

    class Money
    {
        // 2) declare 2 properties Amount, CurrencyType
        public decimal Amount { get; set; }
        public CurrencyTypes CurrencyTypes { get; set; }
        private static int _countCurrencyTypes = Enum.GetNames(typeof(CurrencyTypes)).Length;
        private static decimal[,] _cource = new decimal[_countCurrencyTypes, _countCurrencyTypes];
        public static decimal Cource(CurrencyTypes currencyTypes1, CurrencyTypes currencyTypes2)
        {
            return Math.Round(_cource[(int)currencyTypes1, (int)currencyTypes2], 4);
        }
        // 3) declare parameter constructor for properties initialization
        static Money()
        {
            for (int i = 0; i <= _countCurrencyTypes - 1; i++)
            {
                for (int j = 0; j <= _countCurrencyTypes - 1; j++)
                    _cource[i, j] = 1.0M;
            }
        }
        public Money()
        {
            Amount = 0m;
            CurrencyTypes = CurrencyTypes.UAH;
        }
        public Money(CurrencyTypes currencyTypes, decimal amount)
        {
            Amount = amount;
            CurrencyTypes = currencyTypes;
        }
        public static void InputCource(CurrencyTypes currencyTypes, decimal cource)
        {
            if (currencyTypes != CurrencyTypes.UAH)
            {
                _cource[0, (int)currencyTypes] = cource;
                _cource[(int)currencyTypes, 0] = 1 / cource;
                //right
                for (int i = (int)currencyTypes + 1; i < _countCurrencyTypes; i++)
                {
                    _cource[(int)currencyTypes, i] = _cource[0, (int)currencyTypes] / _cource[0, i];
                }
                //left
                for (int i = (int)currencyTypes - 1; i > 0; i--)
                {
                    _cource[(int)currencyTypes, i] = _cource[0, i] / _cource[0, (int)currencyTypes];
                }
                //down
                for (int i = 1; i < _countCurrencyTypes; i++)
                {
                    _cource[i, (int)currencyTypes] = _cource[0, (int)currencyTypes] / _cource[0, i];
                }
            }
        }
        // 4) declare overloading of operator + to add 2 objects of Money
        public static Money operator +(Money amount1, Money amount2)
        {
            return new Money
            {
                CurrencyTypes = amount1.CurrencyTypes,
                Amount = amount1.Amount + amount2.Amount * _cource[(int)amount1.CurrencyTypes, (int)amount2.CurrencyTypes]
            };
        }
        public static Money operator +(Money amount, double d)
        {
            return new Money
            {
                CurrencyTypes = amount.CurrencyTypes,
                Amount = amount.Amount + (decimal)d
            };
        }
        public static Money operator +(double d, Money amount)
        {
            return new Money
            {
                CurrencyTypes = amount.CurrencyTypes,
                Amount = amount.Amount + (decimal)d
            };
        }
        // 5) declare overloading of operator -- to decrease object of Money by 1
        public static Money operator --(Money amount)
        {
            return new Money
            {
                CurrencyTypes = amount.CurrencyTypes,
                Amount = amount.Amount-1
            };
        }
        // 7) declare overloading of operator > and < to compare 2 objects of Money
        public static bool operator >(Money amount1, Money amount2)
        {
            return (amount1.Amount > amount2.Amount * _cource[(int)amount1.CurrencyTypes, (int)amount2.CurrencyTypes]);
        }
        public static bool operator <(Money amount1, Money amount2)
        {
            return (amount1.Amount < amount2.Amount * _cource[(int)amount1.CurrencyTypes, (int)amount2.CurrencyTypes]);
        }
        public static bool operator ==(Money amount1, Money amount2)
        {
            return (amount1.Amount == amount2.Amount * _cource[(int)amount1.CurrencyTypes, (int)amount2.CurrencyTypes]);
        }
        public static bool operator !=(Money amount1, Money amount2)
        {
            return (amount1.Amount != amount2.Amount * _cource[(int)amount1.CurrencyTypes, (int)amount2.CurrencyTypes]);
        }
        // 8) declare overloading of operator true and false to check CurrencyType of object
        public static bool operator &(Money amount, CurrencyTypes currencyTypes)
        {
            return (string)amount == currencyTypes.ToString();
        }
        // 9) declare overloading of implicit/explicit conversion  to convert Money to double, string and vice versa
        public static explicit operator double (Money amount)
        {
            return (double)amount.Amount;
        }
        public static explicit operator string (Money amount)
        {
            return amount.CurrencyTypes.ToString();
        }
        public static explicit operator Money(double amount)
        {
            return new Money { CurrencyTypes = CurrencyTypes.UAH, Amount = (decimal)amount };
        }
        public static explicit operator Money(string currencyTypes)
        {     
            return new Money { CurrencyTypes = (CurrencyTypes)Enum.Parse(typeof(CurrencyTypes), currencyTypes), Amount = 0 };
        }

        public override string ToString()
        {
            return $"{Math.Round(this.Amount, 4)}({this.CurrencyTypes})";
        }
    }
}
