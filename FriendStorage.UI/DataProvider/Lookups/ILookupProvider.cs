using System.Collections.Generic;

namespace FriendStorage.UI.DataProvider.Lookups
{
  public interface ILookupProvider<T>
  {
    IEnumerable<LookupItem> GetLookup();
  }
}
