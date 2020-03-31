using System;
using System.Windows.Input;
using WPFPokerGame.Commands;
using WPFPokerGame.Models;
using System.Threading.Tasks;

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


        // Constructors
        public HumanPlayer(string playerName) : base(playerName) {}

        public HumanPlayer() : base() { }

        // Player Decision Commands
        /*public ICommand CommandCall { get; private set; }
        public ICommand CommandFold { get; private set; }
        public ICommand CommandRaise { get; private set; }*/

        // Methods

        //public TaskCompletionSource<bool> WaitingForPlayerDecision = new TaskCompletionSource<bool>();
        public Decision PerformTurn()
        {
            return new Decision(PlayersDecision);
        }

        public async Task WaitForHumanResponse()
        {
            while(PlayersDecision == 0)
            {
                PerformTurn();
                await Task.Delay(5);
            }
        }

        /*public bool InDebt()
        {
            return PlayerLoan.LoanAmount > 0.01;
        }

        public void AcceptLoan(Loan newLoan)
        {
            PlayerLoan = newLoan;
        }

        public void MakePayment(double payment)
        {
            Money -= payment;
            Console.WriteLine(Environment.NewLine + $"Your current debt is now {PlayerLoan.LoanAmount:c}");
        }

        public void PrintDebtOutstanding()
        {
            Console.WriteLine($"Debt Outstanding: {PlayerLoan.LoanAmount:c}" + Environment.NewLine +
                $"Loan Duration: {PlayerLoan.DurationOfLoan}" + Environment.NewLine);
        }*/
    }
}

