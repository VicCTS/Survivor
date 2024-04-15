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
        healthSlider.maxValue = treeHealth;
        healthSlider.value = treeHealth;
    }

    // Update is called once per frame

    void Update()
    {
        if(Input.GetButtonDown("Jump")) 
        {
          TakeDamage(5);
        }  
    }

    public void TakeDamage(int damage)
    {
        if(treeHealth > 0) 
        {
            Debug.Log("arbol a recibido " + damage + " de daño");
            treeHealth -= damage; 
            healthSlider.value = treeHealth;

            if(treeHealth <= 0)
            {
                GameManager.instance.TreeDestroyed();
                Destroy(gameObject);
            }
        } 
    }
}
