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
        private readonly Updater _updater;
        private readonly Pauser _pauser;

        public Level(PlayerFactory playerFactory, ILoadingCurtain loadingCurtain, InputService inputService,
            Updater updater, Pauser pauser)
        {
            _playerFactory = playerFactory;
            _loadingCurtain = loadingCurtain;
            _inputService = inputService;
            _updater = updater;
            _pauser = pauser;
        }

        public void Initialize() => InitializeAsync().Forget();

        public void Tick()
        {
            if (_pauser.IsPaused)
                return;

            _updater.Update();
        }

        private async UniTaskVoid InitializeAsync()
        {
            await Create();

            _loadingCurtain.Hide();

            _inputService.Enable();

            _pauser.Resume();
        }

        private async UniTask Create() => await CreateWorld();

        private async UniTask CreateWorld()
        {
            GameObject player = await _playerFactory.Create();
        }
    }
}