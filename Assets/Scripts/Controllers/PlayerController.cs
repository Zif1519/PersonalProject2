using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IController
{
    public event Action<Vector2> OnKeyboardInputHandler;
    public event Action<Vector2> OnMouseMoveHandler;
    public event Action<AttackSO> OnMouseClickHandler;

    private Camera _camera;// Start is called before the first frame update
    private AttackSO _attackSO;
    private void Awake()
    {   
        _camera = Camera.main;
        _attackSO = GetComponent<CharacterStatsHandler>()._CurrentStats._AttackSO;
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
        OnMouseMoveHandler?.Invoke(newAimDir);
    }
    public void OnMouseClick(InputValue value)
    {
        Debug.Log("»£√‚µ ");
        OnMouseClickHandler?.Invoke(_attackSO);
    }
}
