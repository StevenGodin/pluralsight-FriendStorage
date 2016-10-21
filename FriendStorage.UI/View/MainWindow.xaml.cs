using System.ComponentModel;
using System.Windows;
using FriendStorage.UI.ViewModel;

namespace FriendStorage.UI.View
{
  public partial class MainWindow : Window
  {
	  private MainViewModel _viewModel;


	  public MainWindow(MainViewModel viewModel)
    {
      InitializeComponent();
	    _viewModel = viewModel;
      DataContext = _viewModel;
    }

	  protected override void OnClosing(CancelEventArgs e)
	  {
		  base.OnClosing(e);
		  _viewModel.OnClosing(e);
	  }
  }
}
