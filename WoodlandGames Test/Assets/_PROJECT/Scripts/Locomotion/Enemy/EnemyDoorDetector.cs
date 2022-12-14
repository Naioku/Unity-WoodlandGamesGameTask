using System;
using _PROJECT.Scripts.Locomotion.Mechanisms;
using UnityEngine;

namespace _PROJECT.Scripts.Locomotion.Enemy
{
    public class EnemyDoorDetector : MonoBehaviour
    {
        public event Action OpenDoorEvent;
        public event Action DoorOpenedEvent;
        
        [SerializeField] private ObjectInteractableWithDoor objectInteractableWithDoor;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out DoorMechanism doorMechanism)) return;
            if (!doorMechanism.OpenDoor(objectInteractableWithDoor)) return;
            
            OpenDoorEvent?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<DoorMechanism>() == null) return;

            DoorOpenedEvent?.Invoke();
        }
    }
}
