using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int recoverHealth = 1;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

        IsometricController player = collider.transform.GetComponent<IsometricController>();

        if (player != null)
        {
           player.TakeHealth(recoverHealth);
        }
    }

 
    


}
