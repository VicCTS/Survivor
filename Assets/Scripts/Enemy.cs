using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]private int _health =5;
    [SerializeField] private GameObject[] randomObjects;

    [SerializeField] GameObject[] treeTransforms;
    [SerializeField] float[] distances;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(randomObjects.Length);

        treeTransforms = GameObject.FindGameObjectsWithTag("Arbol");
        distances = new float[treeTransforms.Length]; 

        for (int i = 0; i < treeTransforms.Length; i++)
        {
            distances[i] = Vector3.Distance(transform.position, treeTransforms[i].transform.position);
        }
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
