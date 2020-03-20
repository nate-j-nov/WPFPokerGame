using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WPFPokerGame.Models.Player;
using System.Collections.ObjectModel;

namespace WPFPokerGame.Models
{
    public class Game
    {
        // Properties
        private List<PlayerModel> _playerList { get; set; }
        
        // Perhaps make a bool in player that if it's false, they are faded to indicate that they are no longer participating in the round. 
        public int RoundCount { get; private set; }
        public LoanShark LoanShark = new LoanShark();
        public HumanPlayer HumanPlayingGame = new HumanPlayer();
        public Round CurrentRound { get; set; } 

        // Constructors
        public Game() { }
        public Game(IEnumerable<PlayerModel> playersInGame)
        {
            _playerList = playersInGame.ToList();
        }

        // Methods
        public void RunGame()
        {
            CurrentRound = new Round();
            CurrentRound.RunRound(_playerList, LoanShark, RoundCount);
        }

        /*public void NextRound(double v, LoanShark loanShark, int roundNumber)
        {
            var nextRound = new Round(v);
            nextRound.RunRound(_playerList, loanShark, roundNumber);
        }

        public void DeletePlayersHands()
        {
            foreach(var p in _playerList)
            {
                p.Hand.Clear();
            }
        }

        public bool DidHumanDebtExpire()
        {
            *//*if (HumanPlayingGame.PlayerLoan != null)
                return HumanPlayingGame.PlayerLoan.DurationOfLoan > 2;// 4;
            else
                return false;*//*
            return false;
        }
        
        public void GetGameWinner(List<PlayerModel> players)
        {
            // Needs to be implemented. 
            throw new NotImplementedException();
        }*/
    }
}
