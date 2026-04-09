using MiciomaXD;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MiciomaXD
{
    [RequireComponent(typeof(PlayerState))]
    public class ShootingEnhanced : MonoBehaviour
    {

        public GameObject bulletPrefab;
        public Transform shootingPoint;
        [SerializeField] Gun gun;
        [SerializeField] PlayerState playerState;

        public float bulletForce = 20f;

        private void Awake()
        {
            if (playerState == null)
                playerState = GetComponent<PlayerState>();
        }

        public void Shoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {

                GameObject playerBullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
                playerBullet.GetComponent<PlayerBullet>().SetPlayerState(playerState);

                Rigidbody bulletRb = playerBullet.GetComponent<Rigidbody>();

                if (bulletRb != null)
                {

                    bulletRb.AddForce(shootingPoint.up * bulletForce, ForceMode.VelocityChange);
                    gun.PlayShootSFX();
                }

            }

        }
    }
}