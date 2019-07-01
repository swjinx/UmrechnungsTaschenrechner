using System;
using System.Collections.Generic;
using System.Text;
using UmrechnungTaschenrechner.StringHelpers;

namespace UmrechnungTaschenrechner.Converters
{
    public class Converter
    {
        /// <summary>
        /// Converts a string to a decimal number.
        /// Uses the leading character to determine the base number.
        /// </summary>
        /// <param name="number">number to convert</param>
        /// <returns>number as decimal string</returns>
        public static int FromString(string number)
        {
            switch (number[0])
            {
                case 'b':
                    return NumStringToDec(number,2);
                case 'h':
                    return NumStringToDec(number,16);
                case 'o':
                    return NumStringToDec(number,8);
                case 'd':
                    return NumStringToDec(number,10);
            }
            return 0;
        }
        /// <summary>
        /// Converts a string to a decimal number.
        /// Uses the leading character to determine the base number.
        /// </summary>
        /// <param name="number">number to convert</param>
        /// <returns>number as decimal string</returns>
        public static decimal FromDecString(string number)
        {
            switch (number[0])
            {
                case 'b':
                    return NumStringToDecimal(number, 2);
                case 'h':
                    return NumStringToDecimal(number, 16);
                case 'o':
                    return NumStringToDecimal(number, 8);
                case 'd':
                    return NumStringToDecimal(number, 10);
            }
            return 0;
        }
        /// <summary>
        /// Converts a string to a decimal number.
        /// </summary>
        /// <param name="number">number to convert</param>
        /// <param name="convBase">base the number is in</param>
        /// <returns>number as decimal integer</returns>
        private static int NumStringToDec(string number, int convBase)
        {
            int res = 0;
            number = number.Substring(1);
            int multiply = 1;
            for (int i = number.Length - 1; i > -1; i--)
            {
                res += CharacterToInt(number[i]) * multiply;
                multiply *= convBase;
            }
            if (number[0] == '-')
                res *= -1;
            return res;
        }
        /// <summary>
        /// Converts a string to a decimal number.
        /// </summary>
        /// <param name="number">number to convert</param>
        /// <param name="convBase">base the number is in</param>
        /// <returns>number as decimal integer</returns>
        private static decimal NumStringToDecimal(string number, int convBase)
        {
            decimal res = 0;
            number = number.Substring(1);
            var numberSplit = number.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int multiply = 1;
            for (int i = numberSplit[0].Length - 1; i > -1; i--)
            {
                res += CharacterToInt(numberSplit[0][i]) * multiply;
                multiply *= convBase;
            }
            if(numberSplit.Length == 2)
            {
                multiply = convBase;
                for(int i = 0; i < numberSplit[1].Length; i++)
                {
                    res += CharacterToInt(numberSplit[1][i]) * (1 / (decimal)multiply);
                    multiply *= convBase;
                }
            }
            if (number[0] == '-')
                res *= -1;
            return res;
        }
        /// <summary>
        /// Converts a character to a number so a '0' becomes 0.
        /// </summary>
        /// <param name="number">number to convert as character</param>
        /// <returns>number as integer</returns>
        private static int CharacterToInt(char number)
        {
            if(number >= '0' && number <= '9')
            {
                return number - '0';
            }
            else if(number >= 'a' && number <= 'f')
            {
                return number - 'a' + 10;
            }
            return 0;
        }
        /// <summary>
        /// Converts a decimal number to a given base.
        /// </summary>
        /// <param name="num">number to convert</param>
        /// <param name="b">base to convert to</param>
        /// <returns>convertet number as string</returns>
        public static string NumToString(string num, int b)
        {
            int.TryParse(num, out int numInt);
            string res = string.Empty;
            while(numInt > 0)
            {
                int a = numInt % b;
                res += IntToCharacter(a);
                numInt /= b;
            }
            return res.Reverse();
        }
        /// <summary>
        /// Converts a decimal number to a given base.
        /// </summary>
        /// <param name="num">number to convert</param>
        /// <param name="b">base to convert to</param>
        /// <returns>convertet number as string</returns>
        public static string DecimalToString(string num, int b)
        {
            var numberSplit = num.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int.TryParse(numberSplit[0], out int numInt);
            string res = string.Empty;
            if (numInt == 0)
                res += "0";
            while (numInt > 0)
            {
                int a = numInt % b;
                res += IntToCharacter(a);
                numInt /= b;
            }
            res = res.Reverse();
            res += ',';
            if (numberSplit.Length == 2)
            { 
                string comma = "0," + numberSplit[1];
                int i = 0;
                decimal.TryParse(comma, out decimal numDec);
                while(numDec > 0 && i < 8)
                {
                    numDec *= b;
                    res += IntToCharacter((int)numDec);
                    numDec -= (int)numDec;
                    i++;
                }
                if(numberSplit[1] == "0")
                {
                    res += "0";
                }
            }
            else
            {
                res += '0';
            }
            return res;
        }
        /// <summary>
        /// Converts a number to a string.
        /// Everything above ten is assoumed to be a hexadecimal number.
        /// </summary>
        /// <param name="number">number to convert</param>
        /// <returns>number as string</returns>
        private static string IntToCharacter(int number)
        {
            if (number >= 0 && number <= 9)
            {
                return number.ToString();
            }
            else if (number >= 10 && number <= 15)
            {
                return ((char)('a' + (number - 10))).ToString();
            }
            return "0";
        }
    }
}
