using Application.Constants;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameSimulator
{
    public class BowlingGameRandomSimulator(BowlingGame bowlingGame)
    {
        private readonly BowlingGame bowlingGame = bowlingGame;

        public IEnumerable<int> SimulateGame()
        {
            List<int> roundsScore = [];
            var random = new Random();
            var maxScore = BowlingGameConstants.MaxScorePerRound;
            var isFirstRound = true;
            while (!bowlingGame.IsFinished)
            {
                var randomScore = random.Next(0, maxScore + 1);
                bowlingGame.PlayRound(randomScore);
                roundsScore.Add(randomScore);

                maxScore = (randomScore == BowlingGameConstants.MaxScorePerRound || !isFirstRound)
                    ? BowlingGameConstants.MaxScorePerRound
                    : maxScore - randomScore;
                isFirstRound = randomScore == BowlingGameConstants.MaxScorePerRound || !isFirstRound;
            }

            return roundsScore;
        }
    }
}
