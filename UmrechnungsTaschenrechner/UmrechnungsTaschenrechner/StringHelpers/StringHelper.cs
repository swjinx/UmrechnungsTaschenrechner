using System;
using System.Collections.Generic;
using System.Text;

namespace UmrechnungTaschenrechner.StringHelpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Reverses a string.
        /// </summary>
        /// <param name="a">string to reverse</param>
        /// <returns>reversed string</returns>
        public static string Reverse(this string a)
        {
            if(a.Length < 1)
            {
                return string.Empty;
            }
            char[] x = new char[a.Length];
            for(int i = 0; i <= a.Length/2; i++)
            {
                x[i] = a[a.Length - 1 - i];
                x[a.Length - 1 - i] = a[i];
            }
            return new string(x);
        }
    }
}
