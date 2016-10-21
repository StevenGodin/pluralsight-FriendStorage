using System.Collections.ObjectModel;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider.Lookups;
using FriendStorage.UI.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Linq;
using System.Windows.Input;
using FriendStorage.UI.Command;

namespace FriendStorage.UI.ViewModel
{
  public interface INavigationViewModel
  {
    void Load();
  }

  public class NavigationViewModel : INavigationViewModel
  {
    private readonly IEventAggregator _eventAggregator;
    private readonly ILookupProvider<Friend> _friendLookupProvider;

    public NavigationViewModel(IEventAggregator eventAggregator,
      ILookupProvider<Friend> friendLookupProvider)
    {
      _eventAggregator = eventAggregator;
      _eventAggregator.GetEvent<FriendSavedEvent>().Subscribe(OnFriendSaved);
      _eventAggregator.GetEvent<FriendDeletedEvent>().Subscribe(OnFriendDeleted);
      _friendLookupProvider = friendLookupProvider;
      NavigationItems = new ObservableCollection<NavigationItemViewModel>();
    }

    public void Load()
    {
      NavigationItems.Clear();
      foreach (var friendLookupItem in _friendLookupProvider.GetLookup())
      {
        NavigationItems.Add(
          new NavigationItemViewModel(
            friendLookupItem.Id,
            friendLookupItem.DisplayValue,
            _eventAggregator));
      }
    }

    public ObservableCollection<NavigationItemViewModel> NavigationItems { get; set; }

    private void OnFriendDeleted(int friendId)
    {
      var navigationItem =
        NavigationItems.SingleOrDefault(item => item.FriendId == friendId);
      if (navigationItem != null)
      {
        NavigationItems.Remove(navigationItem);
      }
    }

    private void OnFriendSaved(Friend savedFriend)
    {
      var navigationItem =
        NavigationItems.SingleOrDefault(item => item.FriendId == savedFriend.Id);
      if (navigationItem != null)
      {
        navigationItem.DisplayValue = string.Format("{0} {1}", savedFriend.FirstName, savedFriend.LastName);
      }
      else
      {
        Load();
      }
    }
  }

  public class NavigationItemViewModel : Observable
  {
    private readonly IEventAggregator _eventAggregator;
    private string _displayValue;

    public NavigationItemViewModel(int friendId,
      string displayValue,
      IEventAggregator eventAggregator)
    {
      FriendId = friendId;
      DisplayValue = displayValue;
      _eventAggregator = eventAggregator; ;
      OpenFriendEditViewCommand = new DelegateCommand(OpenFriendEditViewExecute);
    }

    public ICommand OpenFriendEditViewCommand { get; set; }

    public int FriendId { get; private set; }

    public string DisplayValue
    {
      get { return _displayValue; }
      set
      {
        _displayValue = value;
        OnPropertyChanged();
      }
    }

    private void OpenFriendEditViewExecute(object obj)
    {
      _eventAggregator.GetEvent<OpenFriendEditViewEvent>().Publish(FriendId);
    }
  }
}
