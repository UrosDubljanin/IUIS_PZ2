using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NetworkService.ViewModel
{
    public class HomeViewModel : BindableBase
    {
        public ICommand ShowEntitiesCommand { get; }
        public ICommand ShowGraphCommand { get; }
        public ICommand ShowDisplayCommand { get; }
        public ICommand ExitCommand { get; }

        public ICommand ShowPocetniCommand { get; }

        public event Action<string> OnNavigationRequested;

        public HomeViewModel()
        {
            ShowPocetniCommand = new RelayCommand(_ => Navigate("PocetniView")); 
            ShowEntitiesCommand = new RelayCommand(_ => Navigate("EntitiesView"));
            ShowGraphCommand = new RelayCommand(_ => Navigate("GraphView"));
            ShowDisplayCommand = new RelayCommand(_ => Navigate("DisplayView"));
            ExitCommand = new RelayCommand(_ => Application.Current.Shutdown());
        }

        private void Navigate(string destination)
        {
            OnNavigationRequested?.Invoke(destination);
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

}

