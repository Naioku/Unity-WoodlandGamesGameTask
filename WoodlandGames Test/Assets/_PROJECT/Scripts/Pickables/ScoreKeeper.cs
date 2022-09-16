using UnityEngine;
using UnityEngine.SceneManagement;

namespace _PROJECT.Scripts.Pickables
{
    public class ScoreKeeper : MonoBehaviour
    {
        private int _allPipsQuantity;
        private int _pipsGathered;
        
        private void Awake()
        {
            _allPipsQuantity = transform.childCount;
            _pipsGathered = 0;
        }
        
        public void AddPoint()
        {
            _pipsGathered++;
            if (_pipsGathered == _allPipsQuantity)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
