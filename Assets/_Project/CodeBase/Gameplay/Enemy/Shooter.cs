using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Enemy
{
    public class Shooter : IUpdatable
    {
        private const float BASE_COOLDOWN = 1f;
        private readonly AggroTrigger _aggroTrigger;
        private readonly ProjectileFactory _projectileFactory;
        private readonly Transform _firePoint;
        private Transform _target;
        private float _cooldown;

        public Shooter(AggroTrigger aggroTrigger, ProjectileFactory projectileFactory, Transform firePoint)
        {
            _aggroTrigger = aggroTrigger;
            _projectileFactory = projectileFactory;
            _firePoint = firePoint;
        }

        public void Initialize() => _aggroTrigger.Collided += StartShooting;

        public void Dispose() => _aggroTrigger.Collided -= StartShooting;

        public void Update()
        {
            if (!_target)
                return;

            _cooldown -= Time.deltaTime;

            if (_cooldown > 0)
                return;

            _cooldown = BASE_COOLDOWN;

            Fire();
        }

        private void StartShooting(Collider other) => _target = other.transform;

        private void Fire()
        {
            Vector3 direction = _target.position - _aggroTrigger.transform.position;

            _projectileFactory.Create(_firePoint.position, direction).Forget();
        }
    }
}