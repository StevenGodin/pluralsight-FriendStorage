using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FriendStorage.UI.Behaviors
{
    public class DataGridChangeBehavior
    {
        public static readonly DependencyProperty IsActiveProperty;

        static DataGridChangeBehavior()
        {
            IsActiveProperty = DependencyProperty.RegisterAttached(
            "IsActive", typeof(bool), typeof(DataGridChangeBehavior), new PropertyMetadata(default(bool), OnIsActivePropertyChanged));
        }

        public static void SetIsActive(DependencyObject element, bool value)
        {
            element.SetValue(IsActiveProperty, value);
        }

        public static bool GetIsActive(DependencyObject element)
        {
            return (bool) element.GetValue(IsActiveProperty);
        }

        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid == null)
                return;

            if ((bool) e.NewValue)
                dataGrid.Loaded += DataGrid_Loaded;
            else
                dataGrid.Loaded -= DataGrid_Loaded;
        }

        private static void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
	        var dataGrid = (DataGrid) sender;
	        foreach (var textColumn in dataGrid.Columns.OfType<DataGridTextColumn>())
	        {
		        var binding = textColumn.Binding as Binding;
		        if (binding != null)
		        {
			        textColumn.EditingElementStyle
				        = CreateEditingElementStyle(dataGrid, binding.Path.Path);

			        textColumn.ElementStyle
				        = CreateElementStyle(dataGrid, binding.Path.Path);
		        }
	        }
        }

	    private static Style CreateEditingElementStyle(DataGrid dataGrid, string bindingPath)
	    {
		    var baseStyle = dataGrid.FindResource(typeof(TextBox)) as Style;
		    var style = new Style(typeof(TextBox), baseStyle);
		    AddSetters(style, bindingPath);
		    return style;
	    }

	    private static Style CreateElementStyle(DataGrid dataGrid, string bindingPath)
	    {
		    var baseStyle = dataGrid.FindResource("TextBlockBaseStyle") as Style;
		    var style = new Style(typeof(TextBlock), baseStyle);
		    AddSetters(style, bindingPath);
		    return style;
	    }

	    private static void AddSetters(Style style, string bindingPath)
	    {
		    style.Setters.Add(new Setter(ChangeBehavior.IsActiveProperty, false));

		    style.Setters.Add(new Setter(ChangeBehavior.IsChangedProperty,
			    new Binding(bindingPath + "IsChanged")));

			style.Setters.Add(new Setter(ChangeBehavior.OriginalValueProperty,
				new Binding(bindingPath + "OriginalValue")));
	    }
    }
}