using System.Collections.Generic;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
    [TestClass]
    public class ChangeTrackingCollectionTests
    {
        private List<FriendEmailWrapper> _emails;

        [TestInitialize]
        public void Initialize()
        {
            _emails = new List<FriendEmailWrapper>
            {
                new FriendEmailWrapper(new FriendEmail { Email = "thomas@thomasclaudiushuber.com" }),
                new FriendEmailWrapper(new FriendEmail { Email = "julia@juhu-design.com" })
            };
        }

        [TestMethod]
        public void ShouldTrackAddedItems()
        {
            var emailToAdd = new FriendEmailWrapper(new FriendEmail());

            var c = new ChangeTrackingCollection<FriendEmailWrapper>(_emails);
            Assert.AreEqual(2, c.Count);
            Assert.IsFalse(c.IsChanged);

            c.Add(emailToAdd);
            Assert.AreEqual(3, c.Count);
            Assert.AreEqual(1, c.AddedItems.Count);
            Assert.AreEqual(0, c.RemovedItems.Count);
            Assert.AreEqual(0, c.ModifiedItems.Count);
            Assert.AreEqual(emailToAdd, c.AddedItems.First());
            Assert.IsTrue(c.IsChanged);

            c.Remove(emailToAdd);
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual(0, c.AddedItems.Count);
            Assert.AreEqual(0, c.RemovedItems.Count);
            Assert.AreEqual(0, c.ModifiedItems.Count);
            Assert.IsFalse(c.IsChanged);
        }
    }
}