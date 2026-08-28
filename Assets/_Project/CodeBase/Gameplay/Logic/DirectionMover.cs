using UnityEngine;
using UnityEngine.AI;

namespace _Project.CodeBase.Gameplay.Logic
{
    public class DirectionMover
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly float _speed;
        private readonly float _rotationSpeed;

        public DirectionMover(NavMeshAgent navMeshAgent, float speed, float rotationSpeed)
        {
            _navMeshAgent = navMeshAgent;
            _speed = speed;
            _rotationSpeed = rotationSpeed;
        }

        public void Move(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;

            Translate(direction);

            Rotate(direction);
        }

        private void Translate(Vector3 moveDirection) => _navMeshAgent.Move(moveDirection * (_speed * Time.deltaTime));

        private void Rotate(Vector3 moveDirection)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            _navMeshAgent.transform.rotation = Quaternion.RotateTowards(_navMeshAgent.transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime);
        }
    }
}