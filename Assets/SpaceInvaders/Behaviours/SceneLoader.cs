using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceInvaders.Game
{
    public class SceneLoader: MonoBehaviour
    {
        public void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}