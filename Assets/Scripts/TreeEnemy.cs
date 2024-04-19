using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TreeEnemy : MonoBehaviour
{
    enum State
    {
        Rushing,
        Attacking
    }

    State _currentState;

    NavMeshAgent _treeEnemyAgent;

    [SerializeField]private int _health = 5;

    public int _enemyDamage = 2; 
    [SerializeField]GameObject[] _treePosition;
    int _nearestTreeIndex;
    [SerializeField]TreeHealth _treeTarget; 
    [SerializeField] float[] distances;

    [SerializeField] float _attackRange = 5;
    
    [SerializeField] private float waitTime = 2.0f;
    [SerializeField] private float timer = 0.0f;

    void Awake()
    {
        _treeEnemyAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        SetTrees();
        _currentState = State.Rushing;
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
        _treePosition = GameObject.FindGameObjectsWithTag("Arbol");
        distances = new float[_treePosition.Length];

        for (int i = 0; i < _treePosition.Length; i++)
        {
            distances[i] = Vector3.Distance(transform.position, _treePosition[i].transform.position);
        } 
        _nearestTreeIndex = GetNearestTree();
        _treeTarget = _treePosition[_nearestTreeIndex].GetComponent<TreeHealth>();
    }

    bool OnRange()
    {
        if(Vector3.Distance(transform.position, _treePosition[_nearestTreeIndex].transform.position) <= _attackRange) 
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

        _treeEnemyAgent.destination = _treePosition[_nearestTreeIndex].transform.position;

        if(OnRange() == true)
        {
            _treeEnemyAgent.isStopped = true;
            _currentState = State.Attacking;
        }
    }

    void Attack()
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            timer = 0;
            _treeTarget.TakeDamage(_enemyDamage);
        }

        if (_treeTarget == null)
        {
            SetTrees();
            GetNearestTree();
            _treeEnemyAgent.isStopped = false;
            _currentState = State.Rushing;
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
}