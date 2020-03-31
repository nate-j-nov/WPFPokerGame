using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPokerGame.Models.Cards;
using WPFPokerGame.Models;
using WPFPokerGame.Models.Player;
using WPFPokerGame.Commands;
using System;

namespace WPFPokerGame.ViewModels
{
    public class ViewModel
    {
        Dealer dealer = new Dealer();

        // Create players
        public ComputerPlayer jake { get; set; } = new ComputerPlayer("Jake");
        public ComputerPlayer evan { get; set; } = new ComputerPlayer("Evan");
        public ComputerPlayer chad { get; set; } = new ComputerPlayer("Chad");
        public HumanPlayer nate { get; set; } = new HumanPlayer("Nate");

        public List<PlayerModel> PlayersInGame { get; set; } = new List<PlayerModel>();
        public Game CurrentGame { get; set; }

        public ViewModel()
        {
            // Add players to the ObservableCollection         
            PlayersInGame.Add(jake);
            PlayersInGame.Add(evan);
            PlayersInGame.Add(chad);
            PlayersInGame.Add(nate);


            CurrentGame = new Game(PlayersInGame);
            CurrentGame.RunGame();
        }

        // Methods

        /// <summary>
        /// Draw the community cards to be displayed on the table. 
        /// Creates temporary card, and puts into the ObservableCollection<Card> CommunityCards 
        /// as well as the static PlayerCommCards parameter in PlayerModel that is used so all players know what they have. 
        /// </summary>
        /// <param name="numCards"></param>
        /*public void DrawCommunityCards(int numCards)
        {
            for (int i = 0; i < numCards; i++)
            {
                Card tempCard = dealer.DrawCard();
                CommunityCards.Add(tempCard);
                PlayerModel.PlayerCommCards.Add(tempCard);
            }
        }*/


        // Implement Shuffle Cards Command (Will delete because it's just here for demonstration purposes)
        /* private ICommand _shuffleCardsCommand = null;
         public ICommand ShuffleCardsCmd => _shuffleCardsCommand ?? (_shuffleCardsCommand = new ShuffleCardsCommand());*/

        // Implement Call Command
        private ICommand _commandCall = null;
        public ICommand CommandCall => _commandCall ?? (_commandCall = new PlayerDecisionCommand(nate, DecisionType.Call));

        public void PopulateDisplayCards()
        {
            dealer.PopulateDeck();
            dealer.ShuffleDeck();
        }
    }
}