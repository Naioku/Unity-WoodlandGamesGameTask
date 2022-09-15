using System.Collections;
using System.Linq;
using UnityEngine;

namespace _PROJECT.Scripts.Locomotion.Mechanisms
{
    public class DoorMechanism : MonoBehaviour
    {
        [SerializeField] private ObjectInteractableWithDoor[] confirmedObjects;
        
        [Tooltip("Wait time between starting an opening animation and starting a closing animation.")]
        [SerializeField] private float waitTimeBetweenOpeningAndClosing = 3f;

        private static readonly int OpenTheDoorHash = Animator.StringToHash("OpenTheDoor");
        private static readonly int CloseTheDoorHash = Animator.StringToHash("CloseTheDoor");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public bool OpenDoor(ObjectInteractableWithDoor objectInteractableObject)
        {
            if (!confirmedObjects.Contains(objectInteractableObject)) return false;

            StartCoroutine(ManageAnimation());
            return true;
        }

        private IEnumerator ManageAnimation()
        {
            _animator.SetTrigger(OpenTheDoorHash);
            yield return new WaitForSecondsRealtime(waitTimeBetweenOpeningAndClosing);
            _animator.SetTrigger(CloseTheDoorHash);
        }
    }

    public enum ObjectInteractableWithDoor
    {
        Player,
        Enemy
    }
}
