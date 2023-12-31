using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody _rBody;
    public float _bulletSpeed = 100;
    public int shootDamage = 2;

    void Start()
    {
        _rBody = GetComponent<Rigidbody>();

        _rBody.AddForce(transform.right * _bulletSpeed, ForceMode.Impulse);

    }

    void OnTriggerEnter(Collider collider)
    {
         if(collider.gameObject.tag != "Player"  && collider.gameObject.tag != "Bullet" && collider.gameObject.layer != 7)
        {
            Destroy(this.gameObject);
        }

        Enemy enemy = collider.transform.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(shootDamage);
        }

    }
}
