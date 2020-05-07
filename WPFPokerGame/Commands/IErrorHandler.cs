using System;

namespace WPFPokerGame.Commands
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}