using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.zero; //(0.0, 0.0, 0.0)
    public float moveSpeed = 5.0f;

    private void Awake()
    {
        // Inizializziamo la direzione
        moveDirection = transform.forward;
    }

    void Update()
    {
        // Move the object in the specified direction at the defined speed (meters per second)
        //transform.position += moveDirection * moveSpeed * Time.deltaTime;
        Vector3 movement = moveDirection * moveSpeed;
        transform.position += movement * Time.deltaTime;
    }
}
