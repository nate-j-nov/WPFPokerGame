using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPokerGame.Models.Cards;
using WPFPokerGame.Models;
using WPFPokerGame.Models.Player;
using WPFPokerGame.Commands;

namespace WPFPokerGame.ViewModels
{
    public class ViewModel
    {
        Dealer dealer = new Dealer();
        
        // Create players
        public HumanPlayer nate { get; set; } = new HumanPlayer("Nate");
        public ComputerPlayer jake { get; set; } = new ComputerPlayer("Jake");
        public ComputerPlayer evan { get; set; } = new ComputerPlayer("Evan");
        public ComputerPlayer chad { get; set; } = new ComputerPlayer("Chad");

        public ObservableCollection<PlayerModel> PlayersInGame { get; set; } = new ObservableCollection<PlayerModel>();

        public ViewModel()
        {
            // Add players to the ObservableCollection
            PlayersInGame.Add(nate);
            PlayersInGame.Add(jake);
            PlayersInGame.Add(evan);
            PlayersInGame.Add(chad);
            
            PopulateDisplayCards();
            foreach(var player in PlayersInGame)
            {
                dealer.DealPlayerCards(player);
            }
        }
        
        private ICommand _shuffleCardsCommand = null;
        public ICommand ShuffleCardsCmd => _shuffleCardsCommand ?? (_shuffleCardsCommand = new ShuffleCardsCommand());
        public void PopulateDisplayCards()
        {
            dealer.PopulateDeck();
            dealer.ShuffleDeck();
        }
    }
}