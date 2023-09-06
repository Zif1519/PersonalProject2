using System.Collections;
using UnityEngine;

public class Movement_Grid : MonoBehaviour, IKeyboardInputHandler
{
    private GameObject _player;
    private IController _controller;

    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private float _timeSinceLastMove = float.MaxValue;

    private float _movementTime = 0.2f;

    private void Awake()
    {
        _player = gameObject;
        _controller = GetComponent<IController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMoveDelay();
    }
    private void Start()
    {
        _controller.OnKeyboardInputHandler += Character_Move;
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

    private IEnumerator GridMove(Vector2 target)
    {
        Vector2 initialPosition = _player.transform.position;
        float currenttime = 0.0f;

        while (currenttime < _movementTime)
        {
            float t = currenttime / _movementTime;
            Vector2 newPosition = Vector2.Lerp(initialPosition, target, t);
            _player.transform.position = new Vector3(newPosition.x, newPosition.y, _player.transform.position.z);
            currenttime += Time.deltaTime;

            yield return null;
        }
        _player.transform.position = new Vector3(target.x, target.y, _player.transform.position.z);
    }

    public void Character_Move(Vector2 direction)
    {
        _movementDirection = direction;
    }
}
