using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPokerGame.Models.Cards;
using WPFPokerGame.Models;
using WPFPokerGame.Models.Player;
using WPFPokerGame.Commands;
using Microsoft.Expression.Interactivity.Core;
using System;
using WPFPokerGame.Services;

namespace WPFPokerGame.ViewModels
{
    public class ViewModel
    {
        private IGameService _gameService;
        private Dealer dealer = new Dealer();

        // Create players
        public HumanPlayer Nate { get; } = new HumanPlayer("Nate");
        public ComputerPlayer Jake { get; } = new ComputerPlayer("Jake");
        public ComputerPlayer Evan { get; } = new ComputerPlayer("Evan");
        public ComputerPlayer Chad { get; } = new ComputerPlayer("Chad");

        public ObservableCollection<PlayerModel> PlayersInGame { get; } = new ObservableCollection<PlayerModel>();
        public ObservableCollection<Card> CommunityCards { get; } = new ObservableCollection<Card>();

        public ICommand CallCommand { get; }
        public ICommand RaiseCommand { get; }
        public ICommand FoldCommand { get; }

        public ViewModel(IGameService gameService)
        {
            _gameService = gameService;

            // Add players to the ObservableCollection
            PlayersInGame.Add(Nate);
            PlayersInGame.Add(Jake);
            PlayersInGame.Add(Evan);
            PlayersInGame.Add(Chad);

            PopulateDisplayCards();
            foreach (var player in PlayersInGame)
            {
                dealer.DealPlayerCards(player);
            }

            CallCommand = new SyncCommand<HumanPlayer>(Call);
            RaiseCommand = new SyncCommand<HumanPlayer>(Raise);
            FoldCommand = new SyncCommand<HumanPlayer>(Fold);

            //DrawCommunityCards(5);
        }

        private void Call(HumanPlayer p)
        {
            //  Pass call command to the game service.
            //  Handle updates to the ViewModel.
            _gameService.HandleCall(p);
        }

        private void Raise(HumanPlayer p)
        {
            //  Pass raise command to the game service.
            //  Handle updates to the ViewModel.
            _gameService.HandleRaise(p);
        }

        private void Fold(HumanPlayer p)
        {
            //  Pass fold command to the game service.
            //  Handle updates to the ViewModel.
            _gameService.HandleFold(p);
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