using System;
using System.Collections.Generic;
using System.Text;

namespace UmrechnungTaschenrechner.StringHelpers
{
    public static class ArrayHelpers
    {
        /// <summary>
        /// Replaces the array entries specified with start and end index with a single element.
        /// </summary>
        /// <param name="arr">array</param>
        /// <param name="startIndex">start index</param>
        /// <param name="endIndex">end index</param>
        /// <param name="replace">replacement element</param>
        /// <returns>array with the replacement</returns>
        public static T[] ReplaceManyWithOne<T>(this T[] arr, int startIndex, int endIndex, T replace)
        {
            T[] newArr = new T[arr.Length - (endIndex - startIndex)];
            for(int i = 0, j = 0; i < arr.Length; i++,j++)
            {
                if(i == startIndex)
                {
                    newArr[j] = replace;
                    i = endIndex;
                }
                else
                {
                    newArr[j] = arr[i];
                }
            }
            return newArr;
        }
    }
}
