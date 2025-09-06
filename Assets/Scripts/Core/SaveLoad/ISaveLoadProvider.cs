namespace Core.SaveLoad
{
    public interface ISaveLoadProvider
    {
        public string Load(string type);
        public void Save(string type, string data);
    }
}