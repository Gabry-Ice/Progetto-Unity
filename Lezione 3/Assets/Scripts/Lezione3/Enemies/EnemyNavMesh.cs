using MiciomaXD;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace MiciomaXD
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyNavMesh : MonoBehaviour
    {
        [Header("Proprietà Nemico")]
        public int health = 100;
        public int scoreForKilling = 100;

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
        ////////////////////
        [SerializeField] Gun Gun;
        [SerializeField] Score score;
        [SerializeField] NavMeshAgent agent;
        ////////////////////

        private float timeSinceLastShot = 0f;
        private Vector3 randomPatrolTarget;

        private Transform myTransform;
        private Transform player;

        private GameObject playerGO;

        private enum State
        {
            Patrolling,
            Chasing,
            Shooting
        }

        private State currentState = State.Patrolling;

        private void Awake()
        {
            if (agent == null)
                agent = GetComponent<NavMeshAgent>();

            myTransform = GetComponent<Transform>();
            playerGO = GameObject.FindGameObjectWithTag("Player");
            player = playerGO.transform;
            GetRandomPatrolTarget();

            if (score == null)
                score = FindFirstObjectByType<Score>();
        }

        private void Update()
        {
            if (player != null)
            {
                float distanceToPlayer = Vector3.Distance(myTransform.position, player.position);

                switch (currentState)
                {
                    case State.Patrolling:
                        HandlePatrol();

                        if (distanceToPlayer <= detectionRadius)
                        {
                            currentState = State.Chasing;
                            agent.ResetPath(); //stop patrol movement immediately
                        }
                        break;

                    case State.Chasing:
                        HandleChase(distanceToPlayer);
                        break;

                    case State.Shooting:
                        HandleShooting();

                        if (distanceToPlayer > shootingRange)
                        {
                            currentState = State.Chasing;
                        }
                        break;
                }
            }
        }

        private void HandlePatrol()
        {
            if (!agent.hasPath) //first time, set destination
            {
                randomPatrolTarget = GetRandomPatrolTarget();
                agent.SetDestination(randomPatrolTarget);
            }
            else if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                randomPatrolTarget = GetRandomPatrolTarget();
                agent.SetDestination(randomPatrolTarget);
            }
            // else is moving to destination
        }

        private void HandleChase(float distanceToPlayer)
        {
            agent.SetDestination(player.position);
            if (distanceToPlayer > detectionRadius)
            {
                currentState = State.Patrolling;
            }
            else if (distanceToPlayer <= shootingRange)
            {
                agent.ResetPath(); //stop chasing movement immediately
                currentState = State.Shooting;
            }
        }

        private void HandleShooting()
        {
            RotateTowards(player.position);

            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= shootingInterval)
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }

        private void RotateTowards(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - myTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        Vector3 GetRandomPatrolTarget()
        {
            int maxAttempts = 10;

            for (int i = 0; i < maxAttempts; i++)
            {
                Vector2 dir = Random.insideUnitCircle.normalized;

                float distance = Random.Range(patrolRadius * 0.3f, patrolRadius); //min 30% of patrol radius to avoid too close targets
                Vector3 candidate = myTransform.position + new Vector3(dir.x, 0, dir.y) * distance;

                if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }

            return myTransform.position;
        }


        private void Shoot()
        {
            if (bulletPrefab != null && shootingPoint != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                if (bulletRb != null)
                {
                    bulletRb.AddForce(shootingPoint.up * bulletForce, ForceMode.VelocityChange);
                    ///////////////////////
                    Gun.PlayShootSFX();
                    ///////////////////////
                }
            }
        }

        /// <summary>
        /// Returns health left.
        /// </summary>
        /// <param name="damage"></param>
        public int TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                KilledByPlayer();
            }

            return health;
        }

        void KilledByPlayer()
        {
            score.AddScore(scoreForKilling);
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            if (myTransform)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(myTransform.position, detectionRadius);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(myTransform.position, shootingRange);
            }
        }
    }
}