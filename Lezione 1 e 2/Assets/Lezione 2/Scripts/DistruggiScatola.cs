using UnityEngine;
namespace Lesson2 {
    public class DistruggiScatola : MonoBehaviour {
        private void OnCollisionEnter(Collision collision) {

            if (collision.gameObject.CompareTag("Box")) {
                Destroy(collision.gameObject);
            }
            // Destroy(collision.gameObject);
        }

    }
}
