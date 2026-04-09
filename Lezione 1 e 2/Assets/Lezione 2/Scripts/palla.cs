using UnityEngine;

public class palla : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Box")){
            Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
    }
}
