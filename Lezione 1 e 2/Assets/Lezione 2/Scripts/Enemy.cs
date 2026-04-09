using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Lesson2 {
    public class Enemy : MonoBehaviour {
        [Header("Proprietà Nemico")]
        public int health = 100;

        [Header("Proprietà Rilevamento")]
        public float detectionRadius = 30f;
        public float shootingRange = 15f;
        public float shootingInterval = 0.5f;
        public float moveSpeed = 3f;

        [Header("Proprietà Pattugliamento")]
        public float patrolRadius = 5f;
        public float patrolRotationSpeed = 2f;

        [Header("Proprietà Sparo")]
        public GameObject bulletPrefab;
        public Transform shootingPoint;
        public float bulletForce = 20;

        private float timeSinceLastShot = 0f;
        private Vector3 randomPatrolTarget;

        private Transform myTransform;
        private Transform player;

        private GameObject playerGO;

        private enum State {
            Patrolling,
            Chasing,
            Shooting
        }

        private State currentState = State.Patrolling;

        private void Awake() {
            myTransform = GetComponent<Transform>();
            playerGO = GameObject.FindGameObjectWithTag("Player");
            player = playerGO.transform;
            SetRandomPatrolTarget();


        }
        private void Start() {

        }

        private void Update() {
            if (player != null) {
                float distanceToPlayer = Vector3.Distance(myTransform.position, player.position);

                switch (currentState) {
                    case State.Patrolling:
                        HandlePatrol();
                        if (distanceToPlayer <= detectionRadius) {
                            currentState = State.Chasing;
                        }
                        break;

                    case State.Chasing:
                        HandleChase(distanceToPlayer);
                        break;

                    case State.Shooting:
                        HandleShooting();
                        if (distanceToPlayer > shootingRange) {
                            currentState = State.Chasing;
                        }
                        break;
                }
            }
        }

        private void HandlePatrol() {
            if (Vector3.Distance(myTransform.position, randomPatrolTarget) < 1f) {
                SetRandomPatrolTarget();
            }

            MoveTowards(randomPatrolTarget);
            RotateDuringPatrol();
        }

        private void HandleChase(float distanceToPlayer) {
            if (distanceToPlayer > detectionRadius) {
                currentState = State.Patrolling;
            } else if (distanceToPlayer <= shootingRange) {
                currentState = State.Shooting;
            } else {
                // Continue chasing
                MoveTowards(player.position);
                RotateTowards(player.position);
            }
        }

        private void RotateDuringPatrol() {
            Vector3 directionToTarget = (randomPatrolTarget - myTransform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));

            myTransform.rotation = Quaternion.Slerp(
                myTransform.rotation,
                targetRotation,
                Time.deltaTime * patrolRotationSpeed
            );
        }

        private void HandleShooting() {
            RotateTowards(player.position);

            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= shootingInterval) {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }

        private void RotateTowards(Vector3 targetPosition) {
            Vector3 direction = (targetPosition - myTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        private void MoveTowards(Vector3 targetPosition) {
            Vector3 newTarget = new Vector3(targetPosition.x, myTransform.position.y, targetPosition.z);
            myTransform.position = Vector3.MoveTowards(myTransform.position, newTarget, moveSpeed * Time.deltaTime);
        }


        private void SetRandomPatrolTarget() {
            Vector3 randomOffset = Random.insideUnitSphere * patrolRadius;
            randomPatrolTarget = new Vector3(myTransform.position.x + randomOffset.x, myTransform.position.y, myTransform.position.z + randomOffset.z);
        }


        private void Shoot() {
            if (bulletPrefab != null && shootingPoint != null) {
                GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                if (bulletRb != null) {
                    bulletRb.AddForce(shootingPoint.up * bulletForce, ForceMode.VelocityChange);
                }
            }
        }

        public void TakeDamage(int damage) {
            health -= damage;
            if (health <= 0) {
                Die();
            }
        }

        void Die() {
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected() {
            if (myTransform) {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(myTransform.position, detectionRadius);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(myTransform.position, shootingRange);
            }
        }
    }
}