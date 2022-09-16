using UnityEngine;

namespace _PROJECT.Scripts.Locomotion.Mechanisms
{
    public class TeleportIn : MonoBehaviour
    {
        [SerializeField] private TeleportOut teleportOut;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Teleport(other.transform);
        }

        private void Teleport(Transform player)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.position = teleportOut.transform.position;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
