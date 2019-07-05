using System;
using System.Text.RegularExpressions;
using UmrechnungTaschenrechner.StringHelpers;

namespace UmrechnungTaschenrechner.Calculators
{
    public class Term
    {
        /// <summary>
        /// Derives the result of a given term, where numbers and operators are seperated by
        /// a space.
        /// </summary>
        /// <param name="term">the provided term</param>
        /// <returns>the result of the term as a decimal number string</returns>
        public static string DeriveTerm(string term)
        {
            var x = Regex.Replace(term, "d([0-9]+,?[0-9]*)|h([0-9a-f]+,?[0-9a-f]*)|o([0-8]+,?[0-8]*)|b([0-1]+,?[0-1]*)|[+]|[*]|[(]|[)]|[-]|[\\/]", string.Empty).Trim();
            if (string.IsNullOrEmpty(x))
            {
                var termMatches = Regex.Matches(term, "d([0-9]+,?[0-9]*)|h([0-9a-f]+,?[0-9a-f]*)|o([0-8]+,?[0-8]*)|b([0-1]+,?[0-1]*)|[+]|[*]|[(]|[)]|[-]|[\\/]");
                var termArr = new string[termMatches.Count];
                int bracketsCount = 0;
                for (int i = 0; i < termMatches.Count; i++)
                {
                    termArr[i] = termMatches[i].Value;
                    if (termArr[i] == "(")
                        bracketsCount++;
                    else if (termArr[i] == ")")
                        bracketsCount--;
                }
                if (bracketsCount != 0)
                    throw new Exception();
                termArr = Brackets(termArr);
                termArr = PointBeforeLines(termArr);
                return CalculateResult(termArr);
            }
            throw new Exception();
        }
        /// <summary>
        /// Asserts the rule to claculate the result of bracets first
        /// </summary>
        /// <param name="termArr">term as array</param>
        /// <returns>term with all subterms derived</returns>
        private static string[] Brackets(string[] termArr)
        {
            for (int i = 0; i < termArr.Length; i++)
            {
                if (termArr[i] == "(")
                {
                    int j;
                    int brackets = 1;
                    string subterm = string.Empty;
                    for (j = i + 1; j < termArr.Length && brackets > 0; j++)
                    {
                        subterm += termArr[j] + " ";
                        if (termArr[j] == "(")
                            brackets++;
                        else if (termArr[j] == ")")
                            brackets--;
                    }
                    //delete rightmost copied char and space from string
                    j--;
                    subterm = subterm.Substring(0, subterm.Length - 2);
                    termArr = termArr.ReplaceManyWithOne(i, j, DeriveTerm(subterm));
                }
            }
            return termArr;
        }
        /// <summary>
        /// Asserts the rule of points before lines in math.
        /// </summary>
        /// <param name="termArr">term as array</param>
        /// <returns>term with only + and - as operands left</returns>
        private static string[] PointBeforeLines(string[] termArr)
        {
            for(int i = 0; i < termArr.Length; i++)
            {
                if(termArr[i] == "*" || termArr[i] == "/")
                {
                    termArr = termArr.ReplaceManyWithOne(i - 1, i + 1, Numbers.Derive(termArr[i - 1], termArr[i + 1], termArr[i]));
                }
            }
            return termArr;
        }
        /// <summary>
        /// Calculates the result of the term from left to right.
        /// </summary>
        /// <param name="termArr">term as array</param>
        /// <returns>result</returns>
        private static string CalculateResult(string[] termArr)
        {
            if(termArr.Length == 1)
            {
                return "d" + Converters.Converter.FromDecString(termArr[0]).ToString();
            }
            while(termArr.Length > 2)
            {
                termArr = termArr.ReplaceManyWithOne(0, 2, Numbers.Derive(termArr[0], termArr[2], termArr[1]));
            }
            return termArr[0];
        }
    }
}
