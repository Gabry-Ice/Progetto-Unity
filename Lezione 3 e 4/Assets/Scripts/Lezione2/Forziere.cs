using UnityEngine;
using UnityEngine.InputSystem;

public class Forziere : MonoBehaviour {

    // Reference to the player or input action for interaction
    [SerializeField] private InputActionReference interactAction;

    // Prefabs for loot
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject diamondPrefab;

    // Loot spawn settings
    [SerializeField] private int minCoins = 1;
    [SerializeField] private int maxCoins = 5;
    [SerializeField] private int minDiamonds = 0;
    [SerializeField] private int maxDiamonds = 2;

    // Spawn area settings
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(2f, 1f, 2f);

    private bool isOpen = false;

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {

            if (interactAction != null) {
                interactAction.action.Enable();
                interactAction.action.performed += OnInteract; // Iscrive il metodo OnInteract all'evento performed: serve per chiamare OnInteract quando l'azione viene eseguita
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        // Disabilita l'azione e rimuove il metodo OnInteract dall'evento performed
        if (other.CompareTag("Player")) {
            if (interactAction != null) {
                interactAction.action.Disable();
                interactAction.action.performed -= OnInteract; // Rimuove il metodo OnInteract dall'evento performed: serve per non chiamare più OnInteract quando l'azione viene eseguita
            }
        }
    }

    private void OnInteract(InputAction.CallbackContext context) {
        if (!isOpen) {
            OpenForziere();
        }
    }

    private void OpenForziere() {
        isOpen = true;

        Debug.Log("Forziere aperto");

        SpawnLoot(); // Prima di distruggere il forziere, genero il bottino

        Destroy(gameObject);

        // Nella lezione 3 vedrete come animare e come aggiungere effetti sonori
        // Per esempio:
        // animator.SetTrigger("Open");
        // PlayOpenSound();
    }

    private void SpawnLoot() {

        int coinCount = Random.Range(minCoins, maxCoins + 1); // quantità casuale da un minimo a un massimo, +1 serve per ottenere massimo compreso
        int diamondCount = Random.Range(minDiamonds, maxDiamonds + 1);

        // Spawn monete
        for (int i = 0; i < coinCount; i++) {
            SpawnLootItem(coinPrefab);
        }

        // Spawn diamanti
        for (int i = 0; i < diamondCount; i++) {
            SpawnLootItem(diamondPrefab);
        }
    }

    private void SpawnLootItem(GameObject prefab) {

        Vector3 spawnPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
            spawnAreaSize.y / 2f,
            Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
        );

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }


    public void ResetForziere() {
        isOpen = false;
        Debug.Log("Forziere chiuso");
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, spawnAreaSize.y / 2f, 0), spawnAreaSize);
    }
}