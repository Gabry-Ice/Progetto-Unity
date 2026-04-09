using UnityEngine;

public class bottone : MonoBehaviour
{
    public Rigidbody palla;

    public void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            
            Debug.Log("Pulsante Premuto");
            palla.useGravity = true;
        }
    }
}
