using System.Collections.Generic;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
	[TestClass]
	public class ValidationCollectionProperty
	{
		private Friend _friend;

		[TestInitialize]
		public void Initialize()
		{
			_friend = new Friend
			{
				FirstName = "Thomas",
				Address = new Address { City = "Mullheim" },
				Emails = new List<FriendEmail>
				{
					new FriendEmail {Email = "thomas@thomasclaudiushuber.com"},
					new FriendEmail {Email = "julia@juhu-design.com" }
				}
			};
		}

		[TestMethod]
		public void ShouldSetIsValidOfRoot()
		{
			var wrapper = new FriendWrapper(_friend);
			Assert.IsTrue(wrapper.IsValid);

			wrapper.Emails.First().Email = "";
			Assert.IsFalse(wrapper.IsValid);

			wrapper.Emails.First().Email = "thomas@thomasclaudiushuber.com";
			Assert.IsTrue(wrapper.IsValid);
		}

		[TestMethod]
		public void ShouldSetIsValidOfRootWhenInitializing()
		{
			_friend.Emails.First().Email = "";
			var wrapper = new FriendWrapper(_friend);
			Assert.IsFalse(wrapper.IsValid);
			Assert.IsFalse(wrapper.HasErrors);
			Assert.IsTrue(wrapper.Emails.First().HasErrors);
		}

		[TestMethod]
		public void ShouldSetIsValidOfRootWhenRemovingInvalidItem()
		{
			var wrapper = new FriendWrapper(_friend);
			Assert.IsTrue(wrapper.IsValid);

			wrapper.Emails.First().Email = "";
			Assert.IsFalse(wrapper.IsValid);

			wrapper.Emails.RemoveAt(0);
			Assert.IsTrue(wrapper.IsValid);
		}

		[TestMethod]
		public void ShouldSetIsValidOfRootWhenAddingInvalidItem()
		{
			var emailToAdd = new FriendEmailWrapper(new FriendEmail());
			var wrapper = new FriendWrapper(_friend);
			Assert.IsTrue(wrapper.IsValid);
			wrapper.Emails.Add(emailToAdd);
			Assert.IsFalse(wrapper.IsValid);
			emailToAdd.Email = "thomas@thomasclaudiushuber.com";
			Assert.IsTrue(wrapper.IsValid);
		}

		[TestMethod]
		public void ShouldRaisePropertyChangedEventForIsValidOfRoot()
		{
			var fired = false;
			var wrapper = new FriendWrapper(_friend);
			wrapper.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == "IsValid")
					fired = true;
			};
			wrapper.Emails.First().Email = "";
			Assert.IsTrue(fired);

			fired = false;
			wrapper.Emails.First().Email = "thomas@thomascloudiushuber.com";
			Assert.IsTrue(fired);
		}

		[TestMethod]
		public void ShouldRaisePropertyChangedEventForIsValidOfRootWhenRemovingInvalidItem()
		{
			var fired = false;
			var wrapper = new FriendWrapper(_friend);
			wrapper.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == "IsValid")
					fired = true;
			};
			wrapper.Emails.First().Email = "";
			Assert.IsTrue(fired);

			fired = false;
			wrapper.Emails.Remove(wrapper.Emails.First());
			Assert.IsTrue(fired);
		}
	}
}