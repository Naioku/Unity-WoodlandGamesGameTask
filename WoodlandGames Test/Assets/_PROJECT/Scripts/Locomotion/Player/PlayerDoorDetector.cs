using _PROJECT.Scripts.Locomotion.Mechanisms;
using UnityEngine;

namespace _PROJECT.Scripts.Locomotion.Player
{
    public class PlayerDoorDetector : MonoBehaviour
    {
        [SerializeField] private ObjectInteractableWithDoor objectInteractableWithDoor;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out DoorMechanism doorMechanism)) return;
            doorMechanism.OpenDoor(objectInteractableWithDoor);
        }
    }
}
