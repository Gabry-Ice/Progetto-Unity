using UnityEngine;

public class FunctionBasics : MonoBehaviour
{
    // Variable to track total method calls
    public int callCount = 0; //posso vedere il valore sull'editore di unity'

    // Awake is called when the script instance is being loaded
    // FIRST method called in the script lifecycle
    // Use Awake for initial setup and initialization
    private void Awake()
    {
        callCount++;
        Debug.Log($"[Awake] Call #{callCount}");
    }

    // Start is called before the first frame update
    // Use Start to prepare game state and components
    void Start()
    {
        callCount++;
        Debug.Log($"[Start] Call #{callCount}");
    }

    // Update is called once per frame
    // Use Update for game logic and input handling
    void Update()
    {
        callCount++;

        // Only log periodically to prevent console spam
        if (callCount % 100 == 0)
        {
            Debug.Log($"[Update] Call #{callCount}");
        }
    }
}