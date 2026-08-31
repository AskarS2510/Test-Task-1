using _Project.CodeBase.Gameplay.Logic;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Enemy
{
    public class Chaser : IUpdatable
    {
        private readonly ChaseTrigger _chaseTrigger;
        private readonly NavMeshMover _navMeshMover;
        private Transform _target;

        public Chaser(ChaseTrigger chaseTrigger, NavMeshMover navMeshMover)
        {
            _chaseTrigger = chaseTrigger;
            _navMeshMover = navMeshMover;
        }

        public void Initialize() => _chaseTrigger.Collided += Track;

        public void Dispose() => _chaseTrigger.Collided -= Track;

        public void Update()
        {
            if (!_target)
                return;

            _navMeshMover.Move(_target.position);
        }

        private void Track(Collider other) => _target = other.transform;
    }
}