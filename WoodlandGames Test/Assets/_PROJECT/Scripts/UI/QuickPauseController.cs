using _PROJECT.Scripts.Core;
using _PROJECT.Scripts.InputSystem;
using UnityEngine;

namespace _PROJECT.Scripts.UI
{
    public class QuickPauseController : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        
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

        private void OnEnable()
        {
            _inputReader.QuickPauseEvent += ToggleMenu;
        }

        private void OnDisable()
        {
            _inputReader.QuickPauseEvent -= ToggleMenu;
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
