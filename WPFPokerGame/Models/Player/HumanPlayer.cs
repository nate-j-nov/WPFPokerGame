using System;

namespace WPFPokerGame.Models.Player
{
    public class HumanPlayer : PlayerModel
    {
        public bool HasALoan { get; set; }
        public bool InDebt;
        public double DebtOutstanding { get; set; }
        public Loan PlayerLoan = null;
        
        public HumanPlayer(string playerName) : base(playerName)
        {

        }
        public HumanPlayer() { }

        public Decision PerformTurn()
        {
            // DecisionType decision = ()userChoice;
            DecisionType decision = DecisionType.Call;
            return new Decision(decision);
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

