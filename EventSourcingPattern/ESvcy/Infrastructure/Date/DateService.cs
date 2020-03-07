using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Date
{
    public class DateService : IDateService
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
