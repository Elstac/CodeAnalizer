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
            fileManager = new FileManager(paths);
            projectAnalizer = new ProjectAnalizer(fileManager.Analizers);
            contributorManager = new ContributorManager();
        }

        //================File manage methods==================

        public void AddFilessSet(string[] paths)
        {
            fileManager.AddFilesGroup(paths);
        }

        public void RemoveFileSet(string[] paths)
        {
            foreach (var path in paths)
                fileManager.RemoveFiles(path);
        }

        //===================Contributors management======================
        public void AddContributor(string name)
        {

        }
        //=================Statistics methods=============================
        public int CountUsings()
        {
            return projectAnalizer.TotalUsings();
        }
        public int CountProjectsLines()
        {
            return projectAnalizer.TotalLines();
        }
    }
}
