using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WPFPokerGame.Models.Cards;
using WPFPokerGame.Models.Player;

namespace WPFPokerGame.Models
{
    class Round : INotifyPropertyChanged
    {
        // Properties
        private double _ante = 2.00;

        private double _betToMatch;
        public double BetToMatch
        {
            get
            {
                return _betToMatch;
            }
            set
            {
                if (_betToMatch == value) return;
                _betToMatch = value;
                RaisePropertyChanged("BetToMatch");
            }
        }

        private double _pot;
        public double Pot
        {
            get
            {
                return _pot;
            }
            set
            {
                if (_pot == value) return;
                _pot = value;
                RaisePropertyChanged("Pot");
            }
        }

        private Dealer _dealer = new Dealer();
        private List<Card> _commCards = new List<Card>();
        public LoanShark LoanShark = new LoanShark();
        public int RoundCount { get; set; }
        private List<PlayerModel> _roundWinners;

        // Constructors
        public Round() { }
        public Round(double ante)
        {
            _ante = ante;
        }

        // Methods

        /// <summary>
        /// Acts as the driver of the rounds. Essentially all of the businesses logic behind a round.
        /// </summary>
        /// <param name="roundParticipants"></param>
        /// <param name="ls"></param>
        /// <param name="roundNumber"></param>
        public void RunRound(IEnumerable<PlayerModel> roundParticipants, LoanShark ls, int roundNumber)
        {
            LoanShark = ls;
            List<PlayerModel> playersInRound = new List<PlayerModel>();
            playersInRound.AddRange(roundParticipants);

            _dealer.PopulateDeck();
            _dealer.ShuffleDeck();

            PayAntes(playersInRound);
            _betToMatch = _ante;

            PlayerModel.SetOtherPlayersBets(_betToMatch);

            foreach (var p in playersInRound)
            {
                _dealer.DealPlayerCards(p);
            }

            int flips = 0;
            do
            {
                int cardsToDraw;
                if (flips == 0)
                {
                    cardsToDraw = 3;
                    DrawCommunityCards(cardsToDraw);
                }
                else
                {
                    cardsToDraw = 1;
                    DrawCommunityCards(cardsToDraw);
                }
                BettingCycle(playersInRound, flips);
                playersInRound.RemoveAll(player => player.PlayersDecision == DecisionType.Fold);
                flips++;
            } while (flips < 3 && playersInRound.Count() > 1);

            _roundWinners = GetWinner(playersInRound);
            DistributeWinnings(_roundWinners);
            DeleteHands(playersInRound);
            DeletePlayerCommCards();
            _commCards.Clear();
        }

        public void PayAntes(List<PlayerModel> playerList)
        {
            foreach (var p in playerList)
            {
                if (p.Money < _ante)
                {
                    Pot += p.Money;
                    p.Money = 0;
                }
                else
                {
                    p.Money -= _ante;
                    Pot += _ante;
                }
            }
        }

        public void DrawCommunityCards(int numCards)
        {
            for (int i = 0; i < numCards; i++)
            {
                Card tempCard = _dealer.DrawCard();
                _commCards.Add(tempCard);
                PlayerModel.PlayerCommCards.Add(tempCard);
            }
        }

        public void BettingCycle(List<PlayerModel> playerList, int roundNumber)
        {
            foreach (var p in playerList)
            {
                if (p is HumanPlayer)
                {
                    HumanPlayer hp = new HumanPlayer();
                    hp = (HumanPlayer)p;

                    if (hp.PlayerLoan != null)
                    {
                        LoanShark.AskForRepayment(hp);
                    }
                    else
                    {
                        LoanShark.OfferLoan(RoundCount, hp);
                    }
                }
                ExecuteTurn(p, roundNumber);
            }
        }

        public void ExecuteTurn(PlayerModel player, int roundNumber)
        {
            Decision decision = new Decision();
            do
            {
                if (player is ComputerPlayer)
                {
                    ComputerPlayer tempComputerPlayer = (ComputerPlayer)player;
                    decision = tempComputerPlayer.PerformTurn(roundNumber);
                }
                else
                {
                    decision = player.PerformTurn();
                }

                switch (decision.SelDecisionType)
                {
                    case DecisionType.Call:
                        Call(player);
                        break;

                    case DecisionType.Fold:
                        Fold(player);
                        break;

                    case DecisionType.Raise:
                        if (player is HumanPlayer)
                        {
                            HumanPlayer humanPlayer = (HumanPlayer)player;
                        }
                        // Add logic to deal with this. 
                        Raise(player);
                        break;
                }
            } while (!player.VerifyDecision());
        }

        void Call(PlayerModel player)
        {
            _pot += BetToMatch;
            player.Money -= BetToMatch;
        }

        bool Fold(PlayerModel player)
        {
            return true;
        }

        void Raise(PlayerModel player)
        {
            if(player is HumanPlayer)
            {
                try
                {
                    // Get info from player.
                }
                catch
                {
                    Raise(player);
                }
                if(player.RaiseAmount <= player.Money && player.RaiseAmount > 0)
                {
                    player.Money -= (player.RaiseAmount + _betToMatch);
                    _pot += player.RaiseAmount;
                    BetToMatch += player.RaiseAmount;
                }
            }
        }

        public List<PlayerModel> GetWinner(List<PlayerModel> playersInRound)
        {
            List<PlayerModel> roundWinners = new List<PlayerModel>();

            if (playersInRound.Where(player => player.MyBestHand == playersInRound.Max(x => x.MyBestHand)).Count() == 1)
            {
                roundWinners.Add(playersInRound.OrderByDescending(player => player.MyBestHand).First());
                return roundWinners;
            }
            else
            {
                if (playersInRound.Where(player => player.MyBestHand == playersInRound.Max(x => x.MyBestHand) && player.BestWinningFace == playersInRound.Max(x => x.BestWinningFace)).Count() == 1)
                {
                    roundWinners.Add(playersInRound.OrderByDescending(player => player.MyBestHand).ThenByDescending(x => x.BestWinningFace).First());
                }
                else
                {
                    roundWinners.AddRange(playersInRound.Where(player => player.MyBestHand == playersInRound.Max(x => x.MyBestHand) && player.BestWinningFace == playersInRound.Max(x => x.BestWinningFace)));
                }

                return roundWinners;
            }
        }

        public void DistributeWinnings(List<PlayerModel> winners)
        {
            // Add language to say who won. I'm envisioning a little text box up at the top left that says what's happening in the game.
            double eachPlayersWinnings = _pot / winners.Count();

            foreach (var p in winners)
            {
                p.Money += eachPlayersWinnings;
            }
        }

        public void DeleteHands(List<PlayerModel> playerList)
        {
            foreach (var p in playerList)
                p.Hand.Clear();
        }

        public void DeletePlayerCommCards()
        {
            PlayerModel.PlayerCommCards.Clear();
        }

        // INotifyPropertyChagned Implementation
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
