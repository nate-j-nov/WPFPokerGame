using System;
using System.Windows.Input;
using WPFPokerGame.Commands;
using WPFPokerGame.Models;
using System.Threading.Tasks;
using System.Threading;
using Nito.AsyncEx;

namespace WPFPokerGame.Models.Player
{
    public class HumanPlayer : PlayerModel
    {
        // Parameters
        public bool HasALoan { get; set; }
        public bool InDebt;
        public double DebtOutstanding { get; set; }
        public Loan PlayerLoan = null;
        public bool PlayerDecisionChanged { get; set; }
        public IAsyncCommand CallCommandAsync { get; set; }

        public static AsyncManualResetEvent asyncManualResetEvent = new AsyncManualResetEvent(true);

        // Constructors
        public HumanPlayer(string playerName) : base(playerName)
        {
            CallCommandAsync = new AsyncCommand(ExecuteCallAsync, CanCall);
        }

        public HumanPlayer() : base() { }

        public async Task PerformTurn()
        {
            IsPlayerTurn = true;
            await CallCommandAsync.ExecuteAsync();
        }

        /*public void PerformTurn() }*/

        #region Implement PlayerDecisionCommand
        
        // I know this doesn't need to be async--it just needs to be awaitable--I just haven't changed it to be synchronous
        private async Task ExecuteCallAsync()
        {
            Console.WriteLine("Inside ExecuteCallAsync" + Environment.NewLine +
                $"Thread in OnCall: {Thread.CurrentThread.ManagedThreadId}");
            PlayersDecision = DecisionType.Call;
            //asyncManualResetEvent.Set();
        }

        private bool CanCall()
        {
            return IsPlayerTurn;
        }
        #endregion
    }
}

