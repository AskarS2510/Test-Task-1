using _Project.CodeBase.AssetManagement;
using _Project.CodeBase.StaticData;
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
    }
}