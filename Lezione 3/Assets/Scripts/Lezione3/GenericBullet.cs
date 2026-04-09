using Lesson2;
using UnityEngine;

namespace MiciomaXD
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class GenericBullet : MonoBehaviour
    {
        public int damage = 20;
        [SerializeField] GameObject onContactVFX;

        private void Awake()
        {
            Destroy(gameObject, 10f); //To be safe the bullet will be destroyed after 10 seconds if it doesn't collide with anything
        }

        protected void SpawnImpactFX(Collision collision)
        {
            GameObject effect = Instantiate(onContactVFX, collision.GetContact(0).point, Quaternion.identity);
            Destroy(effect, 1);

            Destroy(gameObject);
        }
    }
}