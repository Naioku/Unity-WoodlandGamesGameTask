using UnityEngine;

namespace _PROJECT.Scripts.Pickables
{
    public class PipPickup : MonoBehaviour
    {
        private ScoreKeeper _scoreKeeper;

        private void Awake()
        {
            _scoreKeeper = transform.GetComponentInParent<ScoreKeeper>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _scoreKeeper.AddPoint();
            Destroy(gameObject);
        }
    }
}
