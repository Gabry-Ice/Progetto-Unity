using UnityEngine;
namespace Lesson2 {
    public class SimpleEnemy : MonoBehaviour {

        public int health = 100;

        public Transform player;

        [Header("Enemy Properties")]
        public float detectionRadius = 20f;
        public float shootingRange = 15f;
        public float shootingInterval = 1f;

        [Header("Shooting Properties")]
        public GameObject bulletPrefab;
        public Transform shootingPoint;
        public float bulletForce = 20;

        private Transform myTransform;

        private float timeSinceLastShot = 0f;  // Timer per il tempo tra un colpo e l'altro

        public void TakeDamage(int damage) {
            health -= damage;
            if (health <= 0) {
                Die();
            }
        }

        void Die() {
            Destroy(gameObject);
        }

        private void Awake() {
            myTransform = GetComponent<Transform>();
        }

        private void Update() {

            if (player != null) {

                float distanceToPlayer = Vector3.Distance(myTransform.position, player.position);

                // Controlla se il giocatore è nel raggio di rilevamento
                if (distanceToPlayer <= detectionRadius) {
                    // Ruota il nemico verso il giocatore
                    Vector3 direction = (player.position - myTransform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth rotation

                    // Controlla se il giocatore è nel range di tiro
                    if (distanceToPlayer <= shootingRange) {
                        timeSinceLastShot += Time.deltaTime;
                        if (timeSinceLastShot >= shootingInterval) {
                            Shoot();
                            timeSinceLastShot = 0f; // Reimposta il timer
                        }
                    }
                }
            }
        }

        void Shoot() {
            if (bulletPrefab != null && shootingPoint != null) {
                GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                if (bulletRb != null) {
                    bulletRb.AddForce(shootingPoint.up * bulletForce, ForceMode.VelocityChange);
                }

            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, shootingRange);
        }
    }
}

