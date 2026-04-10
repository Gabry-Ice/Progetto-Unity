using UnityEngine;

namespace Lesson2 {

    public class SimpleBullet : MonoBehaviour {

        public int damage = 35;

        // Distrugge il proiettile dopo 3 secondi se non collide con nulla per evitare problemi di memoria
        // private void Start() {
        //     Destroy(gameObject, 3f);
        // }

        // private void OnCollisionEnter(Collision other) {
        //     Destroy(gameObject);
        // }

    }

}