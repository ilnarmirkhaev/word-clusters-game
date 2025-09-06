using Core.Modules;
using Core.SceneLoading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class MainMenuWindow : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TMP_Text _passedLevelsCountText;
        [SerializeField] private TMP_Text _nextLevelNumberText;

        [Inject] private SceneLoadingModule _loadingModule;
        [Inject] private LevelsProgressionModule _progressionModule;

        private void Start()
        {
            _playButton.onClick.AddListener(Play);

            SetupLevelsText();
        }

        private void Play()
        {
            _loadingModule.LoadGameLevel();
        }

        private void SetupLevelsText()
        {
            _passedLevelsCountText.text = _progressionModule.GetPassedLevelsCount().ToString();
            _nextLevelNumberText.text = _progressionModule.GetNextLevelNumber().ToString();
        }
    }
}