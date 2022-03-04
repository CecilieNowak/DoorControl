using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControlProject
{
    public interface IEntryNotification
    {
        void NotifyEntryGranted();
        void NotifyEntryDenied();
    }
}
