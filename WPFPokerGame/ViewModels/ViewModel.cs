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
        public ObservableCollection<Card> CommunityCards { get; set; } = new ObservableCollection<Card>();

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
            DrawCommunityCards(5);
        }

        // Methods

        /// <summary>
        /// Draw the community cards to be displayed on the table. 
        /// Creates temporary card, and puts into the ObservableCollection<Card> CommunityCards 
        /// as well as the static PlayerCommCards parameter in PlayerModel that is used so all players know what they have. 
        /// </summary>
        /// <param name="numCards"></param>
        public void DrawCommunityCards(int numCards)
        {
            for (int i = 0; i < numCards; i++)
            {
                Card tempCard = dealer.DrawCard();
                CommunityCards.Add(tempCard);
                PlayerModel.PlayerCommCards.Add(tempCard);
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