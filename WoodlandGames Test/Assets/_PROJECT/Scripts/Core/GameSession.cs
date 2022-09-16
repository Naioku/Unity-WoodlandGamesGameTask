using _PROJECT.Scripts.Pickables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _PROJECT.Scripts.Core
{
    public class GameSession : MonoBehaviour
    {
        private int _allPipsQuantity;
        private int _pipsGathered;

        private void Awake()
        {
            _allPipsQuantity = FindObjectsOfType<PipPickup>().Length;
        }

        public void AddPoint()
        {
            _pipsGathered++;
            if (_pipsGathered == _allPipsQuantity)
            {
                LoadNextStage();
            }
        }

        private static void LoadNextStage()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
