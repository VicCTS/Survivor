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

    State currentState;

    NavMeshAgent enemyAgent;
    Transform playerTransform;

    [SerializeField] float attackRange = 5;
    [SerializeField] float attackAngle = 90;
    [SerializeField] float damage = 1;
    float attackTime;
    float attackWait = 1;
    bool canAttack = true;

    IsometricController player;
    // Start is called before the first frame update
    void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.Find("Player").GetComponent<IsometricController>();
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
            player.TakeDamage(40);
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
