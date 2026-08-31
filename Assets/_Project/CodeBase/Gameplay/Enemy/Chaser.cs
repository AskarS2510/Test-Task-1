using _Project.CodeBase.Gameplay.Logic;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Enemy
{
    public class Chaser : IUpdatable
    {
        private readonly AggroTrigger _aggroTrigger;
        private readonly NavMeshMover _navMeshMover;
        private Transform _target;

        public Chaser(AggroTrigger aggroTrigger, NavMeshMover navMeshMover)
        {
            _aggroTrigger = aggroTrigger;
            _navMeshMover = navMeshMover;
        }

        public void Initialize() => _aggroTrigger.Collided += Track;

        public void Dispose() => _aggroTrigger.Collided -= Track;

        public void Update()
        {
            if (!_target)
                return;

            _navMeshMover.Move(_target.position);
        }

        private void Track(Collider other) => _target = other.transform;
    }
}