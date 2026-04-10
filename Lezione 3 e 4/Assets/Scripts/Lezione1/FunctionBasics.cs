using UnityEngine;

public class UnityLifecycleMethods : MonoBehaviour
{
    // Variable to track total method calls
    private int methodCallCount = 0;

    // Awake is called when the script instance is being loaded
    // FIRST method called in the script lifecycle
    // Use Awake for initial setup and initialization
    private void Awake()
    {
        methodCallCount++;
        Debug.Log($"[Awake] Call #{methodCallCount}");
    }

    // Start is called before the first frame update
    // Use Start to prepare game state and components
    void Start()
    {
        methodCallCount++;
        Debug.Log($"[Start] Call #{methodCallCount}");
    }

    // Update is called once per frame
    // Use Update for game logic and input handling
    void Update()
    {
        methodCallCount++;

        // Only log periodically to prevent console spam
        if (methodCallCount % 100 == 0)
        {
            Debug.Log($"[Update] Call #{methodCallCount}");
        }
    }
}