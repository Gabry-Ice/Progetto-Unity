using UnityEngine;
using System.Collections.Generic;   

public class fuoco : MonoBehaviour

{
    public List<GameObject> gestoreFuoco;
    public string PlayerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            foreach (GameObject fuoco in gestoreFuoco)
            {
                fuoco.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            foreach (GameObject fuoco in gestoreFuoco)
            {
                fuoco.SetActive(true);
            }
        }
    }
}
