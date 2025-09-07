using TMPro;
using UnityEngine;

namespace UI.Views
{
    public class LetterCellView : CachedMonoBehaviour
    {
        [SerializeField] private TMP_Text _letter;

        public void Setup(string letter)
        {
            _letter.text = letter;
        }
    }
}