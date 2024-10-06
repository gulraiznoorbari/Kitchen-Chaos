using UnityEngine.SceneManagement;

namespace KitchenChaos.UI.Utility
{
    public static class Loader 
    {
        public enum Scene
        {
            Loading,
            MainMenu,
            Gameplay
        }

        private static Scene targetScene;

        public static void Load(Scene targetScene)
        {
            Loader.targetScene = targetScene;
            SceneManager.LoadScene(Scene.Loading.ToString());
        }

        public static void LoaderCallback()
        {
            SceneManager.LoadScene(targetScene.ToString());
        }
    }
}

