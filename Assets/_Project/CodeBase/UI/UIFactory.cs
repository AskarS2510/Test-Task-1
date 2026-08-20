using _Project.CodeBase.AssetManagement;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.UI
{
    public class UIFactory
    {
        private readonly GameConfig _gameConfig;
        private readonly AssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;

        public UIFactory(StaticDataService staticDataService, AssetProvider assetProvider, IInstantiator instantiator)
        {
            _gameConfig = staticDataService.GameConfig;
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public async UniTask<Hud> CreateHud()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(_gameConfig.HudReference);
            GameObject hud = _instantiator.InstantiatePrefab(prefab);

            return hud.GetComponent<Hud>();
        }
    }
}