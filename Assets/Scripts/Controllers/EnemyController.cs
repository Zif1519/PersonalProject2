using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PlayerController
{
    GameManager _gameManager;
    public Transform _ClosestTarget { get; private set; }

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        _gameManager = GameManager.Instance;
        _ClosestTarget = _gameManager._Player;
    }

    protected virtual void FixedUpdate()
    {
        
    }
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, _ClosestTarget.position);
    }

    protected Vector2 DirectionToTarget()
    {
        var a = (_ClosestTarget.position - transform.position).normalized;
        Debug.Log("최단거리 뽑음" + a);
        return a;
    }
}
