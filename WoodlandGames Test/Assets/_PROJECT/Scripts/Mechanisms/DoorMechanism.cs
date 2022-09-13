using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace _PROJECT.Scripts.Mechanisms
{
    public class DoorMechanism : MonoBehaviour
    {
        [SerializeField] private ObjectInteractableWithDoor[] confirmedObjects;
        
        [Tooltip("Wait time between starting an opening animation and starting a closing animation.")]
        [SerializeField] private float waitTimeBetweenOpeningAndClosing = 3f;

        private static readonly int OpeningTheDoorHash = Animator.StringToHash("OpeningTheDoor");
        private static readonly int ClosingTheDoorHash = Animator.StringToHash("ClosingTheDoor");

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
            _animator.Play(OpeningTheDoorHash);
            yield return new WaitForSecondsRealtime(waitTimeBetweenOpeningAndClosing);
            _animator.Play(ClosingTheDoorHash);
        }
    }

    public enum ObjectInteractableWithDoor
    {
        Player,
        Enemy
    }
}
