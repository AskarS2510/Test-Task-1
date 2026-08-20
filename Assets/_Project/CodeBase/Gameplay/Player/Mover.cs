using UnityEngine;

namespace _Project.CodeBase.Gameplay.Player
{
    public class Mover
    {
        private readonly CharacterController _characterController;
        private readonly float _speed;
        private readonly float _rotationSpeed;

        public Mover(float speed, float rotationSpeed, CharacterController characterController)
        {
            _speed = speed;
            _rotationSpeed = rotationSpeed;
            _characterController = characterController;
        }

        public void Move(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;

            Vector3 moveDirection = new(direction.x, 0f, direction.y);

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

            _characterController.transform.rotation = Quaternion.RotateTowards(_characterController.transform.rotation, targetRotation,
                _rotationSpeed * Time.deltaTime);
        }
    }
}