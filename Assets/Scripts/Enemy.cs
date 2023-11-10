using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private int _health =5;
    [SerializeField] private GameObject[] randomObjects;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(randomObjects.Length);
    }
    
    private void Death()
    {
        //anim.SetBool("is dead", true);

        Destroy(this.gameObject);
        int randomObject = Random.Range(0, 20);
        Debug.Log(randomObject);

        if(randomObject <= randomObjects.Length -1)
        {
            Instantiate(randomObjects[randomObject], transform.position, transform.rotation);
        }

        
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
                
            Death();

        }
    }
}
