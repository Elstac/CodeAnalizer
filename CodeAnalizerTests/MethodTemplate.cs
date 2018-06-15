using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizerTests
{
    /// <summary>
    /// Class containing informations about method template
    /// and responsible for comparing strings with this template.
    /// </summary>
    class MethodTemplate
    {
        private string[] template;
        public MethodTemplate(string[] template)
        {
            this.template = template;
        }

        public bool IsInTemplate(string text)
        {


            return false;
        }
    }
}
