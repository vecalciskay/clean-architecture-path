using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class EventSequence
    {
        public Guid Id { get; set; }
        public long NextSequence { get; set; }
    }
}
