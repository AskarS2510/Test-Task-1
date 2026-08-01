using _Project.CodeBase.AssetManagement;
using _Project.CodeBase.Curtain;
using _Project.CodeBase.Data;
using _Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class EntryPoint : IInitializable
    {
        private readonly AssetProvider _assetProvider;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ProgressService _progressService;
        private readonly StaticDataService _staticDataService;
        private readonly SceneLoader _sceneLoader;

        public EntryPoint(AssetProvider assetProvider, ILoadingCurtain loadingCurtain, ProgressService progressService,
            StaticDataService staticDataService, SceneLoader sceneLoader)
        {
            _assetProvider = assetProvider;
            _loadingCurtain = loadingCurtain;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _sceneLoader = sceneLoader;
        }

        public async void Initialize()
        {
            _loadingCurtain.Show();

            await InitializeServices();

            await _sceneLoader.Load(_progressService.Progress.LevelData.GetLevelKey());
        }

        private async UniTask InitializeServices()
        {
            _progressService.LoadProgressOrInitNew();
            await _staticDataService.InitializeAsync();
            await _assetProvider.InitializeAsync();
        }
    }
}