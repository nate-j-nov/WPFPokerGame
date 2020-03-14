using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPokerGame.Models
{
    class Round
    {
        // Properties
        private double _ante = 2.00;
        public double BetToMatch { get; private set; }
        double Pot;
        Dealer dealer = new Dealer(); // Now this is where I'm unsure if I even need this. Or if I really just need a ViewModel. 
                                      // Or, I could of course have the round take the dealer as a reference parameter from the ViewModel... I think that's what I'll do. 

        // Constructors
        public Round() { }
        public Round(double PLACEHOLDER) { }

        /// <summary>
        /// Acts as the driver of the rounds. Essentially all of the businesses logic behind a round.
        /// </summary>
        /// <param name="roundParticipants"></param>
        /// <param name="ls"></param>
        /// <param name="roundNumber"></param>
        public void RunRound(IEnumerable<PlayerModel> roundParticipants, LoanShark ls, int roundNumber)
        {
            // needs to be added
            throw new NotImplementedException();
        }
    }
}
