using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.zero; //(0.0, 0.0, 0.0)
    public float moveSpeed = 5f;

    private void Awake()
    {
        //Inizializziamo la direzione
        moveDirection = transform.forward;
    }

    void Update()
    {
        // Move the object in the specified direction at the defined speed (meters per second)
        //per ogni frame aggiungo alla posizione originale un valore moltiplicato per 5 rispetto anche al numero di cicli effettuati
        //Time.deltaTime serve per normalizzare su ogni dispositivo la velocità dell'oggetto'
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}