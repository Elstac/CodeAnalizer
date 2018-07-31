using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{ 
    public class RepositoryNotFoundException:Exception
    {
        public RepositoryNotFoundException(string msg):base(msg)
        {

        }
    }
}
