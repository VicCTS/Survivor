using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemyAttackPlayer : MonoBehaviour
{
    enum State
    {
        Chasing,
        Attacking
    }

    [SerializeField]State currentState;

    NavMeshAgent enemyAgent;
    Transform playerTransform;

    [SerializeField]private int _health =5;

    [SerializeField] float attackRange = 5;
    [SerializeField] float attackAngle = 90;
    [SerializeField] int attackDamage = 1;
    float attackTime;
    float attackWait = 1;
    bool canAttack = true;

    IsometricController player;
    // Start is called before the first frame update
    void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = playerTransform.GetComponent<IsometricController>();
    }

    void Start()
    {
        currentState = State.Chasing;
    }

    void Update()
    {
        if(GameManager.instance._gameOver == false)
        {
            switch (currentState) 
            {
                case State.Chasing:
                    Chase();
                break;
                case State.Attacking:
                    Attack();
                break;
            }
        }

    }

    void Chase()
    {
        enemyAgent.destination = playerTransform.position;

        if(OnRange() == true)
        {
            currentState = State.Attacking;
        }
    }

    void Attack()
    {
        if(OnRange() == false)
        {
            currentState = State.Chasing;
        }

        if(canAttack == false)
        {
            attackTime += Time.deltaTime;
            if(attackTime >= attackWait)
            {
                canAttack = true;
                attackTime = 0;
            }
        }

        if(canAttack == true)
        {
            player.TakeDamage(attackDamage);
            canAttack = false;
        }
    }

    bool OnRange()
    {

        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if(distanceToPlayer <= attackRange && angleToPlayer < attackAngle * 0.5f)
        {
            return true;
        }
            return false;
    }

    private void Death()
    {
        //anim.SetBool("is dead", true);

        GameManager.instance.EnemyDestroyed();

        Destroy(this.gameObject);
        /*int randomObject = Random.Range(0, 20);
        Debug.Log(randomObject);

        if(randomObject <= randomObjects.Length -1)
        {
            Instantiate(randomObjects[randomObject], transform.position, transform.rotation);
        }*/        
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
                
            Death();

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.red;
        Vector3 fovLine1 = Quaternion.AngleAxis(attackAngle * 0.5f, transform.up) * transform.forward * attackRange;
        Vector3 fovLine2= Quaternion.AngleAxis(-attackAngle * 0.5f, transform.up) * transform.forward * attackRange;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);
    }

}
