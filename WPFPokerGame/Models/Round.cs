using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using WPFPokerGame.Models.Cards;
using WPFPokerGame.Models.Player;
using System.Collections.ObjectModel;
using System.Threading;

namespace WPFPokerGame.Models
{
    public class Round : INotifyPropertyChanged
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

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (_message == value) return;
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        private List<PlayerModel> PlayersInRound = new List<PlayerModel>();
        private Dealer _dealer = new Dealer();
        public LoanShark LoanShark = new LoanShark();
        public int RoundCount { get; set; }
        //private List<PlayerModel> _roundWinners;

        

        public ObservableCollection<Card> CommunityCards { get; set; } = new ObservableCollection<Card>();

        // Constructors
        public Round() {  /*RoundCount called here*/ }
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
        public async Task RunRound(IEnumerable<PlayerModel> roundParticipants, LoanShark ls, int roundNumber)
        {
            LoanShark = ls;

            PlayersInRound.AddRange(roundParticipants);

            _dealer.PopulateDeck();
            _dealer.ShuffleDeck();

            PayAntes(PlayersInRound);
            _betToMatch = _ante;

            PlayerModel.SetOtherPlayersBets(_betToMatch);
            foreach (var p in PlayersInRound)
            { 
                _dealer.DealPlayerCards(p);
            }

            int flips = 0;

            int cardsToDraw;
            if (flips == 0)
            {
                cardsToDraw = 3;
                DrawCommunityCards(cardsToDraw);
                await BettingCycle(PlayersInRound, RoundCount);
            }
            else
            {
                cardsToDraw = 1;
                DrawCommunityCards(cardsToDraw);
            }
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
                CommunityCards.Add(tempCard);
                PlayerModel.PlayerCommCards.Add(tempCard);
            }
        }

        public async Task BettingCycle(List<PlayerModel> playerList, int roundNumber)
        {
            Console.WriteLine($"Number of players Players in Betting Cycle: {playerList.Count}" + Environment.NewLine + $"Thread: {Thread.CurrentThread.ManagedThreadId}");
            foreach (var p in playerList)
            {
                await ExecuteTurn(p, roundNumber);
                Message = $"{p.PlayerName} chose to {p.PlayersDecision.ToString()}" ;
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        public async Task ExecuteTurn(PlayerModel player, int roundNumber)
        {
            Decision decision;
            player.IsPlayerTurn = true;

            if (player is ComputerPlayer computer)
                decision = computer.PerformTurn(roundNumber);
            else
            {
                var humanPlayer = (HumanPlayer)player;
                humanPlayer.IsPlayerTurn = true;
                Message = $"It's your turn, {humanPlayer.PlayerName}!";
                await humanPlayer.PerformTurn();
                decision = new Decision(humanPlayer.PlayersDecision);
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
                    Raise(player);
                    break;
            }
            Console.WriteLine(Message + Environment.NewLine + $"Thread: {Thread.CurrentThread.ManagedThreadId}");
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
            player.Money -= (player.RaiseAmount + _betToMatch);
            _pot += player.RaiseAmount;
            BetToMatch += player.RaiseAmount;
            Console.WriteLine($"{player.PlayerName} bet {player.RaiseAmount}");
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
