using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAnalizer.ProjectAnalizerModule.Classes.MinorClasses
{
    class DateRange
    {
        private DateTime begin;
        private DateTime end;

        public DateTime Begin { get => begin; set => begin = value; }
        public DateTime End { get => end; set => end = value; }

        public DateRange(DateTime date)
        {
            begin = date;
            end = date;
        }
        public DateRange(DateTime begin, DateTime end)
        {
            this.begin = begin;
            this.end = end;
        }
    }
}
