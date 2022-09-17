using _PROJECT.Scripts.Core;
using UnityEngine;

namespace _PROJECT.Scripts.UI
{
    public class QuickPauseController : MonoBehaviour
    {
        private GameSession _gameSession;
        private bool _currentState = true;

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
        }

        private void Start()
        {
            ToggleMenu();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleMenu();
            }
        }
        
        public void Resume()
        {
            ToggleMenu();
        }

        public void MainMenu()
        {
            _gameSession.LoadMainManu();
        }

        private void ToggleMenu()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(!_currentState);
            }

            _currentState = !_currentState;
        }
    }
}
