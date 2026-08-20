using System.Collections.Generic;
using _Project.CodeBase.AssetManagement;
using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.StaticData
{
    public class StaticDataService
    {
        private readonly AssetProvider _assetProvider;
        public GameConfig GameConfig { get; private set; }

        public StaticDataService(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask InitializeAsync()
        {
            List<UniTask> tasks = new();

            tasks.Add(LoadGameConfig());

            await UniTask.WhenAll(tasks);
        }

        private async UniTask LoadGameConfig()
        {
            GameConfig = await _assetProvider.Load<GameConfig>(AssetLabels.GAME_CONFIG);
        }
    }
}