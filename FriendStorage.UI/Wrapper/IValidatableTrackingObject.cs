using System.ComponentModel;

namespace FriendStorage.UI.Wrapper
{
	public interface IValidatableTrackingObject : IRevertibleChangeTracking, INotifyPropertyChanged
	{
		bool IsValid { get; }
	}
}