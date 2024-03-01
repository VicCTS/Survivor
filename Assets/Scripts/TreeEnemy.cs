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
       SetTrees();
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

    void Start()
    {
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
        Debug.Log(_nearestTreeIndex);
        _treeEnemyAgent.destination = _treePosition[_nearestTreeIndex].transform.position;

        if(OnRange() == true)
        {
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

        /*if (_treeTarget == null)
        {
            GetNearestTree();
            _currentState = State.Rushing;
        }*/
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
}