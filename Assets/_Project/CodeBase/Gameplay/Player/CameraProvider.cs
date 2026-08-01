using _Project.CodeBase.AssetManagement;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay.Player
{
    public class CameraProvider
    {
        private readonly IInstantiator _instantiator;
        private readonly AssetProvider _assetProvider;
        private readonly GameConfig _gameConfig;
        private CinemachineCamera _cinemachine;

        public CameraProvider(IInstantiator instantiator, AssetProvider assetProvider, StaticDataService staticDataService)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
            _gameConfig = staticDataService.GameConfig;
        }

        public async UniTask Create()
        {
            GameObject mainCameraPrefab = await _assetProvider.Load<GameObject>(_gameConfig.MainCameraReference);
            GameObject cinemachinePrefab = await _assetProvider.Load<GameObject>(_gameConfig.CinemachineReference);

            _instantiator.InstantiatePrefab(mainCameraPrefab);
            _cinemachine = _instantiator.InstantiatePrefab(cinemachinePrefab).GetComponent<CinemachineCamera>();
        }

        public void Follow(Transform target)
        {
            _cinemachine.Follow = target;
        }
    }
}