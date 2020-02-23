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
        public ComputerPlayer jake = new ComputerPlayer("Jake");
        public ComputerPlayer evan = new ComputerPlayer("Evan");
        public ComputerPlayer chad = new ComputerPlayer("Chad");

        public ObservableCollection<PlayerModel> PlayersInGame { get; set; } = new ObservableCollection<PlayerModel>();

        public ViewModel()
        {
            // Add players to the ObservableCollection
            PlayersInGame.Add(nate);
            PlayersInGame.Add(jake);
            PlayersInGame.Add(evan);
            PlayersInGame.Add(evan);
            
            PopulateDisplayCards();
            dealer.DealPlayerCards(nate);
        }
        
        /*public void LoadCards()
        {
            List<Card> displayCardsToObservable = PopulateDisplayCards();
            foreach (var c in displayCardsToObservable)
                DisplayCards.Add(c);
        }*/

        private ICommand _shuffleCardsCommand = null;
        public ICommand ShuffleCardsCmd => _shuffleCardsCommand ?? (_shuffleCardsCommand = new ShuffleCardsCommand());
        public void PopulateDisplayCards()
        {
            dealer.PopulateDeck();
            dealer.ShuffleDeck();
            //List<Card> displayCards = new List<Card>();

            /*for (int x = 0; x < 2; x++)
            {
                displayCards.Add(dealer.Deck.Pop());
            }
            return displayCards;*/
        }
    }
}