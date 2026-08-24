using _Project.CodeBase.Gameplay.Logic;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Enemy
{
    public class Patrol : IUpdatable
    {
        private NavMeshMover _navMeshMover;
        private Transform[] _wayPoints;
        private int _currentPointIndex;

        public void Initialize(NavMeshMover navMeshMover, WayPoints wayPoints)
        {
            _navMeshMover = navMeshMover;
            _wayPoints = wayPoints.Points;
        }

        public void Update()
        {
            if (!_navMeshMover.HasReachedDestination())
                return;

            GetNextPointIndex();
            MoveToNextPoint();
        }

        private void GetNextPointIndex()
        {
            _currentPointIndex = (_currentPointIndex + 1) % _wayPoints.Length;
        }

        private void MoveToNextPoint()
        {
            _navMeshMover.Move(_wayPoints[_currentPointIndex].position);
        }
    }
}