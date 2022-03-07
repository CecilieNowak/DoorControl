using NUnit.Framework;
using NSubstitute;
using DoorControlProject;

namespace DoorControlProject.Test
{
    [TestFixture]
    public class DoorControlEntryGrantedTests
    {
        private DoorControl _uut;
        private IDoorControlFactory _mockFactory;
        
        [SetUp]
        public void Setup()
        {
            _mockFactory = Substitute.For<IDoorControlFactory>();
            _uut = new DoorControl(_mockFactory);

            //Setup for user TFJ to be allowed
            _mockFactory.CreateUserValidation().ValidateEntryRequest("TFJ").Returns(true);
        }

        [Test] //1
        public void RequestEntry_CorrectIdUsedForDbQuery()
        {
            _uut.RequestEntry("TFJ");
            _mockFactory.CreateUserValidation().Received(1).ValidateEntryRequest(("TFJ"));
        }
        
        [Test] //2
        public void RequestEntry_CardDbApprovesEntryRequest_DoorOpenCalled()
        {
            _uut.RequestEntry("TFJ");
            _mockFactory.CreateDoor().Received(1).Open();
        }

        [Test] //3
        public void RequestEntry_CardDbApprovesEntryRequest_DoorCloseNotCalled()
        {
            _uut.RequestEntry("TFJ");
            _mockFactory.CreateDoor().Received(0).Close();
        }

        [Test] //4
        public void RequestEntry_CardDbApprovesEntryRequest_BeeperMakeHappyNoiseNotCalled()
        {
            _uut.RequestEntry("TFJ");
            _mockFactory.createEntryNotification().Received(1).NotifyEntryGranted();
        }

        [Test] //5
        public void RequestEntry_CardDbApprovesEntryRequest_BeeperMakeUnhappyNoiseNotCalled()
        {
            _uut.RequestEntry("TFJ");
            _mockFactory.createEntryNotification().Received(0).NotifyEntryDenied();
        }

        [Test] //6
        public void RequestEntry_DoorOpened_DoorIsClosed()
        {
            _uut.RequestEntry("TFJ");
            _uut.DoorOpened();
            _mockFactory.CreateDoor().Received(1).Close();
        }

        [Test] //7
        public void RequestEntry_DoorOpenedAndClosed_AlarmNotSounded()
        {
            _uut.RequestEntry("TFJ");
            _uut.DoorOpened();
            _uut.DoorClosed();
            _mockFactory.CreateAlarm().Received(0).SoundAlarm();
        }

   
    }
}