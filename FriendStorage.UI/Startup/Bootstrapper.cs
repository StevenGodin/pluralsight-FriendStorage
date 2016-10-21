using Autofac;
using FriendStorage.UI.ViewModel;
using Microsoft.Practices.Prism.PubSubEvents;
using FriendStorage.DataAccess;
using FriendStorage.Model;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.DataProvider.Lookups;
using FriendStorage.UI.View.Services;

namespace FriendStorage.UI.Startup
{
  public class Bootstrapper
  {
    public IContainer Bootstrap()
    {
      var builder = new ContainerBuilder();

      builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
      builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

      builder.RegisterType<FileDataService>().As<IDataService>();
      builder.RegisterType<FriendLookupProvider>().As<ILookupProvider<Friend>>();
      builder.RegisterType<FriendGroupLookupProvider>().As<ILookupProvider<FriendGroup>>();
      builder.RegisterType<FriendDataProvider>().As<IFriendDataProvider>();

      builder.RegisterType<FriendEditViewModel>().As<IFriendEditViewModel>();
      builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
      builder.RegisterType<MainViewModel>().AsSelf();

      return builder.Build();
    }
  }
}
