using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer.GitTrackerModule.Classes
{
    public class AuthorInfo
    {
        public string name;
        public string email;

        public AuthorInfo(string name,string email)
        {
            this.name = name;
            this.email = email;
        }
        public override bool Equals(object obj)
        {
            AuthorInfo tmp = obj as AuthorInfo;
            return (tmp.email == email && tmp.name == name);
        }
    }
}
