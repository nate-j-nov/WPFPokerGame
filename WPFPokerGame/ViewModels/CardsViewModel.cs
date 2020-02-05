using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using WPFPokerGame.Models.Cards;
using WPFPokerGame.Models;
using WPFPokerGame.Commands;

namespace WPFPokerGame.ViewModels
{
    public class CardsViewModel
    {
        Dealer dealer = new Dealer();
        
        public ShuffleCardsCommand ShuffleCardsCmd { get; private set; }
        public CardsViewModel()
        {
            LoadCards();
            //ICommand _shuffleCardsCommand = null;
            this.ShuffleCardsCmd = new ShuffleCardsCommand();
        }

        // Properties
        public ObservableCollection<Card> DisplayCards { get; } = new ObservableCollection<Card>();

        public void LoadCards()
        {
            List<Card> displayCardsToObservable = PopulateDisplayCards();

            foreach (var c in displayCardsToObservable)
                DisplayCards.Add(c);
        }

        public List<Card> PopulateDisplayCards()
        {
            dealer.PopulateDeck();
            dealer.ShuffleDeck();

            List<Card> displayCards = new List<Card>();

            for (int x = 0; x <= 2; x++)
            {
                displayCards.Add(dealer.Deck.Pop());
            }
            return displayCards;
        }
    }
}