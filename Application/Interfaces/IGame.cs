namespace Application.Interfaces
{
    public interface IGame
    {
        bool IsFinished { get; }
        void PlayRound(int roundScore);
    }
}