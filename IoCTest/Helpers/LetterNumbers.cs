using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ServiceStack;

namespace IoCTest.Helpers
{
    public static class LetterNumbers
    {
        private const int NumLetters = 26;

        public static int LetterToInt(this string str, int offset = 1)
        {
            return str.LetterToNumber(offset).ToInt();
        }

        public static string LetterToNumber(this string str, int offset = 1, bool prependZero = true)
        {
            int number = 0;

            foreach (char letter in str)
            {
                number = number * NumLetters + letter - 'A' + offset;
            }

            number += 1; //<-- I need to shift everything up one due to the "blank" file being first.

            if (number < 10)
            {
                if (prependZero)
                    return "0" + number.ToString();
            }

            return number.ToString();
        }

    }
}