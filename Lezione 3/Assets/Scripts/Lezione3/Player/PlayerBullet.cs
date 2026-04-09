using UnityEngine;

namespace MiciomaXD
{
    public class PlayerBullet : GenericBullet
    {
        [SerializeField] GameObject hitMarker;
        PlayerState state;

        private void OnCollisionEnter(Collision collision)
        {
            SpawnImpactFX(collision);

            GameObject collidedWith = collision.gameObject;

            if (collidedWith.CompareTag("Enemy") && collidedWith.TryGetComponent(out EnemyEnhanced enemy))
            {
                //is enemy and has EnemyEnhanced component
                if (enemy.TakeDamage(damage) <= 0)
                {
                    state.UpdateKillCount();
                }

                GameObject hitDmgDisplay = Instantiate(hitMarker, collision.GetContact(0).point, Quaternion.identity);
                hitDmgDisplay.GetComponentInChildren<DamageVisualizer>().SetDamageNumber(damage);
            }
        }

        public void SetPlayerState(PlayerState state) => this.state = state;
    }
}