using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _PROJECT.Scripts.Pickables
{
    public class ScoreKeeper : MonoBehaviour
    {
        public event Action<int, int> AddPointEvent;
        
        public int AllPipsQuantity { get; private set; }
        public int PipsGathered { get; private set; }
        
        private void Awake()
        {
            AllPipsQuantity = transform.childCount;
            PipsGathered = 0;
        }
        
        public void AddPoint()
        {
            PipsGathered++;
            AddPointEvent?.Invoke(PipsGathered, AllPipsQuantity);
            if (PipsGathered == AllPipsQuantity)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
