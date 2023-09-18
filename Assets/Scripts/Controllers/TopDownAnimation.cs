using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAnimation : MonoBehaviour
{
    protected Animator _animator;
    protected PlayerController _controller;

    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _controller = GetComponent<PlayerController>();
    }
}
