using _Project.CodeBase.Gameplay.Logic;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Projectiles
{
    public class Projectile : IUpdatable
    {
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly Vector3 _direction;

        public Projectile(Transform transform, float speed, Vector3 direction)
        {
            _transform = transform;
            _speed = speed;
            _direction = direction;
        }

        public void Update() => _transform.Translate(_direction * _speed * Time.deltaTime);
    }
}