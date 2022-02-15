using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Trailblazor.Shared.Extensions
{
    public static class RegexHelpers
    {
        public const string CSSLengthPattern = @"^(\d*\.?\d+)+(px|ex|em|rem|%|vh|vw|in|cm|mm|pt|pc)$";

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided text does not satisfy regex pattern.
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="textToMatch"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="RegexParseException"></exception>
        /// <exception cref="RegexMatchTimeoutException"></exception>
        public static void ThrowIfNoMatch(string regex, string textToMatch)
        {
            if (!Regex.IsMatch(textToMatch, regex))
                throw new ArgumentException("Provided text does not satisfy the regex pattern.");
        }
    }
}
