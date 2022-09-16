using UnityEngine;

namespace _PROJECT.Scripts.Pickables
{
    public class PipPickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            print("Pip picked up.");
            Destroy(gameObject);
        }
    }
}
