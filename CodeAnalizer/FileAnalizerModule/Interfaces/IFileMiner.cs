using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer.FileAnalizerModule.Interfaces
{
    public interface IFileMiner
    {
        int GetCharactersCount();
        int GetCommentLines();
        int GetEmptyLines();
        int GetLinesCount();
        int GetMethodsCount();
        Tuple<int,string> GetSmallestFile();
        Tuple<int, string> GetLargestFile();
        int GetUsingsCount();
    }
}
