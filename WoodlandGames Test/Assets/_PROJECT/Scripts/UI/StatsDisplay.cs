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
        PlayerLifes _playerLifes;
        private ScoreKeeper _scoreKeeper;

        private void Awake()
        {
            _playerLifes = FindObjectOfType<PlayerLifes>();
            _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        }

        private void Start()
        {
            RefreshLifeDisplay(_playerLifes.Lifes);
            RefreshScoreDisplay(_scoreKeeper.PipsGathered, _scoreKeeper.AllPipsQuantity);
        }

        private void OnEnable()
        {
            _playerLifes.DropLifeEvent += RefreshLifeDisplay;
            _scoreKeeper.AddPointEvent += RefreshScoreDisplay;
        }

        private void RefreshScoreDisplay(int pipsGathered, int allPipsQuantity)
        {
            scoreValueLabel.text = $"{pipsGathered}/{allPipsQuantity}";
        }

        private void OnDisable()
        {
            _playerLifes.DropLifeEvent -= RefreshLifeDisplay;
            _scoreKeeper.AddPointEvent -= RefreshScoreDisplay;

        }

        private void RefreshLifeDisplay(int lifes)
        {
            lifesValueLabel.text = lifes.ToString();
        }
    }
}
