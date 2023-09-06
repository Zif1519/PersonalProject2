using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IInputHandler
{
    public event Action<Vector2> OnKeyboardInputHandler;
    public event Action<Vector2> OnMouseMoveHandler;
    public event Action OnMouseClickHandler;

    private float _timeSinceLastAttack = float.MaxValue;

    protected bool IsAttacking { get; set; }

    private Camera _camera;// Start is called before the first frame update
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    public void OnKeyboardInput(InputValue value)
    {
        //Debug.Log("OnMove" + value.ToString());
        Vector2 moveInput = value.Get<Vector2>().normalized;
        OnKeyboardInputHandler?.Invoke(moveInput);
    }
    public void OnMouseMove(InputValue value)
    {
        //Debug.Log("OnLook" + value.ToString());
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        Vector2 newAimDir = (worldPos - (Vector2)transform.position).normalized;

        if(newAimDir.magnitude >= 0.9f)
        {
            OnMouseMoveHandler?.Invoke(newAimDir);
        }
    }

    public void OnMouseClick(InputValue value)
    {
        IsAttacking = value.isPressed;
    }

    private void HandleAttackDelay()
    {
        if (_timeSinceLastAttack <= 0.2f)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }
        if (IsAttacking && _timeSinceLastAttack > 0.2f)
        {
            _timeSinceLastAttack = 0f;
            CallMouseClickEvent();
        }
    }


    public void CallMouseClickEvent()
    {
        OnMouseClickHandler?.Invoke();
    }
}
