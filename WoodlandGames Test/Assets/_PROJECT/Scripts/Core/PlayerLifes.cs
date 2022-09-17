using System;
using _PROJECT.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _PROJECT.Scripts.Core
{
    public class PlayerLifes : MonoBehaviour
    {
        public event Action<int> DropLifeEvent;

        [field: SerializeField] public int Lifes { get; private set; } = 5;

        public void DropLife()
        {
            Lifes--;
            DropLifeEvent?.Invoke(Lifes);
            if (Lifes == 0)
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
