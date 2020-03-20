using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPokerGame.Models.Player;
using WPFPokerGame.Models;

namespace WPFPokerGame.Commands
{
    public class CallCommand : CommandBasePro
    {
        // use: https://www.dreamincode.net/forums/topic/346879-wpf-build-an-application-2-commanding-and-enums/
        public override bool CanExecute(object parameter)
        {
            if (parameter != null && (parameter is PlayerModel pm))
            {
                if (pm.IsPlayerTurn)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public override void Execute (object parameter)
        {
            if (parameter is HumanPlayer hp)
            {
                hp.PlayersDecision = DecisionType.Call;
            }
        }
    }
}
