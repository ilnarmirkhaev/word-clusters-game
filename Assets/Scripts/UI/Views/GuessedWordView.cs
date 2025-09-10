using TMPro;
using UnityEngine;

namespace UI.Views
{
    public class GuessedWordView : CachedMonoBehaviour
    {
        [SerializeField] private TMP_Text _guessOrderText;
        [SerializeField] private TMP_Text _wordText;
        
        public void Setup(int number, string word)
        {
            _guessOrderText.text = number.ToString();
            _wordText.text = word;
        }
    }
}