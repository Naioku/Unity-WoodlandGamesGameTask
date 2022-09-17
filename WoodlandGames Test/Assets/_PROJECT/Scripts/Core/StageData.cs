using UnityEngine;

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
    }
}
