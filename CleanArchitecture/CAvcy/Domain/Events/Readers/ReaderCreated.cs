using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events.Readers
{
    public class ReaderCreated
    {
        public Guid BookReaderId { get; set; }
        public string Name { get; set; }
    }
}
