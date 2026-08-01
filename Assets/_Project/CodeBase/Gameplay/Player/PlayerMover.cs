using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        private InputService _inputService;
        private float _speed;
        private float _rotationSpeed;

        [Inject]
        public void Construct(InputService inputService)
        {
            _inputService = inputService;
        }

        public void Initialize(float speed, float rotationSpeed)
        {
            _speed = speed;
            _rotationSpeed = rotationSpeed;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 inputDirection = _inputService.Motion;

            if (inputDirection == Vector2.zero)
                return;

            Vector3 moveDirection = new(inputDirection.x, 0f, inputDirection.y);

            Translate(moveDirection);

            Rotate(moveDirection);
        }

        private void Translate(Vector3 moveDirection)
        {
            _characterController.Move(moveDirection * (_speed * Time.deltaTime));
        }

        private void Rotate(Vector3 moveDirection)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}