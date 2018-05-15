using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace FerdieUnityLib.Extensions
{
    /// <summary>
    /// Extensions for the String type
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Parses a Vector2 from the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.Nullable&lt;Vector2&gt;.</returns>
        public static Vector2? ParseVector2(this String str)
        {
            Vector2? result = null;
            int afterParenthesisOpen = (str.IndexOf("(") + 1);
            int comma = str.IndexOf(",");
            if (afterParenthesisOpen >= 0 && comma >= 0)
            {
                string xValue = str.Substring(afterParenthesisOpen, (comma - afterParenthesisOpen)).Trim();
                string yValue = str.Substring((comma + 1), ((str.IndexOf(")") - 1) - comma)).Trim();
                float x, y;
                if (float.TryParse(xValue, out x) && float.TryParse(yValue, out y))
                {
                    result = new Vector2(x, y);
                }
            }

            return result;
        }

        /// <summary>
        /// Parses a Vector3.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.Nullable&lt;Vector3&gt;.</returns>
        public static Vector3? ParseVector3(this String str)
        {
            Vector3? result = null;
            int afterParenthesisOpen = (str.IndexOf("(") + 1);
            int firstComma = str.IndexOf(",");
            int secondComma = str.LastIndexOf(",");
            if (afterParenthesisOpen >= 0 && firstComma >= 0 && secondComma >= 0)
            {
                string xValue = str.Substring(afterParenthesisOpen, (firstComma - afterParenthesisOpen)).Trim();
                string yValue = str.Substring((firstComma + 1), ((secondComma - 1) - firstComma)).Trim();
                string zValue = str.Substring((secondComma + 1), ((str.IndexOf(")") - 1) - secondComma)).Trim();
                float x, y, z;
                if (float.TryParse(xValue, out x) && float.TryParse(yValue, out y) && float.TryParse(zValue, out z))
                {
                    result = new Vector3(x, y, z);
                }
            }
            return result;
        }

        /// <summary>
        /// Replaces the comma with dot.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string ReplaceCommaWithDot(this String str)
        {
            return str.Replace(",", ".");
        }

        /// <summary>
        /// Replaces the underscore with space.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string ReplaceUnderscoreWithSpace(this String str)
        {
            return str.Replace("_", " ");
        }

        /// <summary>
        /// Cleans the string of non digits.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string CleanStringOfNonDigits(this String str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return new string(str.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Counts the nr of occurrences of the parameter in the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="substringToCheck">The substring to check.</param>
        /// <returns>System.Int32.</returns>
        public static int CountNrOfOccurrences(this String str, string substringToCheck)
        {
            int result = 0;
            if (string.IsNullOrEmpty(str)) return result;
            result = Regex.Matches(str, substringToCheck).Count;
            return result;
        }

        /// <summary>
        /// Takes a CamelCase string and adds spaces before capitalised letters.
        /// </summary>
        /// <example>
        /// <code>
        /// string camelCaseString = "ThisIsAString";
        /// string spacedString = camelCaseString.AddSpacesBeforeCapitalLetters();
        /// 
        /// Debug.Log(spacedString); // result: This Is A String
        /// </code>
        /// </example>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AddSpacesBeforeCapitalLetters(this String str)
        {
            string text = str;
            if (string.IsNullOrEmpty(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        public static string LowerAllExceptFirstCapital(this String str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string result = str.ToLower();
                result = result.Remove(0, 1).Insert(0, Char.ToUpper(result[0]).ToString());
            }
            return str;
        }
    }
}
