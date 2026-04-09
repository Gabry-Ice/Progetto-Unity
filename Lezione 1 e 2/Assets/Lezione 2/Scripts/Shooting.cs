using UnityEngine;
using UnityEngine.InputSystem;
namespace Lesson2 {
    public class Shooting : MonoBehaviour {

        public GameObject bulletPrefab;
        public Transform shootingPoint;

        public float bulletForce = 20f;


        public void Shoot(InputAction.CallbackContext context) {
            if (context.performed) {

                GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);


                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                if (bulletRb != null) {

                    bulletRb.AddForce(shootingPoint.up * bulletForce, ForceMode.VelocityChange);
                }

            }

        }
    }
}