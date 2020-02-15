using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFPokerGame.Models.Cards;
using WPFPokerGame.Models;
using WPFPokerGame.Commands;

namespace WPFPokerGame.ViewModels
{
    public class ViewModel
    {
        Dealer dealer = new Dealer();
        PlayerModel nate = new PlayerModel("Nate");
        public ObservableCollection<PlayerModel> Players { get; set; } = new ObservableCollection<PlayerModel>();
        
        public ObservableCollection<Card> DisplayCards { get; set; } = new ObservableCollection<Card>();
        public ViewModel()
        {
            Players.Add(nate);
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