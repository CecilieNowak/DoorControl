using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControlProject
{
    public interface IDoorControlFactory
    {
        IUserValidation CreateUserValidation();
        IDoor CreateDoor();
        IEntryNotification createEntryNotification();
        IAlarm CreateAlarm();
    }
}
