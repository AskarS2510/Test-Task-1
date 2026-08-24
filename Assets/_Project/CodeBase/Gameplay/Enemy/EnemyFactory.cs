using _Project.CodeBase.AssetManagement;
using _Project.CodeBase.Gameplay.Logic;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Project.CodeBase.Gameplay.Enemy
{
    public class EnemyFactory
    {
        private readonly GameConfig _gameConfig;
        private readonly AssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;

        public EnemyFactory(StaticDataService staticDataService, AssetProvider assetProvider, IInstantiator instantiator)
        {
            _gameConfig = staticDataService.GameConfig;
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public async UniTask<GameObject> CreatePatrol()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_gameConfig.EnemyReference);
            GameObject go = _instantiator.InstantiatePrefab(prefab);

            DirectionMover directionMover = new(go.GetComponent<NavMeshAgent>(), _gameConfig.EnemySpeed, _gameConfig.EnemyRotationSpeed);

            Player player = go.GetComponent<Player>();

            player.Died += () => Object.Destroy(go);

            return go;
        }
    }
}