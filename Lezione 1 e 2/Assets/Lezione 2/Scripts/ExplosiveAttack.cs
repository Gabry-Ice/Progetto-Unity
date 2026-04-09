using UnityEngine;
using UnityEngine.InputSystem;
namespace Lesson2 {
    public class ExplosiveAttack : MonoBehaviour {

        public float explosionRadius = 5f;

        public float explosionForce = 800f;

        private Transform playerTransform;

        private void Start() {
            playerTransform = GetComponent<Transform>();
        }

        public void Attack(InputAction.CallbackContext context) {

            if (context.performed) {
                Vector3 explosionCenter = playerTransform.position;

                ApplyExplosionForce(explosionCenter);
            }
        }

        void ApplyExplosionForce(Vector3 explosionCenter) {
            Collider[] colliders = Physics.OverlapSphere(explosionCenter, explosionRadius);

            foreach (Collider collider in colliders) {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null) {
                    rb.AddExplosionForce(explosionForce, explosionCenter, explosionRadius);
                }
            }
        }

        private void OnDrawGizmos() {
            if (playerTransform) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(playerTransform.position, explosionRadius);
            }
        }
    }
}