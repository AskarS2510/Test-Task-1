using _Project.CodeBase.Gameplay.Logic;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay.Joystick
{
    public class FloatingJoystick : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _joystickView;
        private InputService _inputService;

        [Inject]
        public void Construct(InputService inputService) => _inputService = inputService;

        private void OnEnable()
        {
            _inputService.Clicked += OnClicked;
            _inputService.Released += OnReleased;
        }

        private void OnDisable()
        {
            _inputService.Clicked -= OnClicked;
            _inputService.Released -= OnReleased;
        }

        private void OnClicked(Vector2 cursorPosition)
        {
            _joystickView.transform.position = cursorPosition;
            Show();
        }

        private void OnReleased() => Hide();

        private void Show() => _canvasGroup.alpha = 1;

        private void Hide() => _canvasGroup.alpha = 0;
    }
}