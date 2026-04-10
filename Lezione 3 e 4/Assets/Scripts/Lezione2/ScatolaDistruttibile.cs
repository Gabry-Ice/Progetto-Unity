using UnityEngine;

namespace Lesson2 {
    public class ScatolaDistruttibile : MonoBehaviour {

        public GameObject effettoParticellare;

        private void OnCollisionEnter(Collision collision) {

            if (collision.gameObject.CompareTag("Bomb")) {

                if (effettoParticellare != null) {
                    Instantiate(effettoParticellare, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
    }
}
