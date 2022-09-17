using System;
using _PROJECT.Scripts.Core;
using UnityEngine;

namespace _PROJECT.Scripts.Pickables
{
    public class ScoreKeeper : MonoBehaviour
    {
        public event Action<int, int> AddPointEvent;
        
        public int AllPipsQuantity { get; private set; }
        public int PipsGathered { get; private set; }

        private StageData _stageData;
        
        private void Awake()
        {
            _stageData = FindObjectOfType<StageData>();
            
            AllPipsQuantity = transform.childCount;
            PipsGathered = 0;
        }
        
        public void AddPoint()
        {
            PipsGathered++;
            AddPointEvent?.Invoke(PipsGathered, AllPipsQuantity);
            if (PipsGathered == AllPipsQuantity)
            {
                _stageData.StageLevelUp();
            }
        }
    }
}
