using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPokerGame.Models.Player;
using WPFPokerGame.Models;
using System.Windows.Input;


namespace WPFPokerGame.Commands
{
    public class PlayerDecisionCommand : CommandBasePro
    {
        private readonly DecisionType _decision;
        private HumanPlayer _humanPlayer;

        public PlayerDecisionCommand(HumanPlayer humanPlayer, DecisionType decision)
        {
            this._humanPlayer = humanPlayer;
            this._decision = decision;
        }


        public override bool CanExecute(object parameter)
        {
            return _humanPlayer.IsPlayerTurn;
        }

        public override void Execute(object parameter)
        {
            switch (_decision) 
            {       
                case DecisionType.Call:
                    _humanPlayer.PlayersDecision = DecisionType.Call;
                    //_humanPlayer.PlayerDecisionChanged = true;
                    _humanPlayer.PerformTurn();
                    Console.WriteLine("Executed");
                    break;

                case DecisionType.Fold:
                    _humanPlayer.PlayersDecision = DecisionType.Fold;
                    //_humanPlayer.PlayerDecisionChanged = true;
                    Console.WriteLine("Executed");
                    break;

                case DecisionType.Raise:
                    _humanPlayer.PlayersDecision = DecisionType.Raise;
                    //_humanPlayer.PlayerDecisionChanged = true;
                    Console.WriteLine("Executed");
                    break;

                default:
                    break;
            }
        }
    }
}
