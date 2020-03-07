using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}
