using System;
using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
    [TestClass()]
    public class BasicTests
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

        [TestMethod()]
        public void FriendWrapperTest()
        {
            var wrapper = new FriendWrapper(_friend);

            Assert.AreEqual(_friend, wrapper.Model);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowArgumentNullExceptionIfModelIsNull()
        {
            try
            {
                var wrapper = new FriendWrapper(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("model", ex.ParamName);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionIfAddressIsNull()
        {
            try
            {
                _friend.Address = null;
                var wrapper = new FriendWrapper(_friend);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Address cannot be null", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowArgumentExceptionIfEmailsIsNull()
        {
            try
            {
                _friend.Emails = null;
                var wrapper = new FriendWrapper(_friend);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Emails cannot be null", ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void ShouldGetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendWrapper(_friend);
            Assert.AreEqual(_friend.FirstName, wrapper.FirstName);
        }

        [TestMethod]
        public void ShouldSetValueOfUnderlyingModelProperty()
        {
            var wrapper = new FriendWrapper(_friend);
            wrapper.FirstName = "Julia";
            Assert.AreEqual("Julia", _friend.FirstName);
        }

    }
}