using UnityEngine;

namespace MiciomaXD
{
    public class EnemyBullet : GenericBullet
    {
        private void OnCollisionEnter(Collision collision)
        {
            SpawnImpactFX(collision);

            GameObject collidedWith = collision.gameObject;

            if (collidedWith.CompareTag("Player") && collidedWith.TryGetComponent<PlayerState>(out PlayerState player))
            {
                Debug.Log("Player hit by enemy bullet!");
                //is player and has PlayerState component
                player.TakeDamage(damage);
            }
        }
    }
}