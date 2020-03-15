using System;
using System.Collections.Generic;
using System.Linq;
using WPFPokerGame.Models.Cards;
using WPFPokerGame.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WPFPokerGame.Models
{
    public abstract class PlayerModel : INotifyPropertyChanged
    {
        private string _playerName;
        public string PlayerName
        {
            get
            {
                return _playerName;
            }
            set
            {
                if (_playerName == value) return;
                _playerName = value;
                RaisePropertyChanged("PlayerName");
            }
        }

        private double _money;
        public double Money
        {
            get
            {
                return _money;
            }
            set
            {
                if (_money == value) return;
                _money = value;
                RaisePropertyChanged("Money");
            }
        }

        /*public List<Card> Hand { get; set; }*/


        public ObservableCollection<Card> Hand { get; set; } = new ObservableCollection<Card>();

        public double RaiseAmount { get; set; } // Doesn't need to be shown at this point in development. 
        public static List<Card> PlayerCommCards { get; set; } = new List<Card>(); // Doesn't need to be shown at this point in development. 
        public static double OtherPlayersBets { get; set; }
        public WinningHands MyBestHand { get; set; } // Doesn't need to be shown at this point in development. 
        public CardFace BestWinningFace { get; private set; } // Doesn't need to be shown at this point in development. 
        public DecisionType PlayersDecision { get; protected set; } // Doesn't need to be shown at this point of development. 


        public PlayerModel(string playerName)
        {
            _playerName = playerName;
            _money = 102;
        }

        public PlayerModel()
        {
            _money = 102;
        }

        public WinningHands GetBestHand()
        {
            var combinedHand = new ObservableCollection<Card>();
            foreach (var c in Hand)
            {
                combinedHand.Add(c);
            }

            if (PlayerCommCards != null)
            {
                foreach (var c in PlayerCommCards)
                {
                    combinedHand.Add(c);
                }
            }


            if (HasRoyalFlush(combinedHand))
            {
                BestWinningFace = CardFace.Ace;
                return WinningHands.RoyalFlush;
            }
            else if (HasStraightFlush(combinedHand))
            {
                BestWinningFace = CardFace.Ace;
                return WinningHands.StraightFlush;
            }
            else if (HasFourOfAKind(combinedHand))
            {
                BestWinningFace = combinedHand.GroupBy(card => card.Face).Where(group => group.Count() == 4)
                    .SelectMany(group => group).Max(card => card.Face);

                return WinningHands.FourOfAKind;
            }
            else if (HasFullHouse(combinedHand))
            {
                List<Card> winningFaces = combinedHand.GroupBy(card => card.Face)
                    .Where(group => group.Count() == 2).SelectMany(group => group).ToList();

                winningFaces.AddRange(combinedHand.GroupBy(card => card.Face)
                    .Where(group => group.Count() == 3).SelectMany(group => group).ToList());

                BestWinningFace = winningFaces.Max(card => card.Face);
                return WinningHands.FullHouse;
            }
            else if (HasFlush(combinedHand))
            {
                BestWinningFace = combinedHand.GroupBy(card => card.Suit).Where(group => group.Count() == 5)
                    .SelectMany(group => group).Max(card => card.Face);
                return WinningHands.Flush;
            }
            else if (HasStraight(combinedHand))
            {
                return WinningHands.Straight;
            }
            else if (HasTrips(combinedHand))
            {
                BestWinningFace = combinedHand.GroupBy(card => card.Face).Where(group => group.Count() == 3)
                    .SelectMany(group => group).Max(card => card.Face); // Finds the cards that makes up the "three-of-a-kind" and finds the max
                                                                        // I know that they are all the same, so taking the max may be a bit redundant, but this was the only way to grab one of the faces I could think of.
                                                                        //Not totally comfortable with LINQ yet.
                return WinningHands.ThreeOfAKind;
            }
            else if (HasAtLeastOnePair(combinedHand))
            {
                List<Card> pairs = combinedHand.GroupBy(card => card.Face)
                    .Where(group => group.Count() == 2).SelectMany(group => group).ToList(); // Finds cards that makes up the pairs

                List<CardFace> pairFaces = new List<CardFace>(); // Creates a list of the faces that make up the pairs.

                foreach (var p in pairs)
                {
                    pairFaces.Add(p.Face); // Adds the faces to the list of faces that make up the pairs.
                }

                BestWinningFace = pairFaces.Max(); // Assigns the highest valued face to the BestWinningFace CardFace member

                if (pairs.Count() == 2)
                    return WinningHands.Pair;
                else
                    return WinningHands.TwoPair;
            }
            else
            {
                return WinningHands.HighCard;
            }
        }

        public bool HasAtLeastOnePair(IEnumerable<Card> _handCards)
        {
            return _handCards.GroupBy(card => card.Face).Any(group => group.Count() == 2);
        }

        public bool HasPair(IEnumerable<Card> _handCards)
        {
            return _handCards.GroupBy(card => card.Face).Count(group => group.Count() >= 2) >= 1;
        }

        public bool HasTrips(IEnumerable<Card> _handCards)
        {
            return _handCards.GroupBy(card => card.Face).Count(group => group.Count() == 3) >= 1;
        }

        public bool HasStraight(IEnumerable<Card> _handCards)
        {
            List<Card> orderedCards = _handCards.OrderByDescending(card => card.Face).ToList();
            for (var x = 0; x < orderedCards.Count - 4; x++)
            {
                var skipped = orderedCards.Skip(x);
                var possibleStraight = skipped.Take(5).ToList();
                if (IsStraight(possibleStraight))
                {
                    BestWinningFace = possibleStraight.Max(card => card.Face);
                    return true;
                }
                else if (IsHighStraight(possibleStraight))
                {
                    BestWinningFace = CardFace.Ace;
                    return true;
                }
                else if (IsLowStraight(possibleStraight))
                {
                    BestWinningFace = CardFace.Five;
                    return true;
                }
            }
            return false;
        }

        public bool IsStraight(List<Card> orderedCards)
        {
            //Counts doubles
            return orderedCards.GroupBy(card => card.Face).Count() == orderedCards.Count() && orderedCards.Max(card => (int)card.Face) - orderedCards.Min(card => (int)card.Face) == 4;
        }

        public bool IsHighStraight(IEnumerable<Card> orderedCards)
        {
            return orderedCards.Where(card => card.Face == CardFace.Ace && card.Face == CardFace.King && card.Face == CardFace.Queen && card.Face == CardFace.Jack && card.Face == CardFace.Ten).Count() >= 5;
        }

        public bool IsLowStraight(IEnumerable<Card> orderedCards)
        {
            return orderedCards.Where(card => card.Face == CardFace.Ace && card.Face == CardFace.Five && card.Face == CardFace.Four && card.Face == CardFace.Three && card.Face == CardFace.Two).Count() >= 5;
        }

        public bool HasFlush(IEnumerable<Card> _handCards)
        {
            return _handCards.GroupBy(card => card.Suit).Count(group => group.Count() >= 5) == 1;
        }

        public bool HasFullHouse(IEnumerable<Card> _handCards)
        {
            return HasTrips(_handCards) && HasPair(_handCards);
        }

        public bool HasFourOfAKind(IEnumerable<Card> _handCards)
        {
            return _handCards.GroupBy(card => card.Face).Count(group => group.Count() >= 4) == 1;
        }

        public bool HasStraightFlush(IEnumerable<Card> _handCards)
        {
            return HasStraight(_handCards) && HasFlush(_handCards);
        }

        public bool HasRoyalFlush(IEnumerable<Card> _handCards)
        {
            return HasFlush(_handCards) && _handCards.Where(card => card.Face == CardFace.Ace || card.Face == CardFace.King || card.Face == CardFace.Queen || card.Face == CardFace.Jack || card.Face == CardFace.Ten).Count() == 5;
        }

        public static void SetOtherPlayersBets(double otherBet)
        {
            OtherPlayersBets = otherBet;
        }

        public abstract bool VerifyDecision();

        public abstract Decision PerformTurn();

        // INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}