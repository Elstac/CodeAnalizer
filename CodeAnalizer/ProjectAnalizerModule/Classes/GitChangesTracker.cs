﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using LibGit2Sharp;
namespace CodeAnalizer
{
    public class GitChangesTracker : IGitChangesTracker
    {
        private List<Commit> commits;
        public GitChangesTracker(string pathToRepo)
        {
            commits = new List<Commit>();
            Repository repo = new Repository(pathToRepo);
            foreach (var branch in repo.Branches)
                commits.AddRange(branch.Commits.ToList());
        }
        public Tuple<int, int> ChangedLinesCount()
        {
            throw new NotImplementedException();
        }

        public Tuple<int, int> ChangedLinesCount(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, int> ChangedLinesCount(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public int CommitsCount()
        {
            return commits.Count;
        }

        public int CommitsCount(DateTime date)
        {
            int ret = 0;
            foreach (var commit in commits)
                if (commit.Author.When.Date == date)
                    ret++;
            return ret;
        }

        public int CommitsCount(DateTime from, DateTime to)
        {
            int ret = 0;
            foreach (var commit in commits)
                if (commit.Author.When.Date >= from && commit.Author.When.Date<=to)
                    ret++;
            return ret;
        }

        public int CountAuthorCommits(string authorName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetChanges()
        {
            throw new NotImplementedException();
        }

        public List<string> GetChanges(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<string> GetChanges(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public List<string> MessagesTexts()
        {
            throw new NotImplementedException();
        }

        public List<string> MessagesTexts(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<string> MessagesTexts(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
