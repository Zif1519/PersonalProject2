using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAnimationController : TopDownAnimation
{
    private static readonly int ISWALKING = Animator.StringToHash("ISWALKING");
    private static readonly int ATTACK = Animator.StringToHash("ATTACK");
    private static readonly int ISHIT = Animator.StringToHash("ISHIT");

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _controller.OnShootEvent += Attacking;
        _controller.OnMoveEvent += Move;
    }

    private void Attacking(AttackSO attackSO)
    {
        _animator.SetTrigger(ATTACK);
    }

    private void Move(Vector2 direction)
    {
        _animator.SetBool(ISWALKING, direction.magnitude > 0.5f);
    }

    private void Hit()
    {
        _animator.SetBool(ISHIT, true);
    }

    private void InvincibilityEnd()
    {
        _animator.SetBool(ISHIT, false);
    }
}
