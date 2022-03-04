using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NSubstitute;

namespace DoorControlProject.Test
{
    [TestFixture]
    class DoorControlDoorBreachedTests
    {
        private DoorControl _uut;
        private IDoorControlFactory _mockFactory;

        [SetUp]
        public void Setup()
        {
            _mockFactory = Substitute.For<IDoorControlFactory>();
            _uut = new DoorControl(_mockFactory);
        }

        [Test]
        public void DoorBreached_DoorStateIsBreached_AlarmCalled()
        {
            _uut.DoorOpened();
            _mockFactory.CreateAlarm().Received(1).SoundAlarm();
        }

        [Test]
        public void DoorBreached_DoorStateIsBreached_CloseDoorCalled()
        {
            _uut.DoorOpened();
            _mockFactory.CreateDoor().Received().Close();
        }
    }
}
