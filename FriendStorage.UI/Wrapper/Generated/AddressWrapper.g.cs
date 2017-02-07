

using System;
using System.Linq;
using FriendStorage.Model;

namespace FriendStorage.UI.Wrapper
{
	public partial class AddressWrapper : ModelWrapper<Address>
	{
		public AddressWrapper(Address model) : base(model)
		{
		}
	
		public System.Int32 Id
		{
			get { return GetValue<System.Int32>(); }
			set { SetValue(value); }
		}

		public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));

		public bool IdIsChanged => GetIsChanged(nameof(Id));

		public System.String City
		{
			get { return GetValue<System.String>(); }
			set { SetValue(value); }
		}

		public System.String CityOriginalValue => GetOriginalValue<System.String>(nameof(City));

		public bool CityIsChanged => GetIsChanged(nameof(City));

		public System.String Street
		{
			get { return GetValue<System.String>(); }
			set { SetValue(value); }
		}

		public System.String StreetOriginalValue => GetOriginalValue<System.String>(nameof(Street));

		public bool StreetIsChanged => GetIsChanged(nameof(Street));

		public System.String StreetNumber
		{
			get { return GetValue<System.String>(); }
			set { SetValue(value); }
		}

		public System.String StreetNumberOriginalValue => GetOriginalValue<System.String>(nameof(StreetNumber));

		public bool StreetNumberIsChanged => GetIsChanged(nameof(StreetNumber));
	}
}
