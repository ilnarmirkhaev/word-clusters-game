using System.Collections.Generic;
using Core.SceneLoading;
using Gameplay;
using UI.Views;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class WinScreen : MonoBehaviour, ILevelFinishHandler
    {
        [SerializeField] private List<GuessedWordView> _wordViews;

        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _mainMenuButton;

        [Inject] private SceneLoadingModule _sceneLoadingModule;

        private void Start()
        {
            _nextLevelButton.onClick.AddListener(_sceneLoadingModule.LoadGameLevel);
            _mainMenuButton.onClick.AddListener(_sceneLoadingModule.LoadMainMenu);
        }

        public void Handle(IReadOnlyList<string> guessedWords)
        {
            gameObject.SetActive(true);

            for (var i = 0; i < guessedWords.Count; i++)
            {
                _wordViews[i].Setup(i + 1, guessedWords[i]);
            }
        }
    }
}