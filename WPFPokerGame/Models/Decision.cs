namespace WPFPokerGame.Models
{
    public class Decision
    {
        public DecisionType SelDecisionType { get; }
        public double Amount { get; }

        public Decision() { }
        public Decision(DecisionType selDecisionType)
        {
            SelDecisionType = selDecisionType;            
        }
    }
}