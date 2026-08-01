using _Project.CodeBase.AssetManagement;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay.Player
{
    public class PlayerFactory
    {
        private readonly GameConfig _gameConfig;
        private readonly AssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;
        private readonly CameraProvider _cameraProvider;

        public PlayerFactory(StaticDataService staticDataService, AssetProvider assetProvider, IInstantiator instantiator,
            CameraProvider cameraProvider)
        {
            _gameConfig = staticDataService.GameConfig;
            _assetProvider = assetProvider;
            _instantiator = instantiator;
            _cameraProvider = cameraProvider;
        }

        public async UniTask<GameObject> Create()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_gameConfig.PlayerReference);
            GameObject player = _instantiator.InstantiatePrefab(prefab);

            await _cameraProvider.Create();
            _cameraProvider.Follow(player.transform);

            player.GetComponent<PlayerMover>().Initialize(_gameConfig.PlayerMoveSpeed, _gameConfig.PlayerRotationSpeed);

            return prefab;
        }
    }
}