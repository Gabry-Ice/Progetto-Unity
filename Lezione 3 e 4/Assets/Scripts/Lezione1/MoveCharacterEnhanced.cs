using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacterEnhanced : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Camera cam;
    private Vector2 moveInput;
    private Vector3 velocity = Vector3.zero;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log($"Move input: {moveInput}");
    }

    void Update()
    {
        Vector3 camForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized;
        Vector3 inputDir = (camRight * moveInput.x + camForward * moveInput.y);

        // Smooth movement
        Vector3 targetVelocity = inputDir * moveSpeed;
        velocity = Vector3.Lerp(velocity, targetVelocity, rotationSpeed * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;

        // Smooth rotation — don't rotate when moving backward
        if (inputDir.sqrMagnitude > 0.01f && moveInput.y >= 0f)
        {
            Quaternion toRotation = Quaternion.LookRotation(inputDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}