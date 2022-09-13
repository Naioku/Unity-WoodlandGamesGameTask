using System;
using _PROJECT.Scripts.Mechanisms;
using UnityEngine;

namespace _PROJECT.Scripts.Locomotion
{
    public class EnemyDoorDetector : MonoBehaviour
    {
        public event Action OpenDoorEvent;
        
        [SerializeField] private ObjectInteractableWithDoor objectInteractableWithDoor;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out DoorMechanism doorMechanism)) return;
            if (!doorMechanism.OpenDoor(objectInteractableWithDoor)) return;
            
            OpenDoorEvent?.Invoke();
        }
    }
}
