using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InjectionPattern.PatternObjects
{
    public class StandardDateTimeService : IDateTimeService
    {
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }
    }
}
