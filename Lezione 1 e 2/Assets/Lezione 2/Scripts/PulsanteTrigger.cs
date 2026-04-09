using UnityEngine;

public class PulsanteTrigger : MonoBehaviour
{
	public GameObject porta;
	
	public void OnTriggerEnter(Collider other){
		if (other.CompareTag("Player")){
            Debug.Log("Pulsante Premuto");
			Destroy(porta);
        }
	}
}
