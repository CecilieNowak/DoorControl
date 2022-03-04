using System;
using System.Collections.Generic;
using System.Text;

namespace DoorControlProject
{
    public class DoorControl
    {
        private readonly IUserValidation _userValidation;
        private readonly IDoor _door;
        private readonly IEntryNotification _entryNotification;
        private readonly IAlarm _alarm;
        public enum State
        {
            DoorClosed,
            DoorOpening,
            DoorClosing,
            DoorBreached
        };

        private State _doorState;
        public DoorControl(IDoorControlFactory factory)
        {
            _userValidation = factory.CreateUserValidation();
            _door = factory.CreateDoor();
            _entryNotification = factory.createEntryNotification();
            _alarm = factory.CreateAlarm();
            _doorState = State.DoorClosed;
        }

        public void RequestEntry(string id)
        {
            switch (_doorState)
            {
                case State.DoorClosed:
                    if (_userValidation.ValidateEntryRequest(id) == true)
                    {
                        _door.Open();
                        _entryNotification.NotifyEntryGranted();
                        _doorState = State.DoorOpening;
                    }
                    else
                    {
                        _entryNotification.NotifyEntryDenied();
                    }
                    break;
            }
        }

        public void DoorOpened()
        {
            switch (_doorState)
            {
                case State.DoorOpening:
                    _doorState = State.DoorClosing;
                    _door.Close();
                    break;
                case State.DoorClosed:
                    _alarm.SoundAlarm();
                    _door.Close();
                    _doorState = State.DoorBreached;
                    break;
            }
        }

        public void DoorClosed()
        {
            switch (_doorState)
            {
                case State.DoorClosing:
                    _doorState = State.DoorClosed;
                    break;
            }
        }
    }


}
