using _Project.CodeBase.Gameplay.Logic;
using _Project.CodeBase.Gameplay.Projectiles;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Attack
{
    public class Shooter : IUpdatable
    {
        private const float BASE_COOLDOWN = 1f;
        private readonly ProjectileFactory _projectileFactory;
        private readonly Transform _firePoint;
        private readonly float _rotationSpeed;
        private Transform _target;
        private float _cooldown;

        public Shooter(ProjectileFactory projectileFactory, Transform firePoint)
        {
            _projectileFactory = projectileFactory;
            _firePoint = firePoint;
        }

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

        public void StartShooting(Transform target) => _target = target.transform;

        private void Fire()
        {
            Vector3 direction = _target.position - _firePoint.position;
            direction.y = 0;

            _projectileFactory.Create(_firePoint.position, direction).Forget();
        }
    }
}