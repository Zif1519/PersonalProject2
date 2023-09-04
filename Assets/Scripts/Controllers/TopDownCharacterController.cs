using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnAttackEvent;

    private float _timeSinceLastAttack = float.MaxValue;

    protected bool IsAttacking { get; set; }

    protected virtual void Update()
    {
        HandleAttackDelay();
    }


    void Start()
    {
        
    }

    private void HandleAttackDelay()
    {
        if(_timeSinceLastAttack <= 0.2f)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }
        if (IsAttacking && _timeSinceLastAttack > 0.2f)
        {
            _timeSinceLastAttack = 0f;
            CallAttackEvent();
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }
    public void CallLookEvent(Vector2 direction) 
    { 
        OnLookEvent?.Invoke(direction);
    }
    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
}
    //[SerializeField]
    //private float speed = 3.0f;
    //// Start is called before the first frame update
    /////float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");

        //transform.position += new Vector3(x, y) * Time.deltaTime * speed;