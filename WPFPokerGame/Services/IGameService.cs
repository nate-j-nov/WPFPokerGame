using WPFPokerGame.Models.Player;

namespace WPFPokerGame.Services
{
    public interface IGameService
    {
        void HandleCall(HumanPlayer p);
        void HandleRaise(HumanPlayer p);
        void HandleFold(HumanPlayer p);
    }
}