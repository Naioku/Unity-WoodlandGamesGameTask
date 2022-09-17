using System;
using _PROJECT.Scripts.Core;
using UnityEngine;

namespace _PROJECT.Scripts.UI
{
    public class ReturnToMainMenu : MonoBehaviour
    {
        private GameSession _gameSession;

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MainMenu();
            }
        }

        private void MainMenu()
        {
            _gameSession.LoadMainManu();
        }
    }
}
