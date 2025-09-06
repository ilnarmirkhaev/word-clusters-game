using UnityEngine;

namespace Core.SaveLoad
{
    public class SaveLoadProviderByPrefs : ISaveLoadProvider
    {
        public string Load(string type)
        {
            if (PlayerPrefs.HasKey(type))
            {
                return PlayerPrefs.GetString(type);
            }

            return "";
        }

        public void Save(string type, string data)
        {
            PlayerPrefs.SetString(type, data);
            PlayerPrefs.Save();
        }
    }
}