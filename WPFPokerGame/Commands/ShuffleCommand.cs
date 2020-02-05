using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using WPFPokerGame.Models.Cards;
using WPFPokerGame.Models;
using WPFPokerGame.ViewModels;

namespace WPFPokerGame.Commands
{
    public class ShuffleCardsCommand : CommandBasePro
    {

        public override bool CanExecute(object parameter) => (parameter as Card) != null;

        public override void Execute(object parameter)
        {
            Dealer dealer = new Dealer();
            if (parameter is ObservableCollection<Card> cards)
            {
                cards.Clear();
                dealer.PopulateDeck();
                dealer.ShuffleDeck();

                List<Card> displayCards = new List<Card>();
                
                for (int x = 0; x <= 2; x++)
                {
                    displayCards.Add(dealer.Deck.Pop());
                }

                foreach (var dc in displayCards)
                {
                    cards.Add(dc);
                }
            }
        }
    }
}