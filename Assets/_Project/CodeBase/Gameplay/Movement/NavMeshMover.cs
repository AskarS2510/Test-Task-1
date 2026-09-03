using UnityEngine;
using UnityEngine.AI;

namespace _Project.CodeBase.Gameplay.Movement
{
    public class NavMeshMover
    {
        private readonly NavMeshAgent _navMeshAgent;

        public NavMeshMover(NavMeshAgent navMeshAgent, float speed, float rotationSpeed)
        {
            _navMeshAgent = navMeshAgent;
            _navMeshAgent.speed = speed;
            _navMeshAgent.angularSpeed = rotationSpeed;
        }

        public void Move(Vector3 destination) => _navMeshAgent.SetDestination(destination);

        public bool HasReachedDestination() => _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;
    }
}