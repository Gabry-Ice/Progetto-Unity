using UnityEngine;
using UnityEngine.InputSystem;

// Questa soluzione non permette di gestire le collisioni del personaggio

public class PlayerControllerSimple : MonoBehaviour {
    public float moveSpeed = 5f;
    private Vector2 moveInput;

    public void OnMove(InputValue input) {
        moveInput = input.Get<Vector2>();
    }

    private void Update() {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }
}
