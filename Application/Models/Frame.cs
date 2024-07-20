using Application.Constants;

namespace Application.Models
{
    public class Frame
    {
        public int FirstRoll { get; set; }
        public int SecondRoll { get; set; }
        public int Score => FirstRoll + SecondRoll;
        public bool IsStrike => FirstRoll == BowlingGameConstants.MaxScorePerRound;
        public bool IsSpare => (Score == BowlingGameConstants.MaxScorePerRound) && !IsStrike;
        public bool IsClosed { get; set; }
    }
}
