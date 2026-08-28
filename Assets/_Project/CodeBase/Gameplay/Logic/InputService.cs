using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.CodeBase.Gameplay.Logic
{
    public class InputService
    {
        private readonly InputSystemActions _systemActions;
        public Vector2 Motion => _systemActions.Player.Move.ReadValue<Vector2>();
        public event Action<Vector2> Clicked;
        public event Action Released;

        public InputService() => _systemActions = new InputSystemActions();

        public void Enable()
        {
            _systemActions.Enable();
            _systemActions.UI.Click.performed += OnClick;
            _systemActions.UI.Click.canceled += OnCanceled;
        }

        public void Disable()
        {
            _systemActions.UI.Click.performed -= OnClick;
            _systemActions.UI.Click.canceled -= OnCanceled;
            _systemActions.Disable();
        }

        private void OnClick(InputAction.CallbackContext obj) =>
            Clicked?.Invoke(_systemActions.UI.Point.ReadValue<Vector2>());

        private void OnCanceled(InputAction.CallbackContext obj) => Released?.Invoke();
    }
}