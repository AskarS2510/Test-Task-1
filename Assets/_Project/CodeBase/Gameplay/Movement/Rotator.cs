using _Project.CodeBase.Gameplay.Logic;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Movement
{
    public class Rotator : IUpdatable
    {
        private readonly float _rotationSpeed;
        private readonly Transform _rotatable;
        private Transform _target;

        public Rotator( float rotationSpeed, Transform rotatable)
        {
            _rotationSpeed = rotationSpeed;
            _rotatable = rotatable;
        }

        public void Update()
        {
            if (!_target)
                return;

            Rotate();
        }

        public void Track(Transform target) => _target = target;

        private void Rotate()
        {
            Vector3 direction = _target.position - _rotatable.position;
            direction.y = 0;

            if (direction.sqrMagnitude < 0.01f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _rotatable.rotation = Quaternion.Slerp(_rotatable.rotation, targetRotation,
                _rotationSpeed * Time.deltaTime);
        }
    }
}