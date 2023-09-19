using UnityEngine;

public class Movement_Smooth : MonoBehaviour, IKeyboardInputHandler
{
    [SerializeField]
    private IController _controller;
    [SerializeField]
    private CharacterStatsHandler _stats;

    private Vector2 _movementDirection = Vector2.zero;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _controller = GetComponent<IController>();
        _stats = GetComponent<CharacterStatsHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyMovement(_movementDirection);
    }
    private void Start()
    {
        _controller.OnMoveEvent += Character_Move;
    }

    public void Character_Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * _stats._CurrentStats._Speed;
        _rigidbody.velocity = direction;
    }
}
