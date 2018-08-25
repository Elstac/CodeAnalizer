using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    public class AnalizerBus
    {
        FileManager fileManager;
        ContributorManager contributorManager;
        ProjectAnalizer projectAnalizer;
        GitChangesTracker gitChangesTracker;

        public AnalizerBus(string[] paths,Language lan)
        {
            LanguageSelector.Language = lan;
            fileManager = new FileManager(paths,lan);
            projectAnalizer = new ProjectAnalizer(fileManager.Analizers);
            contributorManager = new ContributorManager();
        }
        

    }
}
