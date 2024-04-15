using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody _rBody;
    public float _bulletSpeed = 100;
    public int shootDamage;

    void Start()
    {
        _rBody = GetComponent<Rigidbody>();
        _rBody.AddForce(transform.right * _bulletSpeed, ForceMode.Impulse);
        shootDamage = Global.playerDamage;

    }

    void OnTriggerEnter(Collider collider)
    {
        /*if(collider.gameObject.tag != "Player"  && collider.gameObject.tag != "Bullet" && collider.gameObject.layer != 7)
        {
            Destroy(this.gameObject);
        }*/

        TreeEnemy treeEnemy = collider.transform.GetComponent<TreeEnemy>();

        if(treeEnemy != null)
        {
            treeEnemy.TakeDamage(shootDamage);
            Destroy(this.gameObject);
        }

        IAEnemyAttackPlayer enemy = collider.transform.GetComponent<IAEnemyAttackPlayer>();

        if(enemy != null)
        {
            enemy.TakeDamage(shootDamage);
            Destroy(this.gameObject);
        }

    }
}
