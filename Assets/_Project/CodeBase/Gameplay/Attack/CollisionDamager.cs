using UnityEngine;

namespace _Project.CodeBase.Gameplay.Attack
{
    public class CollisionDamager
    {
        private readonly DamageableRepository _damageableRepository;
        private readonly int _damage;

        public CollisionDamager(DamageableRepository damageableRepository, int damage)
        {
            _damageableRepository = damageableRepository;
            _damage = damage;
        }

        public void DealDamage(GameObject other) => _damageableRepository.TryGet(other)?.TakeDamage(_damage);
    }
}