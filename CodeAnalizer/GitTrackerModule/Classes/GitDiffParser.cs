using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer.GitTrackerModule.Classes
{
    public static class GitDiffParser
    {
        public static string GetChangedLinesText(string diffText)
        {
            string ret = "";

            List<string> tmp = StringEditor.GetLines(diffText);

            foreach (var item in tmp)
            {
                string text = StringEditor.GetRawText(item);
                if (text.Length == 0)
                    continue;
                if (text.First() == '+' || text.First() == '-')
                    if (text.Length == 1 || (text[1] != '+' && text[1] != '-'))
                        ret += text + "\n";
            }
            return ret;
        }
    }
}
