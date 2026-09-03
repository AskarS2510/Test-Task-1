using System.Collections.Generic;
using _Project.CodeBase.Gameplay.Healths;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.Attack
{
    public class DamageableRepository
    {
        private readonly Dictionary<GameObject, IDamageable> _repository;

        public DamageableRepository() => _repository = new Dictionary<GameObject, IDamageable>();

        public IDamageable TryGet(GameObject gameObject) => _repository.GetValueOrDefault(gameObject);

        public void Register(GameObject gameObject, IDamageable damageable) => _repository[gameObject] = damageable;

        public void Unregister(GameObject gameObject) => _repository.Remove(gameObject);
    }
}