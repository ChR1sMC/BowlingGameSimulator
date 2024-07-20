using Application.Constants;
using Application.Exceptions;
using Application.Interfaces;
using Application.Models;

namespace Application.Services
{
    public class BowlingGame : IGame, IScore
    {
        private readonly List<Frame> frames = [];
        private bool isFinished;

        public bool IsFinished => isFinished;

        public void PlayRound(int roundScore)
        {
            if(roundScore < BowlingGameConstants.MinScorePerRound
                || roundScore > BowlingGameConstants.MaxScorePerRound)
            {
                throw new ArgumentException($"Round score '{roundScore}' is not in range [{BowlingGameConstants.MinScorePerRound},{BowlingGameConstants.MaxScorePerRound}].", nameof(roundScore));
            }

            if (isFinished)
            {
                throw new GameFinishedException("Can not play after the game is finished.");
            }

            var lastFrame = frames.LastOrDefault();

            //is first bowl
            if (lastFrame?.IsClosed ?? true)
            {
                var frame = new Frame { FirstRoll = roundScore };
                frame.IsClosed = frame.IsStrike;

                isFinished = (frames.Count == BowlingGameConstants.MaxFramesWithBonus && frame.IsStrike)
                    || (frames.Count == BowlingGameConstants.MaxFramesWithBonus + 1 && frames[^1].IsStrike);

                frames.Add(frame);

                return;
            }

            //is second bowl
            if (lastFrame.FirstRoll + roundScore > BowlingGameConstants.MaxScorePerRound)
            {
                throw new ArgumentException($"Two rounds in a frame can not score more than {BowlingGameConstants.MaxScorePerRound} points.", nameof(roundScore));
            }

            lastFrame.SecondRoll = roundScore;
            lastFrame.IsClosed = true;

            isFinished = (frames.Count == BowlingGameConstants.StandartFramesCount && !lastFrame.IsSpare)
                || (frames.Count >= BowlingGameConstants.MaxFramesWithBonus);
        }

        public int Score()
        {
            if (!isFinished)
            {
                throw new GameNotFinishedException("Can not score if the game is not finished.");
            }

            var score = 0;
            Frame frame;
            for (var i = 0; i < BowlingGameConstants.StandartFramesCount; i++)
            {
                frame = frames[i];
                score += frame.Score;

                if (frame.IsStrike)
                {
                    score += frames[i + 1].FirstRoll;
                    score += frames[i + 1].FirstRoll == BowlingGameConstants.MaxScorePerRound
                        ? frames[i + 2].FirstRoll
                        : frames[i + 1].SecondRoll;
                }
                else if (frame.IsSpare)
                {
                    score += frames[i + 1].FirstRoll;
                }
            }
            return score;
        }
    }
}
