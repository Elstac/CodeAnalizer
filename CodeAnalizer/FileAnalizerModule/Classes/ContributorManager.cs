﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer.FileAnalizerModule.Classes
{
    public class ContributorManager
    {
        private List<Contributor> contributors;
        public List<Contributor> Contributors { get => contributors;}
        public ContributorManager()
        {
            contributors = new List<Contributor>();
        }


        public void AddContributor(string name)
        {
            contributors.Add(new Contributor(name));
            
        }

        public void AddContributor(string name,string[] paths)
        {
            contributors.Add(new Contributor(name,new FileSetMiner(paths)));
        }

        public void AddFilesToContributor(string contributorName, string[] paths)
        {
            bool succes = false;
            foreach (var con in contributors)
            {
                if (con.Name == contributorName)
                {
                    succes = true;
                    con.AddFiles(paths);
                }
                if (!succes)
                    throw new MissingFieldException("Contributor " + contributorName + " doesnt exist");

            }
        }
        public void RemoveFiles(string contributorName, string[] paths)
        {
            bool succes = false;
            foreach (var con in contributors)
            {
                if (con.Name == contributorName)
                {
                    succes = true;
                    con.RemoveFiles(paths);
                }
                if (!succes)
                    throw new MissingFieldException("Contributor " + contributorName + " doesnt exist");
               
            }
        }

    }
}
