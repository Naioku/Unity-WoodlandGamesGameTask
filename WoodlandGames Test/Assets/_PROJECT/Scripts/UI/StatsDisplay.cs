using _PROJECT.Scripts.Core;
using _PROJECT.Scripts.Pickables;
using TMPro;
using UnityEngine;

namespace _PROJECT.Scripts.UI
{
    public class StatsDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI lifesValueLabel;
        [SerializeField] private TextMeshProUGUI scoreValueLabel;
        GameSession _gameSession;
        private ScoreKeeper _scoreKeeper;

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
            _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        }

        private void Start()
        {
            RefreshLifeDisplay(_gameSession.Lifes);
            RefreshScoreDisplay(_scoreKeeper.PipsGathered, _scoreKeeper.AllPipsQuantity);
        }

        private void OnEnable()
        {
            _gameSession.DropLifeEvent += RefreshLifeDisplay;
            _scoreKeeper.AddPointEvent += RefreshScoreDisplay;
        }

        private void RefreshScoreDisplay(int pipsGathered, int allPipsQuantity)
        {
            scoreValueLabel.text = $"{pipsGathered}/{allPipsQuantity}";
        }

        private void OnDisable()
        {
            _gameSession.DropLifeEvent -= RefreshLifeDisplay;
            _scoreKeeper.AddPointEvent -= RefreshScoreDisplay;

        }

        private void RefreshLifeDisplay(int lifes)
        {
            lifesValueLabel.text = lifes.ToString();
        }
    }
}
