using System;

namespace ArvitumNew.Util
{
    public sealed class MyMath
    {
        public static decimal Round(decimal x)
        {
            decimal integerPart = Math.Truncate(x);
            decimal fractionalPart = x - Math.Truncate(x);

            if (fractionalPart < 0.25M)
                return integerPart;
            else if (fractionalPart >= 0.25M && fractionalPart < 0.625M)
                return integerPart + 0.5M;
            else if (fractionalPart >= 0.625M && fractionalPart < 0.875M)
                return integerPart + 0.75M;
            else if (fractionalPart >= 0.875M)
                return integerPart + 1;
            else
                return x;
        }
    }
}
