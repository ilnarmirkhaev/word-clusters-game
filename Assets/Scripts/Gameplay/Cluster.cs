namespace Gameplay
{
    public class Cluster
    {
        private readonly string _characters;

        public int Length => _characters.Length;

        public Cluster(string characters)
        {
            _characters = characters;
        }

        public char this[int index] => _characters[index];

        public override string ToString() => _characters;
    }
}