using UnityEngine;
using UnityEngine.SceneManagement;

namespace Invader
{
    public class GameManager : MonoBehaviour
    {
        public void GameOver()
        {
            Time.timeScale = 0f;
        }
        
        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
