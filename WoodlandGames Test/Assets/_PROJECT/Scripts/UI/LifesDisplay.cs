using _PROJECT.Scripts.Core;
using TMPro;
using UnityEngine;

namespace _PROJECT.Scripts.UI
{
    public class LifesDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI lifesValueLabel;
        GameSession _gameSession;

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
        }

        private void Start()
        {
            RefreshDisplay(_gameSession.PlayerLifes);
        }

        private void OnEnable()
        {
            _gameSession.DropLifeEvent += RefreshDisplay;
        }

        private void OnDisable()
        {
            _gameSession.DropLifeEvent -= RefreshDisplay;
        }

        private void RefreshDisplay(int lifes)
        {
            lifesValueLabel.text = lifes.ToString();
        }
    }
}
