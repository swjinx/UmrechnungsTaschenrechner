using System;
using System.Collections.Generic;
using System.Text;
using UmrechnungTaschenrechner.Converters;

namespace UmrechnungTaschenrechner.Calculators
{
    public class Numbers
    {
        /// <summary>
        /// Derives the result of two numbers based on the operator.
        /// </summary>
        /// <param name="numA">First number</param>
        /// <param name="numB">Second number</param>
        /// <param name="oper">Operator used when deriving</param>
        /// <returns>Result as a decimal number string leading with a 'd'</returns>
        public static string Derive(string numA, string numB, string oper)
        {
            switch (oper)
            {
                case "+":
                    return Add(numA, numB);
                case "-":
                    return Sub(numA, numB);
                case "*":
                    return Mul(numA, numB);
                case "/":
                    return Div(numA, numB);
            }
            return "0";
        }
        /// <summary>
        /// Adds two numbers.
        /// </summary>
        /// <param name="numA">First number</param>
        /// <param name="numB">Second number</param>
        /// <returns>result</returns>
        private static string Add(string numA, string numB)
        {
            return "d" + (Converter.FromDecString(numA) + Converter.FromDecString(numB));
        }
        /// <summary>
        /// Subtracts the second number from the first one.
        /// </summary>
        /// <param name="numA">First number</param>
        /// <param name="numB">Second number</param>
        /// <returns>result</returns>
        private static string Sub(string numA, string numB)
        {
            return "d" + (Converter.FromDecString(numA) - Converter.FromDecString(numB));
        }
        /// <summary>
        /// Multiplies two numbers.
        /// </summary>
        /// <param name="numA">First number</param>
        /// <param name="numB">Second number</param>
        /// <returns>result</returns>
        private static string Mul(string numA, string numB)
        {
            return "d" + (Converter.FromDecString(numA) * Converter.FromDecString(numB));
        }
        /// <summary>
        /// Divides the second number from the first one.
        /// </summary>
        /// <param name="numA">First number</param>
        /// <param name="numB">Second number</param>
        /// <returns>result</returns>
        private static string Div(string numA, string numB)
        {
            return "d" + (Converter.FromDecString(numA) / Converter.FromDecString(numB));
        }
    }
}
