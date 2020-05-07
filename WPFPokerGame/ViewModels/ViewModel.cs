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
        
        
        public void PopulateDisplayCards()
        {
            dealer.PopulateDeck();
            dealer.ShuffleDeck();
        }
    }
}