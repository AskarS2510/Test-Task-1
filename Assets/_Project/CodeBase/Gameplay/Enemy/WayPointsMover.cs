using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Logic;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Enemy
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

    public class Patrol
    {
        
    }

    public class CollisionDamager
    {
        private readonly DamageableRepository _damageableRepository;
        private readonly int _damage;

        public CollisionDamager(DamageableRepository damageableRepository, int damage)
        {
            _damageableRepository = damageableRepository;
            _damage = damage;
        }

        public void Collide(Collider other)
        {
            _damageableRepository.Get(other.gameObject).TakeDamage(_damage);
        }
    }

    public class DamageableRepository
    {
        private readonly Dictionary<GameObject, IDamageable> _repository;

        public DamageableRepository() => _repository = new Dictionary<GameObject, IDamageable>();

        public IDamageable Get(GameObject gameObject) => _repository[gameObject];

        public void Register(GameObject gameObject, IDamageable damageable) => _repository[gameObject] = damageable;

        public void Unregister(GameObject gameObject) => _repository.Remove(gameObject);
    }
}