using NUnit.Framework;
using NSubstitute;
using DoorControlProject;

namespace DoorControlProject.Test
{
    [TestFixture]
    public class DoorControlEntryDeniedTests
    {
        private DoorControl _uut;
        private IDoorControlFactory _mockFactory;

        [SetUp]
        public void Setup()
        {
            _mockFactory = Substitute.For<IDoorControlFactory>();
            _mockFactory.CreateUserValidation().ValidateEntryRequest(Arg.Any<string>()).Returns(false);
            // Ensure that validation will fail
            _uut = new DoorControl(_mockFactory);
        }

        [Test]
        public void RequestEntry_CardDbDeniesEntryRequest_DoorNotOpened()
        {
            _uut.RequestEntry("TFJ");
            _mockFactory.CreateDoor().Received(0).Open();
        }

        [Test]
        public void RequestEntry_CardDbDeniesEntryRequest_BeeperMakeUnhappyNoiseCalled()
        {
            _uut.RequestEntry("TFJ");
            _mockFactory.createEntryNotification().Received().NotifyEntryGranted();
            }

        [Test]
        public void RequestEntry_CardDbDeniesEntryRequest_BeeperMakeHappyNoiseNotCalled()
        {
            _uut.RequestEntry("TFJ");
            _mockFactory.createEntryNotification().Received(0).NotifyEntryGranted();
        }

        [Test]
        public void RequestEntry_CardDbDeniesEntryRequest_AlarmNotSounded()
        {
            _uut.RequestEntry("TFJ");
            _mockFactory.CreateAlarm().Received(0).SoundAlarm();
        }
    }
}
