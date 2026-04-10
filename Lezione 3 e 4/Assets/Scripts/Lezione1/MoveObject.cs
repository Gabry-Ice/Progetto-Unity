using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.forward;
    public float moveSpeed = 5f;

    void Update()
    {
        // Move the object in the specified direction at the defined speed (meters per second)
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
