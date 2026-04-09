using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacter : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private Vector2 moveInput;
    private Vector3 velocity = Vector3.zero;

    public void OnMove(InputValue value)
    {
        // Read the 2D movement input
        moveInput = value.Get<Vector2>();
        // Debug
        //Debug.Log($"Move input: {moveInput}");
    }

    void Update()
    {
        // Simple movement
        //Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
        //transform.position += movement * Time.deltaTime;

        // Smooth movement
        Vector3 targetVelocity = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
        velocity = Vector3.Lerp(velocity, targetVelocity, rotationSpeed * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;

        // Simple rotation
        //if (movement != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(movement, Vector3.up);
        //}

        // Smooth rotation
        if (velocity != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(velocity, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}