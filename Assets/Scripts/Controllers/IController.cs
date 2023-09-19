using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IController
{
        event Action<Vector2> OnMoveEvent;
        event Action<Vector2> OnLookEvent;
        event Action<AttackSO> OnShootEvent;

        void OnKeyboardInput(InputValue value);
        void OnMouseMove(InputValue value);
        void OnMouseClick(InputValue value);
}
