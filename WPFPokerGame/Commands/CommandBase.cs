using System;
using System.Windows.Input;

namespace WPFPokerGame.Commands
{

    public class CommandBase : ICommand
    {
        private Action _targetExecuteMethod;
        private Func<bool> _targetCanExecuteMethod;

        public CommandBase(Action executeMethod)
        {
            _targetExecuteMethod = executeMethod;
        }

        public CommandBase(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _targetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canExecuteMethod;
        }
        
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged = delegate { };
        
        bool ICommand.CanExecute(object parameter)
        {
            if(_targetCanExecuteMethod != null)
            {
                return _targetCanExecuteMethod();
            }

            if(_targetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        void ICommand.Execute(object parameter)
        {
            if (_targetExecuteMethod != null)
                _targetExecuteMethod();
        }
    }
}