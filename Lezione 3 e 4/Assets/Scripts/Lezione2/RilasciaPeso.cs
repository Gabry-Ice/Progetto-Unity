using UnityEngine;
namespace Lesson2 {
    public class RilasciaPeso : MonoBehaviour {

        public Rigidbody peso;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                peso.useGravity = true;
            }
        }
    }
}