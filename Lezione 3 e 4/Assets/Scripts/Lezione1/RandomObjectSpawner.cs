using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("Prefab to spawn")]
    public GameObject objectToSpawn;

    [Tooltip("Plane on which objects will be spawned")]
    public GameObject spawnPlane;

    [Tooltip("Maximum number of objects to spawn")]
    [Range(1, 100)]
    public int maxSpawnCount = 10;

    [Tooltip("Minimum time between spawns")]
    public float minSpawnInterval = 1f;

    [Tooltip("Maximum time between spawns")]
    public float maxSpawnInterval = 3f;

    [Tooltip("Fixed scale for spawned objects")]
    public float spawnScale = 1f;

    private int currentSpawnCount = 0;
    private float nextSpawnTime;

    private void Start()
    {
        // Validate references
        if (objectToSpawn == null)
        {
            Debug.LogError("Object to spawn is not assigned!");
            enabled = false;
            return;
        }

        if (spawnPlane == null)
        {
            Debug.LogError("Spawn plane is not assigned!");
            enabled = false;
            return;
        }

        // Initialize first spawn time
        SetNextSpawnTime();
    }

    private void Update()
    {
        // Check if we can spawn more objects
        if (currentSpawnCount < maxSpawnCount && Time.time >= nextSpawnTime)
        {
            SpawnObject();
            SetNextSpawnTime();
        }
    }

    private void SpawnObject()
    {
        // Get the renderer of the spawn plane to determine its bounds
        Renderer planeRenderer = spawnPlane.GetComponent<Renderer>();

        if (planeRenderer == null)
        {
            Debug.LogError("Spawn plane must have a Renderer component!");
            return;
        }

        // Calculate random position within the plane's bounds
        float randomX = Random.Range(
            planeRenderer.bounds.min.x,
            planeRenderer.bounds.max.x
        );
        float randomZ = Random.Range(
            planeRenderer.bounds.min.z,
            planeRenderer.bounds.max.z
        );

        // Use the plane's Y position plus a small offset to prevent clipping
        Vector3 spawnPosition = new Vector3(
            randomX,
            spawnPlane.transform.position.y + 0.1f,
            randomZ
        );

        // Instantiate the object as a child of this GameObject
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity, transform);

        // Apply fixed scale
        spawnedObject.transform.localScale = Vector3.one * spawnScale;

        // Increment spawn count
        currentSpawnCount++;
    }

    private void SetNextSpawnTime()
    {
        // Set the next spawn time with some randomness
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // Optional method to reset spawner
    public void ResetSpawner()
    {
        currentSpawnCount = 0;
        SetNextSpawnTime();
    }
}