using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeHealth : MonoBehaviour
{
    [SerializeField]private float treeHealth = 10;
    [SerializeField]private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue=treeHealth;
    }

    // Update is called once per frame

    void Update()
    {
        if(Input.GetButtonDown("Jump")) 
        {
          TakeDamage(5);
        }  
    }

    void TakeDamage(int damage)
    {
         if(treeHealth > 0) 
        {
           treeHealth -= damage; 
           healthSlider.value -=damage;
        }  
        else
        {
            Destroy(gameObject);

        }
    }
}
