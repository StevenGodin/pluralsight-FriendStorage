using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
    [TestClass]
    public class ChangeNotificationSimpleProperty
    {
        private Friend _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new Friend
            {
                FirstName = "Thomas",
                Address = new Address(),
                Emails = new List<FriendEmail>()
            };
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventOnPropertyChange()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(FriendWrapper.FirstName))
                    fired = true;
            };
            wrapper.FirstName = "Julia";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldNotRaisePropertyChangedEventIfPropertyIsSetToSameValue()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(FriendWrapper.FirstName))
                    fired = true;
            };
            wrapper.FirstName = "Thomas";
            Assert.IsFalse(fired);
        }

    }
}