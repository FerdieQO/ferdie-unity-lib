using System;
using System.Text;

namespace FerdieUnityLib
{
    /// <summary>
    /// Extensions for the Array type.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Joins string values with a comma
        /// </summary>
        /// <param name="array">The array.</param>
        /// <example>
        /// <code>
        /// string[] differentColors = new string[]{ "Red", "Blue", "Green" };
        /// string differentColorsPutTogether = differentColors.JoinStringValues();
        /// Debug.Log(differentColorsPutTogether); // result: "Red, Blue, Green"
        /// </code>
        /// </example>
        /// <returns>System.String.</returns>
        public static string JoinStringValues(this Array array, bool replaceLastCommaWithAnd = false, bool includeSpaces = true)
        {
            StringBuilder builder = new StringBuilder();
            foreach (object value in array)
            {
                if (value != null)
                {
                    builder.Append(value);
                    builder.Append(includeSpaces ? ", " : ",");
                }
            }

            string result = builder.ToString().TrimEnd();
            int indexOfLastComma = result.LastIndexOf(',');
            if (indexOfLastComma >= 0)
            {
                result = result.Remove(indexOfLastComma);
                int indexOfNewLastComma = result.LastIndexOf(',');
                if (replaceLastCommaWithAnd && indexOfNewLastComma >= 0)
                {
                    result = result.Insert(indexOfNewLastComma, " and ");
                    indexOfNewLastComma = result.LastIndexOf(',');
                    result = result.Remove(indexOfNewLastComma, 2);
                }
            }
            return result;
        }
    }
}
