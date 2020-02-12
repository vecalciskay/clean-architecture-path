using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InjectionPattern.PatternObjects
{
    public class MyCustomDateTimeService : IDateTimeService
    {
        public DateTime GetDateTime()
        {
            DateTime dt = DateTime.Now.AddMonths(-1);
            return dt;
        }
    }
}
