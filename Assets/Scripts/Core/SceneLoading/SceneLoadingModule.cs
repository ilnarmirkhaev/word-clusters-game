namespace Core.SceneLoading
{
    public class SceneLoadingModule
    {
        private readonly SceneLoader _sceneLoader;

        private const string MainMenuScene = "StartScene";
        private const string LevelScene = "LevelScene";

        public SceneLoadingModule(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void LoadGameLevel()
        {
            _sceneLoader.LoadScene(LevelScene);
        }

        public void LoadMainMenu()
        {
            _sceneLoader.LoadScene(MainMenuScene);
        }
    }
}