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
        private readonly InputService _inputService;

        public PlayerFactory(StaticDataService staticDataService, AssetProvider assetProvider,
            IInstantiator instantiator,
            CameraProvider cameraProvider, HealthPresenter healthPresenter, Updater updater,
            InputService inputService)
        {
            _gameConfig = staticDataService.GameConfig;
            _assetProvider = assetProvider;
            _instantiator = instantiator;
            _cameraProvider = cameraProvider;
            _healthPresenter = healthPresenter;
            _updater = updater;
            _inputService = inputService;
        }

        public async UniTask<GameObject> Create()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_gameConfig.PlayerReference);
            GameObject go = _instantiator.InstantiatePrefab(prefab);

            NavMeshAgent navMeshAgent = go.GetComponent<NavMeshAgent>();

            DirectionMover directionMover = new(navMeshAgent, _gameConfig.PlayerMoveSpeed,
                _gameConfig.PlayerRotationSpeed);
            Health health = new(_gameConfig.PlayerHealth);
            Player player = new(_inputService, directionMover, health);

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