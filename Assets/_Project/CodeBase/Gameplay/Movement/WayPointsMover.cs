using _Project.CodeBase.Gameplay.Logic;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Movement
{
    public class WayPointsMover : IUpdatable
    {
        private readonly NavMeshMover _navMeshMover;
        private readonly Transform[] _wayPoints;
        private int _currentPointIndex;

        public WayPointsMover(NavMeshMover navMeshMover, WayPoints wayPoints)
        {
            _navMeshMover = navMeshMover;
            _wayPoints = wayPoints.Points;
        }

        public void Update()
        {
            if (!_navMeshMover.HasReachedDestination())
                return;

            SetNextPointIndex();
            MoveToNextPoint();
        }

        private void SetNextPointIndex() => _currentPointIndex = (_currentPointIndex + 1) % _wayPoints.Length;

        private Vector3 GetCurrentPoint() => _wayPoints[_currentPointIndex].position;

        private void MoveToNextPoint() => _navMeshMover.Move(GetCurrentPoint());
    }
}