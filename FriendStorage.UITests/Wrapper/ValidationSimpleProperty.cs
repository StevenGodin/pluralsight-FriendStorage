using System.Collections.Generic;
using System.Linq;
using FriendStorage.Model;
using FriendStorage.UI.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FriendStorage.UITests.Wrapper
{
	[TestClass]
	public class ValidationSimpleProperty
	{
		private Friend _friend;

		[TestInitialize]
		public void Initialzie()
		{
			_friend = new Friend
			{
				FirstName = "Thomas",
				Address = new Address {City = "Mullheim"},
				Emails = new List<FriendEmail>()
			};
		}

		[TestMethod]
		public void ShouldReturnValidationErrorIfFirstNameIsEmpty()
		{
			var wrapper = new FriendWrapper(_friend);
			Assert.IsFalse(wrapper.HasErrors);

			wrapper.FirstName = "";
			Assert.IsTrue(wrapper.HasErrors);

			var errors = wrapper.GetErrors(nameof(wrapper.FirstName)).Cast<string>().ToList();
			Assert.AreEqual(1, errors.Count);
			Assert.AreEqual("Firstname is required", errors.First());

			wrapper.FirstName = "Julia";
			Assert.IsFalse(wrapper.HasErrors);
		}

		[TestMethod]
		public void ShouldRaiseErrorsChangedEventWhenFirstNameIsSetToEmptyAndBack()
		{
			var fired = false;
			var wrapper = new FriendWrapper(_friend);

			wrapper.ErrorsChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(wrapper.FirstName))
					fired = true;
			};

			wrapper.FirstName = "";
			Assert.IsTrue(fired);

			fired = false;
			wrapper.FirstName = "Julia";
			Assert.IsTrue(fired);
		}

		[TestMethod]
		public void ShouldSetIsValid()
		{
			var wrapper = new FriendWrapper(_friend);
			Assert.IsTrue(wrapper.IsValid);

			wrapper.FirstName = "";
			Assert.IsFalse(wrapper.IsValid);

			wrapper.FirstName = "Julia";
			Assert.IsTrue(wrapper.IsValid);
		}

		[TestMethod]
		public void ShouldRaisePropertyChangedEventForIsValid()
		{
			var fired = false;
			var wrapper = new FriendWrapper(_friend);

			wrapper.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(wrapper.IsValid))
					fired = true;
			};

			wrapper.FirstName = "";
			Assert.IsTrue(fired);

			fired = false;
			wrapper.FirstName = "Julia";
			Assert.IsTrue(fired);
		}

		[TestMethod]
		public void ShouldSetErrorsAndIsValidAfterInitialization()
		{
			_friend.FirstName = "";
			var wrapper = new FriendWrapper(_friend);

			Assert.IsFalse(wrapper.IsValid);
			Assert.IsTrue(wrapper.HasErrors);

			var errors = wrapper.GetErrors(nameof(wrapper.FirstName)).Cast<string>().ToList();
			Assert.AreEqual(1, errors.Count);
		}

		[TestMethod]
		public void ShouldRefreshErrorsAndIsValidWhenRejectingChanges()
		{
			var wrapper = new FriendWrapper(_friend);
			Assert.IsTrue(wrapper.IsValid);
			Assert.IsFalse(wrapper.HasErrors);

			wrapper.FirstName = "";

			Assert.IsFalse(wrapper.IsValid);
			Assert.IsTrue(wrapper.HasErrors);

			wrapper.RejectChanges();

			Assert.IsTrue(wrapper.IsValid);
			Assert.IsFalse(wrapper.HasErrors);
		}
	}
}