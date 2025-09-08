namespace Gameplay
{
    public interface IPlayingFieldFactory
    {
        IPlayingField Create(int wordCount, int wordLength);
    }
    
    public class PlayingFieldFactory : IPlayingFieldFactory
    {
        public IPlayingField Create(int wordCount, int wordLength)
        {
            return new PlayingFieldState(wordCount, wordLength);
        }
    }
}