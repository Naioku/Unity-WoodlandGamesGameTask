using _PROJECT.Scripts.Core;
using UnityEngine;

namespace _PROJECT.Scripts.Pickables
{
    public class PipPickup : MonoBehaviour
    {
        private GameSession _gameSession;

        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            _gameSession.AddPoint();
            Destroy(gameObject);
        }
    }
}
