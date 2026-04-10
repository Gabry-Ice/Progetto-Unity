using UnityEngine;

namespace Lesson2 {
    public class Bomba : MonoBehaviour {

        public GameObject effettoParticellare;

        private void OnCollisionEnter(Collision collision) {

            if (collision.gameObject.CompareTag("Box")) {

                if (effettoParticellare != null) {
                    Instantiate(effettoParticellare, transform.position, Quaternion.identity);
                }

                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
