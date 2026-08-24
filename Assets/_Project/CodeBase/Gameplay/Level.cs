using _Project.CodeBase.Curtain;
using _Project.CodeBase.Gameplay.Logic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay
{
    public class Level : IInitializable, ITickable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly InputService _inputService;

        public Level(PlayerFactory playerFactory, ILoadingCurtain loadingCurtain, InputService inputService)
        {
            _playerFactory = playerFactory;
            _loadingCurtain = loadingCurtain;
            _inputService = inputService;
        }

        public void Initialize()
        {
            InitializeAsync().Forget();
        }

        public void Tick()
        {
            
        }

        private async UniTaskVoid InitializeAsync()
        {
            await Create();

            _loadingCurtain.Hide();

            _inputService.Enable();
        }

        private async UniTask Create()
        {
            await CreateWorld();
        }

        private async UniTask CreateWorld()
        {
            GameObject player = await _playerFactory.Create();
        }
    }
}