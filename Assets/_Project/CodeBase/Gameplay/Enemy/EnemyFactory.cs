using System;
using _Project.CodeBase.AssetManagement;
using _Project.CodeBase.Gameplay.Logic;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.CodeBase.Gameplay.Enemy
{
    public class EnemyFactory
    {
        private readonly GameConfig _gameConfig;
        private readonly AssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;
        private readonly WayPoints _wayPoints;
        private readonly DamageableRepository _damageableRepository;
        private readonly Updater _updater;
        private readonly ProjectileFactory _projectileFactory;

        public EnemyFactory(StaticDataService staticDataService, AssetProvider assetProvider,
            IInstantiator instantiator, WayPoints wayPoints, DamageableRepository damageableRepository, Updater updater,
            ProjectileFactory projectileFactory)
        {
            _gameConfig = staticDataService.GameConfig;
            _assetProvider = assetProvider;
            _instantiator = instantiator;
            _wayPoints = wayPoints;
            _damageableRepository = damageableRepository;
            _updater = updater;
            _projectileFactory = projectileFactory;
        }

        public async UniTask<GameObject> Create(EnemyType enemyType, Vector3 at)
        {
            switch (enemyType)
            {
                case EnemyType.Patrol:
                    return await CreatePatrol(at);
                case EnemyType.Hunter:
                    return await CreateHunter(at);
                case EnemyType.Shooter:
                    return await CreateShooter(at);
                default:
                    return null;
            }
        }

        private async UniTask<GameObject> CreatePatrol(Vector3 at)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_gameConfig.PatrolReference);
            GameObject go = _instantiator.InstantiatePrefab(prefab, at, Quaternion.identity, null);

            NavMeshAgent agent = go.GetComponent<NavMeshAgent>();
            DamageTrigger damageTrigger = go.GetComponentInChildren<DamageTrigger>();

            NavMeshMover navMeshMover = new(agent, _gameConfig.EnemySpeed, _gameConfig.EnemyRotationSpeed);
            WayPointsMover wayPointsMover = new(navMeshMover, _wayPoints);
            CollisionDamager collisionDamager = new(_damageableRepository, _gameConfig.CollisionDamage,
                damageTrigger);

            collisionDamager.Initialize();

            _updater.Register(wayPointsMover);

            damageTrigger.Collided += _ =>
            {
                _updater.Unregister(wayPointsMover);
                collisionDamager.Dispose();
                Object.Destroy(go);
            };

            return go;
        }

        private async UniTask<GameObject> CreateHunter(Vector3 at)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_gameConfig.HunterReference);
            GameObject go = _instantiator.InstantiatePrefab(prefab, at, Quaternion.identity, null);

            NavMeshAgent agent = go.GetComponent<NavMeshAgent>();
            DamageTrigger damageTrigger = go.GetComponentInChildren<DamageTrigger>();
            ChaseTrigger chaseTrigger = go.GetComponentInChildren<ChaseTrigger>();

            NavMeshMover navMeshMover = new(agent, _gameConfig.EnemySpeed, _gameConfig.EnemyRotationSpeed);
            CollisionDamager collisionDamager = new(_damageableRepository, _gameConfig.CollisionDamage,
                damageTrigger);
            Chaser chaser = new(chaseTrigger, navMeshMover);

            chaser.Initialize();
            collisionDamager.Initialize();

            _updater.Register(chaser);

            damageTrigger.Collided += _ =>
            {
                _updater.Unregister(chaser);
                chaser.Dispose();
                collisionDamager.Dispose();
                Object.Destroy(go);
            };

            return go;
        }

        private async UniTask<GameObject> CreateShooter(Vector3 at)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_gameConfig.ShooterReference);
            GameObject go = _instantiator.InstantiatePrefab(prefab, at, Quaternion.identity, null);

            AggroTrigger aggroTrigger = go.GetComponentInChildren<AggroTrigger>();
            FirePoint firePoint = go.GetComponentInChildren<FirePoint>();

            Shooter shooter = new(aggroTrigger, _projectileFactory, firePoint.transform);

            shooter.Initialize();

            _updater.Register(shooter);

            return go;
        }
    }
}