using System;
using System.Collections.Generic;
using UnityEngine;

namespace _PROJECT.Scripts.Core
{
    [CreateAssetMenu(fileName = "StagesConfig", menuName = "StagesConfig/Create new", order = 1)]
    public class StagesConfig : ScriptableObject
    {
        [SerializeField] private StageConfig[] stageConfigs;
        
        private Dictionary<int, Dictionary<ObjectGroupType, Dictionary<DataType, float>>> _lookupTable;

        public bool GetDataValue(DataType dataType, ObjectGroupType objectGroupType, int stageNumber, out float value)
        {
            BuildLookup();
            value = 0f;
            if (!_lookupTable.TryGetValue(stageNumber, out var objectGroup)) return false;
            if (!objectGroup.TryGetValue(objectGroupType, out var objectData)) return false;
            if (!objectData.TryGetValue(dataType, out float valueDict)) return false;
            
            value = valueDict;
            return true;
        }
        
        private void BuildLookup()
        {
            if (_lookupTable != null) return;

            _lookupTable = new Dictionary<int, Dictionary<ObjectGroupType, Dictionary<DataType, float>>>();
            foreach (var stageConfig in stageConfigs)
            {
                var objectGroupsDict = new Dictionary<ObjectGroupType, Dictionary<DataType, float>>();
                foreach (var objectGroup in stageConfig.ObjectGroups)
                {
                    var objectDataDict = new Dictionary<DataType, float>();

                    foreach (var objectData in objectGroup.Stats)
                    {
                        objectDataDict.Add(objectData.Data, objectData.Value);
                    }
                    
                    objectGroupsDict.Add(objectGroup.Group, objectDataDict);
                }
                
                _lookupTable.Add(stageConfig.StageLevel, objectGroupsDict);
            }
        }
    }

    [Serializable]
    public class StageConfig
    {
        [field: SerializeField] [field: Range(1, 99)] public int StageLevel { get; private set; } = 1;
        [field: SerializeField] public ObjectGroup[] ObjectGroups { get; private set; }
    }
    
    [Serializable]
    public class ObjectGroup
    {
        [field: SerializeField] public ObjectGroupType Group { get; private set; }
        [field: SerializeField] public ObjectData[] Stats { get; private set; }
    }

    [Serializable]
    public class ObjectData
    {
        [field: SerializeField] public DataType Data { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
    
    public enum ObjectGroupType
    {
        Enemy
    }

    public enum DataType
    {
        DefaultSpeed,
        ChasingSpeed,
        SightDistance,
        SightAngle
    }
}
