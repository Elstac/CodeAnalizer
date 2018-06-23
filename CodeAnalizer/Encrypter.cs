using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    public class Encrypter
    {
        public static string Encrypt(string text, char[] key)
        {
            string ret= "";
            char[] tmp = text.ToArray();
            int i = 0, tmpi;

            foreach (var character in tmp)
            {
                if (character == '\n')
                    ret += "!";
                else
                {
                    tmpi = NumberToRange(character + key[(i++) % key.Length], 34, 126);
                    ret += (char)tmpi;
                }
            }
            
            return ret;
        }

        public static string Decrypt(string text, char[] key)
        {
            string ret = "";
            char[] tmp = text.ToArray();
            int i = 0,tmpi;

            foreach (var character in tmp)
            {
                if (character == '!')
                    ret += "\n";
                else
                {
                    tmpi = NumberToRange(character - key[(i++) % key.Length], 34, 126);
                    ret += (char)tmpi;
                }
            }
            
            return ret;
        }

        public static int NumberToRange(int number, int beg, int end)
        {
            
            while (number < 0)
                number += end+1;
            number %= end + 1;
            while (number < beg)
                number = (number + beg) % (end+1);
            return number;
        }
    }
}
