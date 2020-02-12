using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace WPFPokerGame.Models.Cards
{
    //public class CardModel { }
    public sealed class Card : ModelBase
    {
        // Initializes and retrieves card's face and suit

        public bool IsFrontShowing { get; set; }
        public CardFace Face { get; set; }
        
        public CardSuit Suit { get; set; }
        
        public string FullCardName
        {
            get
            {
                return Face + " of " + Suit;
            }
        }

        //Creates a card 
        public Card(CardFace face, CardSuit suit)
        {
            Face = face;
            Suit = suit;
            IsFrontShowing = false;
        }

        //Prints the card
        public override string ToString()
        {
            return Face + " of " + Suit;
        }
    }
}
