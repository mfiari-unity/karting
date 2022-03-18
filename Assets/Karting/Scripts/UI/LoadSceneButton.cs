using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneName;

        public bool loadThatScene = false;

        public void LoadTargetScene() 
        {
            if (!LevelManager.instance || loadThatScene)
            {
                SceneManager.LoadSceneAsync(SceneName);
            } else
            {
                SceneManager.LoadSceneAsync(LevelManager.instance.lastScene);
            }
            
        }
    }
}
