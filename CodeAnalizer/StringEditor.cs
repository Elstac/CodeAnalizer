using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    public static class StringEditor
    {
        /// <summary>
        /// Returns raw text in string( without beginig empty characters).
        /// </summary>
        /// <param name="text">Base string</param>
        /// <returns>String that contain raw text</returns>
        public static string GetRawText(string text)
        {
            string ret = text;
            int tmp = 0;

            while (tmp < ret.Length && ret[tmp] == ' ')
                tmp++;

            ret = text.Substring(tmp);

            return ret;
        }
    }
}
