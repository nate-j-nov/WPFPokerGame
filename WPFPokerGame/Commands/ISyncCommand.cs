using System.Windows.Input;

namespace WPFPokerGame.Commands
{
    public interface ISyncCommand : ICommand
    {
        void Execute();
        bool CanExecute();
    }

    public interface ISyncCommand<T> : ICommand
    {
        void Execute(T parameter);
        bool CanExecute(T parameter);
    }
}