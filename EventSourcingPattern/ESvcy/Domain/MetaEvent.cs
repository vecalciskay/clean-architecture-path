using Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MetaEvent
    {
        public Guid Id { get; set; }
        public string StreamName { get; set; }
        public string EventClass { get; set; }
        public Guid InstanceId { get; set; }
        public string SerializedEvent { get; set; }
        public long Version { get; set; }

        public MetaEvent() { }
    }
}
