using UnityEngine;

namespace _Project.CodeBase.Gameplay.Player
{
    public class InputService
    {
        private readonly InputSystemActions _systemActions;
        public Vector2 Motion => _systemActions.Player.Move.ReadValue<Vector2>();

        public InputService()
        {
            _systemActions = new InputSystemActions();
        }

        public void Enable()
        {
            _systemActions.Enable();
        }

        public void Disable()
        {
            _systemActions.Disable();
        }
    }
}