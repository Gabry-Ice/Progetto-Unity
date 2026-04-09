using UnityEngine;

public class FunctionBasics : MonoBehaviour
{
    // Variable to track total method calls
    public int countCall = 0;

    // Awake is called when the script instance is being loaded
    // FIRST method called in the script lifecycle
    // Use Awake for initial setup and initialization
    private void Awake()
    {
        countCall++;
        Debug.Log($"[Awake] Call #{countCall}");
    }

    // Start is called before the first frame update
    // Use Start to prepare game state and components
    void Start()
    {
        countCall++;
        Debug.Log($"[Start] Call #{countCall}");
    }

    // Update is called once per frame
    // Use Update for game logic and input handling
    void Update()
    {
        countCall++;

        // Only log periodically to prevent console spam
        if (countCall % 100 == 0)
        {
            Debug.Log($"[Update] Call #{countCall}");
        }
    }
}