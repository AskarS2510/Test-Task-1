using _Project.CodeBase.Curtain;
using _Project.CodeBase.Gameplay.Player;
using _Project.CodeBase.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay
{
    public class Level : IInitializable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly UIFactory _uiFactory;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly InputService _inputService;

        public Level(PlayerFactory playerFactory, UIFactory uiFactory, ILoadingCurtain loadingCurtain, InputService inputService)
        {
            _playerFactory = playerFactory;
            _uiFactory = uiFactory;
            _loadingCurtain = loadingCurtain;
            _inputService = inputService;
        }

        public void Initialize()
        {
            InitializeAsync().Forget();
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
            Hud hud = await _uiFactory.CreateHud();

            GameObject player = await _playerFactory.Create(hud.HealthView);
        }
    }
}