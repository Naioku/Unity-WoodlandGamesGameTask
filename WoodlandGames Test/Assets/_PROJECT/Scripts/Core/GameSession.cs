using System;
using _PROJECT.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _PROJECT.Scripts.Core
{
    public class GameSession : MonoBehaviour
    {
        public event Action<int> DropLifeEvent;
        
        [field: SerializeField] public int PlayerLifes { get; private set; }

        public void DropLife()
        {
            PlayerLifes--;
            DropLifeEvent?.Invoke(PlayerLifes);
            if (PlayerLifes == 0)
            {
                SceneManager.LoadScene(SceneManagementEnum.Fail.GetHashCode());
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
