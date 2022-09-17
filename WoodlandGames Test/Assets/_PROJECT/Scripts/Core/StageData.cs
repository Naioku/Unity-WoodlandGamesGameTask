using _PROJECT.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _PROJECT.Scripts.Core
{
    public class StageData : MonoBehaviour
    {
        [field: SerializeField] [field: Range(1, 99)] public int StageLevel { get; private set; }
        
        [SerializeField] private StagesConfig stagesConfig;

        public bool GetDataValue(DataType dataType, ObjectGroupType objectGroupType, out float value)
        {
            return stagesConfig.GetDataValue(dataType, objectGroupType, StageLevel, out value);
        }

        public void StageLevelUp()
        {
            StageLevel++;
            
            if (StageLevel > stagesConfig.GetLevelQuantity())
            {
                SceneManager.LoadScene(SceneManagementEnum.Win.GetHashCode());
                ResetStageLevel();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void ResetStageLevel()
        {
            StageLevel = 1;
        }
    }
}
