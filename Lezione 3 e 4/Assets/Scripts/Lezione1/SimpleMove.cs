using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMove : MonoBehaviour
{
    // Input letto da WASD ad esempio
    public Vector2 moveInput;

    // Smooth moveWithVelocity
    public Vector3 moveWithVelocity = Vector3.zero;

    // Velocità di rotazione
    public float rotationSpeed = 10f;

    // Velocità
    public float moveSpeed = 2f;

    // OnMove viene chiamata quando do un input
    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }

    private void Update()
    {
        // Smooth movement
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
        moveWithVelocity = Vector3.Lerp(moveWithVelocity, movement, rotationSpeed * Time.deltaTime);
        transform.position += moveWithVelocity * Time.deltaTime;

        // Simple rotation
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
