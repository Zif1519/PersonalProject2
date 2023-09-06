using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private GameObject _player;
    private PlayerController _controller;

    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;
    private float _timeSinceLastMove = float.MaxValue;
    private bool IsMoving { get; set; }
    private float _movementTime = 0.2f;
    private void Awake()
    {
        _player = gameObject;
        _controller = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMoveDelay();
    }
    private void Start()
    {
        _controller.OnMoveEvent += Move;
    }

    private void HandleMoveDelay()
    {
        if (_timeSinceLastMove <= 0.2f)
        {
            _timeSinceLastMove += Time.deltaTime;
        }
        if (_timeSinceLastMove > 0.2f && _movementDirection != Vector2.zero)
        {
            _timeSinceLastMove = 0f;
            StartCoroutine(GridMove(new Vector2(transform.position.x +_movementDirection.x, transform.position.y + _movementDirection.y)));
        }
    }
    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private IEnumerator GridMove(Vector2 target)
    {
        Vector2 initialPosition = _player.transform.position;
        float currenttime = 0.0f;

        while (currenttime < _movementTime)
        {
            // 시간에 따른 보간된 위치 계산
            float t = currenttime / _movementTime;
            Vector2 newPosition = Vector2.Lerp(initialPosition, target, t);

            // 트랜스폼 위치 업데이트
            _player.transform.position = new Vector3(newPosition.x, newPosition.y, _player.transform.position.z);

            // 시간 경과 업데이트
            currenttime += Time.deltaTime;

            yield return null;
        }

        // 보간이 완료된 후 최종 위치를 설정
        _player.transform.position = new Vector3(target.x, target.y, _player.transform.position.z);
    }
}
