using System.Collections.Generic;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
	[TestClass]
	public class ValidationComplexProperty
	{
		private Friend _friend;

		[TestInitialize]
		public void Initialize()
		{
			_friend = new Friend
			{
				FirstName = "Thomas",
				Address = new Address {City = "Mullheim"},
				Emails = new List<FriendEmail>()
			};
		}

		[TestMethod]
		public void ShouldSetIsValidOfRoot()
		{
			var wrapper = new FriendWrapper(_friend);
			Assert.IsTrue(wrapper.IsValid);

			wrapper.Address.City = "";
			Assert.IsFalse(wrapper.IsValid);

			wrapper.Address.City = "Salt Lake City";
			Assert.IsTrue(wrapper.IsValid);
		}

		[TestMethod]
		public void ShouldSetIsValidOfRootAfterInitialization()
		{
			_friend.Address.City = "";
			var wrapper = new FriendWrapper(_friend);
			Assert.IsFalse(wrapper.IsValid);

			wrapper.Address.City = "Salt Lake City";
			Assert.IsTrue(wrapper.IsValid);
		}

		[TestMethod]
		public void ShouldRaisePropertyChangedEventForIsValidOfRoot()
		{
			var fired = false;
			var wrapper = new FriendWrapper(_friend);
			wrapper.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(wrapper.IsValid))
					fired = true;
			};
			wrapper.Address.City = "";
			Assert.IsTrue(fired);

			fired = false;
			wrapper.Address.City = "Salt Lake City";
			Assert.IsTrue(fired);
		}
	}
}