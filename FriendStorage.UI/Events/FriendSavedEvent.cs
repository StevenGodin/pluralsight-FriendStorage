using FriendStorage.Model;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FriendStorage.UI.Events
{
  public class FriendSavedEvent : PubSubEvent<Friend>
  {
  }
}
