using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace FriendStorage.UI.Behaviors
{
    public static class ChangeBehavior
    {
        public static readonly DependencyProperty IsChangedProperty;
        public static readonly DependencyProperty OriginalValueProperty;
        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty OriginalValueConverterProperty;

        private static readonly Dictionary<Type, DependencyProperty> _defaultProperties;

        static ChangeBehavior()
        {
            IsChangedProperty = DependencyProperty.RegisterAttached(
                "IsChanged", typeof(bool), typeof(ChangeBehavior), new PropertyMetadata(default(bool)));
            OriginalValueProperty = DependencyProperty.RegisterAttached(
                "OriginalValue", typeof(object), typeof(ChangeBehavior), new PropertyMetadata(default(object)));
            IsActiveProperty = DependencyProperty.RegisterAttached(
                "IsActive", typeof(bool), typeof(ChangeBehavior), new PropertyMetadata(default(bool), OnIsActivePropertyChanged));
            OriginalValueConverterProperty = DependencyProperty.RegisterAttached(
                "OriginalValueConverter", typeof(IValueConverter), typeof(ChangeBehavior),
                new PropertyMetadata(default(IValueConverter), OnOriginalValueConverterPropertyChanged));

            _defaultProperties = new Dictionary<Type, DependencyProperty>
            {
                [typeof(TextBox)] =  TextBox.TextProperty,
                [typeof(CheckBox)] = ToggleButton.IsCheckedProperty,
                [typeof(DatePicker)] = DatePicker.SelectedDateProperty,
                [typeof(ComboBox)] = Selector.SelectedValueProperty
            };
        }

        public static void SetIsChanged(DependencyObject element, bool value)
        {
            element.SetValue(IsChangedProperty, value);
        }

        public static bool GetIsChanged(DependencyObject element)
        {
            return (bool) element.GetValue(IsChangedProperty);
        }

        public static void SetOriginalValue(DependencyObject element, object value)
        {
            element.SetValue(OriginalValueProperty, value);
        }

        public static object GetOriginalValue(DependencyObject element)
        {
            return element.GetValue(OriginalValueProperty);
        }

        public static void SetIsActive(DependencyObject element, bool value)
        {
            element.SetValue(IsActiveProperty, value);
        }

        public static bool GetIsActive(DependencyObject element)
        {
            return (bool) element.GetValue(IsActiveProperty);
        }

        public static void SetOriginalValueConverter(DependencyObject element, IValueConverter value)
        {
            element.SetValue(OriginalValueConverterProperty, value);
        }

        public static IValueConverter GetOriginalValueConverter(DependencyObject element)
        {
            return (IValueConverter) element.GetValue(OriginalValueConverterProperty);
        }

        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (_defaultProperties.ContainsKey(d.GetType()))
            {
                var defaultProperty = _defaultProperties[d.GetType()];
                if ((bool) e.NewValue)
                {
                    var binding = BindingOperations.GetBinding(d, defaultProperty);
                    if (binding != null)
                    {
                        var bindingPath = binding.Path.Path;
                        BindingOperations.SetBinding(d, IsChangedProperty,
                            new Binding(bindingPath + "IsChanged"));
                        CreateOriginalValueBinding(d, bindingPath + "OriginalValue");
                    }
                }
                else
                {
                    BindingOperations.ClearBinding(d, IsChangedProperty);
                    BindingOperations.ClearBinding(d, OriginalValueProperty);
                }
            }
        }

        private static void OnOriginalValueConverterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var originalValueBinding = BindingOperations.GetBinding(d, OriginalValueProperty);
            if (originalValueBinding != null)
                CreateOriginalValueBinding(d, originalValueBinding.Path.Path);
        }

        private static void CreateOriginalValueBinding(DependencyObject d, string originalValueBindingPath)
        {
            var newBinding = new Binding(originalValueBindingPath)
            {
                Converter = GetOriginalValueConverter(d),
                ConverterParameter = d
            };
            BindingOperations.SetBinding(d, OriginalValueProperty, newBinding);
        }
    }
}