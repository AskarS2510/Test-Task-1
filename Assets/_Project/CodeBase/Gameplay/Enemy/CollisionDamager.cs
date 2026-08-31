using _Project.CodeBase.Gameplay.Logic;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Enemy
{
    public class CollisionDamager
    {
        private readonly DamageableRepository _damageableRepository;
        private readonly int _damage;
        private readonly DamageTrigger _damageTrigger;

        public CollisionDamager(DamageableRepository damageableRepository, int damage, DamageTrigger damageTrigger)
        {
            _damageableRepository = damageableRepository;
            _damage = damage;
            _damageTrigger = damageTrigger;
        }

        public void Initialize() => _damageTrigger.Collided += DealDamage;

        public void Dispose() => _damageTrigger.Collided -= DealDamage;

        private void DealDamage(Collider other) => _damageableRepository.TryGet(other.gameObject)?.TakeDamage(_damage);
    }
}