using System;
using System.Collections.Generic;
using WPFPokerGame.Models.Cards;
using System.ComponentModel;

namespace WPFPokerGame.Models
{
    public sealed class Dealer
    {
        public bool IsChanged { get; set; }
        public readonly Stack<Card> Deck = new Stack<Card>();

        public void PopulateDeck()
        {
            foreach (CardSuit s in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardFace f in Enum.GetValues(typeof(CardFace)))
                {
                    Card c = new Card(f, s);
                    Deck.Push(c);
                }
            }
        }

        //Prints deck
        public void PrintDeck()
        {
            foreach (var v in Deck)
            {
                Console.WriteLine(v.ToString());
            }
        }

        //Shuffles deck
        public void ShuffleDeck()
        {
            Random r = new Random();

            Card[] arrOfCards = Deck.ToArray();
            Deck.Clear();
            for (int x = arrOfCards.Length - 1; x > 0; --x)
            {
                int k = r.Next(x + 1);
                var temp = arrOfCards[x];
                arrOfCards[x] = arrOfCards[k];
                arrOfCards[k] = temp;
            }
            foreach (var x in arrOfCards)
            {
                Deck.Push(x);
            }
        }

        //Draws card from deck
        public Card DrawCard()
        {
            return Deck.Pop();
        }

        //Deals hand to player
        /*public void DealPlayerCards(Player player)
        {
            for (int x = 0; x < 2; x++)
            {
                player.Hand.Add(DrawCard());
            }
        }*/
    }
}
