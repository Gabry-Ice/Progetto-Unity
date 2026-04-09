using UnityEngine;

public class acquaTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            Debug.Log("Game Over");
        }   
    }
}