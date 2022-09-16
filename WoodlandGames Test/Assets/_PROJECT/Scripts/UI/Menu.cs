using _PROJECT.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _PROJECT.Scripts.Ui
{
    public class Menu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(SceneManagementEnum.Playground.GetHashCode());
        }
        
        public void QuitGame()
        {
            if (Application.isEditor)
            {
                Debug.Log("Cannot quit the application (Application is editor).");
            }
            else
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
        
        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene(SceneManagementEnum.MainMenu.GetHashCode());
        }
    }
}
