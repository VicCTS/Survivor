using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemyAttackPlayer : MonoBehaviour
{
    enum State
    {
        Chasing,
        Attacking,
        Dead
    }

    [SerializeField]State currentState;

    NavMeshAgent enemyAgent;
    Transform playerTransform;
    Animator _animator;
    ParticlesActivator _particles;

    [SerializeField]private int _health =5;

    [SerializeField] float attackRange = 5;
    [SerializeField] float attackAngle = 90;
    [SerializeField] int attackDamage = 1;
    [SerializeField] float attackTime;
    [SerializeField] float attackWait = 2;
    bool canAttack = true;

    IsometricController player;
    // Start is called before the first frame update
    void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _particles = GetComponent<ParticlesActivator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = playerTransform.GetComponent<IsometricController>();
        
    }

    void Start()
    {
        currentState = State.Chasing;

        _animator.SetBool("isWalking", true);
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
            enemyAgent.isStopped = true;

            currentState = State.Attacking;

            _animator.SetBool("isWalking", false);
        }
    }

    void Attack()
    {
        if(OnRange() == false)
        {
            currentState = State.Chasing;

            _animator.SetBool("isWalking", true);

            enemyAgent.isStopped = false;
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
            _animator.SetTrigger("isAttacking");
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
        _animator.SetTrigger("Die");

        GameManager.instance.EnemyDestroyed();

        Destroy(this.gameObject, 2);

        /*int randomObject = Random.Range(0, 20);
        Debug.Log(randomObject);

        if(randomObject <= randomObjects.Length -1)
        {
            Instantiate(randomObjects[randomObject], transform.position, transform.rotation);
        }*/        
    }

    public void TakeDamage(int damage)
    {
        _particles.ActivateParticles();

        _health -= damage;

        if(_health <= 0)
        {
            currentState = State.Dead;

            enemyAgent.isStopped = true;
                
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
