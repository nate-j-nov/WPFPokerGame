using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using WPFPokerGame.Models.Cards;
using WPFPokerGame.Models;
using WPFPokerGame.Cmds;

namespace WPFPokerGame.ViewModels
{
    public class CardsViewModel
    {
        Dealer dealer = new Dealer();

        public CommandBase ShuffleCommand { get; set; }

        public CardsViewModel()
        {
            LoadCards();
            ShuffleCommand = new CommandBase(OnShuffle, CanShuffle);
        }

        // Properties
        public ObservableCollection<Card> DisplayCards { get; set; }


        // Constructor
        public void LoadCards()
        {

            DisplayCards = PopulateDisplayCards();
        }

        public ObservableCollection<Card> PopulateDisplayCards()
        {
            dealer.PopulateDeck();
            dealer.ShuffleDeck();

            ObservableCollection<Card> displayCards = new ObservableCollection<Card>();

            for (int x = 0; x <= 2; x++)
            {
                displayCards.Add(dealer.Deck.Pop());
            }
            return displayCards;
        }

        private ObservableCollection<Card> _commandCardsList;
        public ObservableCollection<Card> CommandCardsList
        {
            get
            {
                return _commandCardsList;
            }
            set
            {
                _commandCardsList = value;
                ShuffleCommand.RaiseCanExecuteChanged();
            }
        }


        private void OnShuffle()
        {
            DisplayCards = PopulateDisplayCards();
        }

        private bool CanShuffle()
        {
            return DisplayCards != null;
        }
    }
}