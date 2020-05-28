using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WPFPokerGame.Models.Player;

namespace WPFPokerGame.Models
{
    class Game
    {
        // Properties
        public List<PlayerModel> Players { get; }
        // Perhaps make a bool in player that if it's false, they are faded to indicate that they are no longer participating in the round. 
        public int RoundCount { get; private set; }
        public LoanShark LoanShark = new LoanShark();
        public HumanPlayer HumanPlayingGame = new HumanPlayer();

        // Constructors
        public Game(IEnumerable<PlayerModel> playersInGame)
        {
            Players = playersInGame.ToList();
        }

        // Methods
        public void RunGame()
        {
            do
            {
                NextRound(2.0, LoanShark, RoundCount);
                Players.RemoveAll(player => player.Money < 0.01);
                DeletePlayersHands();
                RoundCount++;
                /*if(HumanPlayingGame.PlayerLoan !=null)
                    LoanShark.MatureLoan(RoundCount)*/
            } while (Players.Count() > 1 && !DidHumanDebtExpire() && HumanPlayingGame.Money > 0.01);

            if(DidHumanDebtExpire() || HumanPlayingGame.Money < 0.01)
            {
                // Implement logic for if the human is out of money. 
            }
            else
            {
                
            }
        }

        public void NextRound(double v, LoanShark loanShark, int roundNumber)
        {
            var nextRound = new Round(v);
            nextRound.RunRound(Players, loanShark, roundNumber);
        }

        public void DeletePlayersHands()
        {
            foreach(var p in Players)
            {
                p.Hand.Clear();
            }
        }

        public bool DidHumanDebtExpire()
        {
            /*if (HumanPlayingGame.PlayerLoan != null)
                return HumanPlayingGame.PlayerLoan.DurationOfLoan > 2;// 4;
            else
                return false;*/
            return false;
        }
        
        public void GetGameWinner(List<PlayerModel> players)
        {
            // Needs to be implemented. 
            throw new NotImplementedException();
        }
    }
}
