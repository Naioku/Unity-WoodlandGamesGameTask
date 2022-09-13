using System.Linq;
using UnityEngine;

namespace _PROJECT.Scripts.Mechanisms
{
    public class DoorMechanism : MonoBehaviour
    {
        [SerializeField] private ObjectInteractableWithDoor[] confirmedObjects;

        public bool OpenDoor(ObjectInteractableWithDoor objectInteractableObject)
        {
            if (!confirmedObjects.Contains(objectInteractableObject)) return false;
            
            print("Opening the door...");
            // Animation of opening the door.
            
            return true;
        }
    }

    public enum ObjectInteractableWithDoor
    {
        Player,
        Enemy
    }
}
