using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        public void LoadNextScene()
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCount)
                SceneManager.LoadScene(0);
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }    
    }
}
