using _Project.CodeBase.Gameplay.Logic;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Movement
{
    public class Chaser : IUpdatable
    {
        private readonly NavMeshMover _navMeshMover;
        private Transform _target;

        public Chaser(NavMeshMover navMeshMover)
        {
            _navMeshMover = navMeshMover;
        }

        public void Update()
        {
            if (!_target)
                return;

            _navMeshMover.Move(_target.position);
        }

        public void Track(Transform target) => _target = target.transform;
    }
}