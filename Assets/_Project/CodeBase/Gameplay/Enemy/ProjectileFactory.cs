using _Project.CodeBase.AssetManagement;
using _Project.CodeBase.Gameplay.Logic;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay.Enemy
{
    public class ProjectileFactory
    {
        private readonly GameConfig _gameConfig;
        private readonly AssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;
        private readonly Updater _updater;
        private readonly DamageableRepository _damageableRepository;

        public ProjectileFactory(StaticDataService staticDataService, AssetProvider assetProvider,
            IInstantiator instantiator, Updater updater, DamageableRepository damageableRepository)
        {
            _gameConfig = staticDataService.GameConfig;
            _assetProvider = assetProvider;
            _instantiator = instantiator;
            _updater = updater;
            _damageableRepository = damageableRepository;
        }

        public async UniTask<GameObject> Create(Vector3 at, Vector3 direction)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_gameConfig.ProjectileReference);
            GameObject go = _instantiator.InstantiatePrefab(prefab, at, Quaternion.identity, null);

            DamageTrigger damageTrigger = go.GetComponentInChildren<DamageTrigger>();

            CollisionDamager collisionDamager = new(_damageableRepository, _gameConfig.ProjectileDamage, damageTrigger);
            Projectile projectile = new(go.transform, _gameConfig.ProjectileSpeed, direction.normalized);

            collisionDamager.Initialize();

            _updater.Register(projectile);

            damageTrigger.Collided += _ =>
            {
                _updater.Unregister(projectile);
                collisionDamager.Dispose();
                Object.Destroy(go);
            };

            return go;
        }
    }
}