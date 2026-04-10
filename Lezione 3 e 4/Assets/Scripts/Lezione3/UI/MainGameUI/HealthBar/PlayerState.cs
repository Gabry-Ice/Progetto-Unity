using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace MiciomaXD
{
    /// <summary>
    /// Represents the state and health management of a player, including tracking current and total health and updating
    /// the associated health bar.
    /// In a real game scenario, this class would likely be expanded as a data container (no functions to edit the player state). Other "player state editor" classes should be responsible for editing the player state and keeping domain consistency, but for simplicity, this class is also able to edit the player state.
    /// In other words, in a more complex game, you might want to separate concerns by having a dedicated PlayerStateManager or similar class that handles the logic of modifying the player's health, while PlayerState would simply hold the data and provide methods for querying it.
    /// </summary>
    /// <remarks>This class provides methods to apply damage and healing to the player, ensuring that health
    /// values remain within valid bounds. The health bar is automatically updated to reflect changes in the player's
    /// health. The class requires a reference to a HealthBar component, which is resolved at runtime if not
    /// assigned.</remarks>
    public class PlayerState : MonoBehaviour
    {
        [Header("Health Related")]
        public float totalHP;
        public float curHP;
        public UnityEvent onHealthDepleted;
        public HealthBar healthBar;

        [Header("Enemies Related")]
        public int enemiesKilled;
        public int enemiesAvailable;
        public UnityEvent onAllEnemiesKilled;

        private void Awake()
        {
            if (healthBar == null)
                healthBar = FindFirstObjectByType<HealthBar>();

            curHP = totalHP;

            enemiesAvailable = FindObjectsByType<EnemyEnhanced>(FindObjectsSortMode.None).Length;
        }

        public void UpdateKillCount()
        {
            enemiesKilled++;
            if (AreEnemiesDead())
                onAllEnemiesKilled.Invoke();
        }

        public void TakeDamage(float damage)
        {
            curHP -= damage;
            curHP = Mathf.Clamp(curHP, 0, totalHP);

            if (curHP <= 0)
                onHealthDepleted.Invoke();

            float fillvalue = Mathf.InverseLerp(0, totalHP, curHP);
            healthBar.UpdateHealthBarDisplay(fillvalue);
        }

        public void Heal(float heal)
        {
            TakeDamage(-heal);
        }

        public bool AreEnemiesDead() => enemiesKilled >= enemiesAvailable;
    }
}