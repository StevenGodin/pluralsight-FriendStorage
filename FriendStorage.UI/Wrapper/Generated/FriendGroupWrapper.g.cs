using System;
using System.Linq;
using FriendStorage.Model;

namespace FriendStorage.UI.Wrapper
{
	public partial class FriendGroupWrapper : ModelWrapper<FriendGroup>
	{
		public FriendGroupWrapper(FriendGroup model) : base(model)
		{
		}
	
		public System.Int32 Id
		{
			get { return GetValue<System.Int32>(); }
			set { SetValue(value); }
		}

		public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));

		public bool IdIsChanged => GetIsChanged(nameof(Id));

		public System.String Name
		{
			get { return GetValue<System.String>(); }
			set { SetValue(value); }
		}

		public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));

		public bool NameIsChanged => GetIsChanged(nameof(Name));
	}
}
