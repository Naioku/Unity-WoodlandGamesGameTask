using _PROJECT.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _PROJECT.Scripts.Core
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private int playerLifes;

        public void DropLife()
        {
            playerLifes--;
            if (playerLifes == 0)
            {
                SceneManager.LoadScene(SceneManagementEnum.Fail.GetHashCode());
            }
        }
    }
}
