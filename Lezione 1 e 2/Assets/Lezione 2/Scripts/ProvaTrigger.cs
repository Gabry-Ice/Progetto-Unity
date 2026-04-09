using UnityEngine;

public class Trigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Entered trigger" + other.name);
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Exited trigger " + other.name);
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("Staying in trigger");
    }
}
