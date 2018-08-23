using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    public class ContributorManager
    {
        private List<Contributor> contributors;

        public ContributorManager()
        {
            contributors = new List<Contributor>();
        }

        public void AddContributor(string name)
        {
            contributors.Add(new Contributor(name))
        }
    }
}
