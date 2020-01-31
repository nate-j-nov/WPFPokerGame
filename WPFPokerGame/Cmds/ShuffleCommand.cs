/*using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using WPFPokerGame.Models.Cards;
using WPFPokerGame.Models;
using WPFPokerGame.ViewModels;

namespace WPFPokerGame.Cmds
{
    public class ShuffleCards : CommandBasePro
    {
        Dealer dealer = new Dealer();
        public override bool CanExecute(object parameter)
        {
            return parameter == null && parameter is ObservableCollection<Card>;
        }

        public override void Execute(object parameter)
        {
            if(parameter is ObservableCollection<Card> cards)
            {
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
}*/