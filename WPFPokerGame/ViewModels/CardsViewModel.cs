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

        
        public CardsViewModel()
        {
            LoadCards();
            //ICommand _shuffleCardsCommand = null;
            /*ShuffleCardsCmd = new ShuffleCardsCommand();*/
        }

        // Properties
        public ObservableCollection<Card> DisplayCards { get; } = new ObservableCollection<Card>();

        public void LoadCards()
        {
            List<Card> displayCardsToObservable = PopulateDisplayCards();

            foreach (var c in displayCardsToObservable)
                DisplayCards.Add(c);
        }

        private ICommand _shuffleCardsCommand = null;
        public ICommand ShuffleCardsCmd => _shuffleCardsCommand ?? (_shuffleCardsCommand = new ShuffleCardsCommand());
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