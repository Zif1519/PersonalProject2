using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private IInputHandler _controller;
    [SerializeField] private Transform projectileSpawnPosition;

    public GameObject testPrefab;

    private float _timeSinceLastAttack = 0f;
    private bool _isReady = true;
    
    private void Awake()
    {
        _controller = GetComponent<IInputHandler>();
    }
    private void Start()
    {
        _controller.OnMouseClickHandler += OnShoot;
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void OnShoot()
    {
        if (_isReady)
        {
            CreateProjectile();
            _isReady = false;
            _timeSinceLastAttack = 0f;
        }
    }

    private void CreateProjectile()
    {
        Instantiate(testPrefab, projectileSpawnPosition.position, Quaternion.identity);
    }

    private void HandleAttackDelay()
    {
        if ( !_isReady )
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_timeSinceLastAttack > 0.2f)
            {
                _isReady = true;
            }
        }
    }
}
