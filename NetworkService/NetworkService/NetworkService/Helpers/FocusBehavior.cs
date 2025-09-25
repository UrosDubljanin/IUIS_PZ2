using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace NetworkService.Helpers
{
    public static class FocusBehavior
    {
        public static readonly DependencyProperty GotFocusCommandProperty =
            DependencyProperty.RegisterAttached(
                "GotFocusCommand",
                typeof(ICommand),
                typeof(FocusBehavior),
                new PropertyMetadata(null, OnGotFocusCommandChanged));

        public static void SetGotFocusCommand(UIElement element, ICommand value)
        {
            element.SetValue(GotFocusCommandProperty, value);
        }

        public static ICommand GetGotFocusCommand(UIElement element)
        {
            return (ICommand)element.GetValue(GotFocusCommandProperty);
        }

        private static void OnGotFocusCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement uiElement)
            {
                // skini stari handler
                uiElement.GotFocus -= UiElement_GotFocus;

                // ako je nova komanda postavljena, dodaj handler
                if (e.NewValue is ICommand)
                {
                    uiElement.GotFocus += UiElement_GotFocus;
                }
            }
        }

        private static void UiElement_GotFocus(object sender, RoutedEventArgs e)
        {
            var uiElement = sender as UIElement;
            var command = GetGotFocusCommand(uiElement);
            if (command != null && command.CanExecute(null))
            {
                command.Execute(null);
            }
        }
        public static readonly DependencyProperty LostFocusCommandProperty =
            DependencyProperty.RegisterAttached(
                "LostFocusCommand",
                typeof(ICommand),
                typeof(FocusBehavior),
                new PropertyMetadata(null, OnLostFocusCommandChanged));

        public static void SetLostFocusCommand(UIElement element, ICommand value)
        {
            element.SetValue(LostFocusCommandProperty, value);
        }

        public static ICommand GetLostFocusCommand(UIElement element)
        {
            return (ICommand)element.GetValue(LostFocusCommandProperty);
        }

        private static void OnLostFocusCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement uiElement)
            {
                uiElement.LostFocus -= UiElement_LostFocus;

                if (e.NewValue is ICommand)
                {
                    uiElement.LostFocus += UiElement_LostFocus;
                }
            }
        }

        private static void UiElement_LostFocus(object sender, RoutedEventArgs e)
        {
            var uiElement = sender as UIElement;
            var command = GetLostFocusCommand(uiElement);
            if (command != null && command.CanExecute(null))
            {
                command.Execute(null);
            }
        }
    }
}
