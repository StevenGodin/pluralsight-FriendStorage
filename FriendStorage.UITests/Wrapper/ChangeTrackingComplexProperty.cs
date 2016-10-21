using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
    [TestClass]
    public class ChangeTrackingComplexProperty
    {
        private Friend _friend;

        [TestInitialize]
        public void Initialize()
        {
            _friend = new Friend
            {
                FirstName = "Thomas",
                Address = new Address { City = "Mullheim" },
                Emails = new List<FriendEmail>()
            };
        }

        [TestMethod]
        public void ShouldSetIsChangedOfFriendWrapper()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Address.City = "Salt Lake City";
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.Address.City = "Mullheim";
            Assert.IsFalse(wrapper.IsChanged);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangedEventForIsChangedPropertyOfFriendWrapper()
        {
            var fired = false;
            var wrapper = new FriendWrapper(_friend);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(wrapper.IsChanged))
                    fired = true;
            };
            wrapper.Address.City = "Salt Lake City";
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void ShouldAcceptChanges()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Address.City = "Salt Lake City";
            Assert.AreEqual("Mullheim", wrapper.Address.CityOriginalValue);
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.AcceptChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("Salt Lake City", wrapper.Address.City);
            Assert.AreEqual("Salt Lake City", wrapper.Address.CityOriginalValue);
        }

        [TestMethod]
        public void ShouldRejectChanges()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.Address.City = "Salt Lake City";
            Assert.AreEqual("Mullheim", wrapper.Address.CityOriginalValue);
            Assert.IsTrue(wrapper.IsChanged);

            wrapper.RejectChanges();

            Assert.IsFalse(wrapper.IsChanged);
            Assert.AreEqual("Mullheim", wrapper.Address.City);
            Assert.AreEqual("Mullheim", wrapper.Address.CityOriginalValue);
        }
    }
}