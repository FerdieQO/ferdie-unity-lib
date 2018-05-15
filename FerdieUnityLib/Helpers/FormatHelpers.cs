using System;
using System.Collections.Generic;
using System.Text;

namespace FerdieUnityLib.Helpers
{
    public static class FormatHelpers
    {
        public static string GetCurrentCurrencyFormat(string currencySign)
        {
            return currencySign + "{0:n0}";
        }

        public static string FormatCash(string currencySign, float amount, bool includeSign = false, bool onlyIncludeMinusSign = false)
        {
            string result = string.Format(GetCurrentCurrencyFormat(currencySign), Math.Abs(amount));
            if (includeSign)
            {
                if (amount >= 0 && !onlyIncludeMinusSign)
                {
                    result = result.Insert(0, "+");
                }
                else if (amount < 0)
                {
                    result = result.Insert(0, "-");
                }
            }
            return result;
        }

        public static string FormatPercentage(float percentage)
        {
            return string.Format("{0:n0}%", Math.Abs(percentage));
        }
    }
}
