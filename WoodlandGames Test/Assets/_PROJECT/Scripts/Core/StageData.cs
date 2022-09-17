using System;
using _PROJECT.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _PROJECT.Scripts.Core
{
    public class StageData : MonoBehaviour
    {
        public event Action<int> DropLifeEvent;
        
        public int Lifes { get; private set; }
        
        [field: SerializeField] [field: Range(1, 99)] public int StageLevel { get; private set; }
        
        [SerializeField] private int StartingLifes = 5;
        [SerializeField] private StagesConfig stagesConfig;

        private void Start()
        {
            Lifes = StartingLifes;
        }

        public bool GetDataValue(DataType dataType, ObjectGroupType objectGroupType, out float value)
        {
            return stagesConfig.GetDataValue(dataType, objectGroupType, StageLevel, out value);
        }
        
        public void DropLife()
        {
            Lifes--;
            DropLifeEvent?.Invoke(Lifes);
            
            if (Lifes == 0)
            {
                SceneManager.LoadScene(SceneManagementEnum.Fail.GetHashCode());
                ResetStageData();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void StageLevelUp()
        {
            StageLevel++;
            
            if (StageLevel > stagesConfig.GetLevelQuantity())
            {
                SceneManager.LoadScene(SceneManagementEnum.Win.GetHashCode());
                ResetStageData();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void ResetStageData()
        {
            StageLevel = 1;
            Lifes = StartingLifes;
        }
    }
}
