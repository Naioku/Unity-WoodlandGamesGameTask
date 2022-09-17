using UnityEngine;

namespace _PROJECT.Scripts.Core
{
    public class StageData : MonoBehaviour
    {
        [field: SerializeField] public StagesConfig StagesConfig { get; private set; }
        
        [SerializeField] [Range(1, 99)] private int stageLevel;

        public bool GetDataValue(DataType dataType, ObjectGroupType objectGroupType, out float value)
        {
            return StagesConfig.GetDataValue(dataType, objectGroupType, stageLevel, out value);
        }
    }
}
