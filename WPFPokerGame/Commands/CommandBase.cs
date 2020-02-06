using System;
using System.Windows.Input;

namespace WPFPokerGame.Commands
{

    public class CommandBase : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        /// <summary>
        /// Creates instance of the command handler
        /// </summary>
        /// <param name="action"> Action to be executed by the comand</param>
        /// <param name="canExecute" A boolean porperty to containing current permissions to execute. 
        public CommandBase(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }
        /// <summary>
        /// Wiresx can execute changed event
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        
        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}