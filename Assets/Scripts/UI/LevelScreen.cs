using Core.SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class LevelScreen : MonoBehaviour
    {
        [SerializeField] private Button _homeButton;

        [Inject] private SceneLoadingModule _sceneLoadingModule;

        private void Start()
        {
            _homeButton.onClick.AddListener(ReturnToMainMenu);
        }

        private void ReturnToMainMenu()
        {
            _sceneLoadingModule.LoadMainMenu();
        }
    }
}