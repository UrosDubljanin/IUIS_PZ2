using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace NetworkService.Helpers
{
        public static class DragDropBehavior
        {
            #region DropCommand
            public static readonly DependencyProperty DropCommandProperty =
                DependencyProperty.RegisterAttached(
                    "DropCommand",
                    typeof(ICommand),
                    typeof(DragDropBehavior),
                    new PropertyMetadata(null, OnDropCommandChanged));

            public static void SetDropCommand(UIElement element, ICommand value)
            {
                element.SetValue(DropCommandProperty, value);
            }

            public static ICommand GetDropCommand(UIElement element)
            {
                return (ICommand)element.GetValue(DropCommandProperty);
            }

            private static void OnDropCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                if (d is UIElement ui)
                {
                    ui.Drop -= Ui_Drop;
                    if (e.NewValue != null)
                        ui.Drop += Ui_Drop;
                }
            }

            private static void Ui_Drop(object sender, DragEventArgs e)
            {
                var ui = sender as UIElement;
                var command = GetDropCommand(ui);
                if (command != null && command.CanExecute(e))
                    command.Execute(e);
            }
            #endregion

            #region DragOverCommand
            public static readonly DependencyProperty DragOverCommandProperty =
                DependencyProperty.RegisterAttached(
                    "DragOverCommand",
                    typeof(ICommand),
                    typeof(DragDropBehavior),
                    new PropertyMetadata(null, OnDragOverCommandChanged));

            public static void SetDragOverCommand(UIElement element, ICommand value)
            {
                element.SetValue(DragOverCommandProperty, value);
            }

            public static ICommand GetDragOverCommand(UIElement element)
            {
                return (ICommand)element.GetValue(DragOverCommandProperty);
            }

            private static void OnDragOverCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                if (d is UIElement ui)
                {
                    ui.DragOver -= Ui_DragOver;
                    if (e.NewValue != null)
                        ui.DragOver += Ui_DragOver;
                }
            }

            private static void Ui_DragOver(object sender, DragEventArgs e)
            {
                var ui = sender as UIElement;
                var command = GetDragOverCommand(ui);
                if (command != null && command.CanExecute(e))
                    command.Execute(e);
            }
            #endregion

            #region MouseDownCommand
            public static readonly DependencyProperty MouseDownCommandProperty =
                DependencyProperty.RegisterAttached(
                    "MouseDownCommand",
                    typeof(ICommand),
                    typeof(DragDropBehavior),
                    new PropertyMetadata(null, OnMouseDownCommandChanged));

            public static void SetMouseDownCommand(UIElement element, ICommand value)
            {
                element.SetValue(MouseDownCommandProperty, value);
            }

            public static ICommand GetMouseDownCommand(UIElement element)
            {
                return (ICommand)element.GetValue(MouseDownCommandProperty);
            }

            private static void OnMouseDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                if (d is UIElement ui)
                {
                    ui.MouseLeftButtonDown -= Ui_MouseLeftButtonDown;
                    if (e.NewValue != null)
                        ui.MouseLeftButtonDown += Ui_MouseLeftButtonDown;
                }
            }

            private static void Ui_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                var ui = sender as UIElement;
                var command = GetMouseDownCommand(ui);
                if (command != null && command.CanExecute(e))
                    command.Execute(e);
            }
            #endregion
        }
}

