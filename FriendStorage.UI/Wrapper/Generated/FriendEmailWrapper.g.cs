using System;
using System.Linq;
using FriendStorage.Model;

namespace FriendStorage.UI.Wrapper
{
	public partial class FriendEmailWrapper : ModelWrapper<FriendEmail>
	{
		public FriendEmailWrapper(FriendEmail model) : base(model)
		{
		}
	
		public System.Int32 Id
		{
			get { return GetValue<System.Int32>(); }
			set { SetValue(value); }
		}

		public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));

		public bool IdIsChanged => GetIsChanged(nameof(Id));

		public System.String Email
		{
			get { return GetValue<System.String>(); }
			set { SetValue(value); }
		}

		public System.String EmailOriginalValue => GetOriginalValue<System.String>(nameof(Email));

		public bool EmailIsChanged => GetIsChanged(nameof(Email));

		public System.String Comment
		{
			get { return GetValue<System.String>(); }
			set { SetValue(value); }
		}

		public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));

		public bool CommentIsChanged => GetIsChanged(nameof(Comment));
	}
}
