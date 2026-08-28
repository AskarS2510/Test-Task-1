using _Project.CodeBase.AssetManagement;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Project.CodeBase.Gameplay.Logic
{
    public class PlayerFactory
    {
        private readonly GameConfig _gameConfig;
        private readonly AssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;
        private readonly CameraProvider _cameraProvider;
        private readonly HealthPresenter _healthPresenter;
        private readonly Updater _updater;

        public PlayerFactory(StaticDataService staticDataService, AssetProvider assetProvider,
            IInstantiator instantiator,
            CameraProvider cameraProvider, HealthPresenter healthPresenter, Updater updater)
        {
            _gameConfig = staticDataService.GameConfig;
            _assetProvider = assetProvider;
            _instantiator = instantiator;
            _cameraProvider = cameraProvider;
            _healthPresenter = healthPresenter;
            _updater = updater;
        }

        public async UniTask<GameObject> Create()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_gameConfig.PlayerReference);
            GameObject go = _instantiator.InstantiatePrefab(prefab);

            DirectionMover directionMover =
                new(go.GetComponent<NavMeshAgent>(), _gameConfig.PlayerMoveSpeed, _gameConfig.PlayerRotationSpeed);
            Health health = new(_gameConfig.PlayerHealth);

            Player player = _instantiator.Instantiate<Player>();
            player.Initialize(directionMover, health);

            _healthPresenter.Initialize(health);

            _cameraProvider.Follow(go.transform);

            _updater.Register(player);

            player.Died += () =>
            {
                _updater.Unregister(player);
                Object.Destroy(go);
            };

            return go;
        }
    }
}