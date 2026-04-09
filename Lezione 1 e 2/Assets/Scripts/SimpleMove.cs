using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMove : MonoBehaviour
{
    public Vector2 moveInput;

    //velocità
    public float moveSpeed = 2f;
    //velocità di rotazione
    public float rotationSpeed = 10f;
    //smooth velocity
    private Vector3 velocity = Vector3.zero;

    //OnMove viene chiamata quando do un input
    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }
    
    private void Update()
    {
        //logica di movimento semplice
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
        transform.position += movement * Time.deltaTime;

        //movimento smooth(morbido)
        Vector3 targetVelocity = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
        velocity = Vector3.Lerp(velocity, targetVelocity, rotationSpeed * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;


        //smooth rotation
        if(movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(velocity, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        //simple rotationSpeed
        /*
        if(movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement, Vector3.up);
        }
        */
    }
}
