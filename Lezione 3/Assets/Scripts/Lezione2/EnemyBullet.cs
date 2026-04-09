using UnityEngine;

namespace Lesson2 {

    public class EnemyBullet : MonoBehaviour {

        public int damage = 2;
        public GameObject explosionFX;

        private void OnCollisionEnter(Collision other) {
            GameObject effect = Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(effect, 1);


            if (other.gameObject.CompareTag("Player")) {

                Debug.Log("Player colpito.");

            }

            Destroy(gameObject, 2f);
        }

        // Distrugge il proiettile dopo 3 secondi se non collide con nulla per evitare problemi di memoria
        private void Start() {
            Destroy(gameObject, 3f);
        }

    }

}