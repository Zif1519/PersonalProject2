using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputHandler
{
        event Action<Vector2> OnKeyboardInputHandler;
        event Action<Vector2> OnMouseMoveHandler;
        event Action OnMouseClickHandler;

        void OnKeyboardInput(InputValue value);
        void OnMouseMove(InputValue value);
        void OnMouseClick(InputValue value);
}
