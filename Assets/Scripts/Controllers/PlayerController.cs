using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IController
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnShootEvent;

    private Camera _camera;// Start is called before the first frame update
    [SerializeField]
    private AttackSO _attackSO;
    private void Awake()
    {   
        _camera = Camera.main;
    }

    private void Start()
    {
        _attackSO = GetComponent<CharacterStatsHandler>()._CurrentStats._AttackSO;
    }

    public void OnKeyboardInput(InputValue value)
    {
        //Debug.Log("OnMove" + value.ToString());
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }
    public void OnMouseMove(InputValue value)
    {
        //Debug.Log("OnLook" + value.ToString());
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        Vector2 newAimDir = (worldPos - (Vector2)transform.position).normalized;
        CallLookEvent(newAimDir);
    }
    public void OnMouseClick(InputValue value)
    {
        CallShootEvent(_attackSO);
    }

    public void CallMoveEvent(Vector2 moveInput)
    {
        OnMoveEvent?.Invoke(moveInput);
    }
    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    public void CallShootEvent(AttackSO attackSO)
    {
        OnShootEvent?.Invoke(attackSO);
    }
}
