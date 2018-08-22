using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer
{
    class EmptyAnalizerException:Exception
    {
        public EmptyAnalizerException(string msg):base(msg)
        {

        }
        public EmptyAnalizerException()
        {

        }
    }
}
