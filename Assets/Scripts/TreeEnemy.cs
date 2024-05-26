using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TreeEnemy : MonoBehaviour
{
    enum State
    {
        Rushing,
        Dead,
        Attacking
    }

    State _currentState;

    NavMeshAgent _treeEnemyAgent;

    Animator _animator;

    ParticlesActivator _particles;

    [SerializeField]private int _health = 5;

    public int _enemyDamage = 2; 
    [SerializeField]GameObject[] _treePosition;
    [SerializeField]List<GameObject> _treeList;
    [SerializeField]TreeHealth[] _treeScripts;
    int _nearestTreeIndex;
    [SerializeField]TreeHealth _treeTarget; 
    [SerializeField] float[] distances;

    [SerializeField] float _attackRange = 5;
    
    [SerializeField] private float waitTime = 2.0f;
    [SerializeField] private float timer = 0.0f;

    public Slider hpSlipder;

    void Awake()
    {
        _treeEnemyAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _particles = GetComponent<ParticlesActivator>();
    }

    void Start()
    {
        SetTrees();
        _currentState = State.Rushing;

        hpSlipder.maxValue = _health;
        hpSlipder.value = hpSlipder.maxValue;

        _animator.SetBool("isWalking", true);
    }

    void Update()
    {
        switch (_currentState)
        {
            case State.Rushing:
                Rush();
            break;
            case State.Attacking:
                Attack();
            break;
        }
    }

    void SetTrees()
    {
        _treeList.Clear();

        _treePosition = GameObject.FindGameObjectsWithTag("Arbol");

        _treeScripts = new TreeHealth[_treePosition.Length];

        for (int i = 0; i < _treePosition.Length; i++)
        {
            _treeScripts[i] = _treePosition[i].GetComponent<TreeHealth>();
        }

        for (int i = 0; i < _treePosition.Length; i++)
        {
            if(_treeScripts[i] != null)
            {
                _treeList.Add(_treePosition[i]);
            }
        }

        distances = new float[_treeList.Count];
        

        for (int i = 0; i < _treeList.Count; i++)
        {
            if(_treePosition[i] != null)
            {
                distances[i] = Vector3.Distance(transform.position, _treeList[i].transform.position);
            }
            
        } 

        _nearestTreeIndex = GetNearestTree();
        _treeTarget = _treeList[_nearestTreeIndex].GetComponent<TreeHealth>();

        /*for (int i = 0; i < _treePosition.Length; i++)
        {
            if( _treeScripts[i] == null)
            {
                _treePosition[i] = null;
            }
        }*/

        /*distances = new float[_treePosition.Length];

        for (int i = 0; i < _treePosition.Length; i++)
        {
            if(_treePosition[i] != null)
            {
                distances[i] = Vector3.Distance(transform.position, _treePosition[i].transform.position);
            }
            
        } 
        _nearestTreeIndex = GetNearestTree();
        _treeTarget = _treePosition[_nearestTreeIndex].GetComponent<TreeHealth>();*/
    }

    bool OnRange()
    {
        /*if(Vector3.Distance(transform.position, _treePosition[_nearestTreeIndex].transform.position) <= _attackRange) 
        {
            return true;
        }
        return false;*/

        if(Vector3.Distance(transform.position, _treeList[_nearestTreeIndex].transform.position) <= _attackRange) 
        {
            return true;
        }
        return false;

    }

    void Rush()
    {
        if (_treeTarget == null)
        {
            SetTrees();
            GetNearestTree();
        }

        //_treeEnemyAgent.destination = _treePosition[_nearestTreeIndex].transform.position;

        _treeEnemyAgent.destination = _treeList[_nearestTreeIndex].transform.position;

        if(OnRange() == true)
        {
            _treeEnemyAgent.isStopped = true;
            _currentState = State.Attacking;

            _animator.SetBool("isWalking", false);
        }
    }

    void Attack()
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            timer = 0;

            _animator.SetTrigger("isAttacking");

            _treeTarget.TakeDamage(_enemyDamage);
            SoundManager.instance.PlaySound(SoundManager.instance.golpeArbol);
        }

        if (_treeTarget == null)
        {
            SetTrees();
            GetNearestTree();
            _treeEnemyAgent.isStopped = false;
            _currentState = State.Rushing;

            _animator.SetBool("isWalking", true);
        }
    }

    int GetNearestTree()
    {
        float _lowestValue = float.PositiveInfinity;
        int _index = 0;

        for (int i = 0; i < distances.Length; i++)
        {
            if(distances[i] < _lowestValue)
            {
                _lowestValue = distances[i];
                _index = i;
            }
        }
        return _index;
    }

    private void Death()
    {
        _animator.SetTrigger("Die");
        
        
        SoundManager.instance.PlaySound(SoundManager.instance.muerteEnemigo);
        GameManager.instance.EnemyDestroyed();

        Destroy(this.gameObject, 2f);
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

        hpSlipder.value = _health;

        _particles.ActivateParticles();

        if(_health <= 0)
        {
            _currentState = State.Dead;

            _treeEnemyAgent.isStopped = true;
                
            Death();

        }
    }
}