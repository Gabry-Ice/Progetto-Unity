using UnityEngine;

public class collezionaMoneta : MonoBehaviour
{
    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            Destroy(gameObject);
        }
    }
}
