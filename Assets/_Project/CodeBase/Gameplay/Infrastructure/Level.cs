using _Project.CodeBase.Curtain;
using _Project.CodeBase.Gameplay.Enemy;
using _Project.CodeBase.Gameplay.Logic;
using _Project.CodeBase.Gameplay.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay.Infrastructure
{
    public class Level : IInitializable, ITickable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly InputService _inputService;
        private readonly Updater _updater;
        private readonly Pauser _pauser;
        private readonly EnemyFactory _enemyFactory;
        private readonly EnemySpawnPoints _enemySpawnPoints;

        public Level(PlayerFactory playerFactory, ILoadingCurtain loadingCurtain, InputService inputService,
            Updater updater, Pauser pauser, EnemyFactory enemyFactory, EnemySpawnPoints enemySpawnPoints)
        {
            _playerFactory = playerFactory;
            _loadingCurtain = loadingCurtain;
            _inputService = inputService;
            _updater = updater;
            _pauser = pauser;
            _enemyFactory = enemyFactory;
            _enemySpawnPoints = enemySpawnPoints;
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
            await CreateWorld();

            _loadingCurtain.Hide();

            _inputService.Enable();

            _pauser.Resume();
        }

        private async UniTask CreateWorld()
        {
            GameObject player = await _playerFactory.Create();

            foreach (SpawnPoint spawnPoint in _enemySpawnPoints.Points)
            {
                await _enemyFactory.Create(spawnPoint.EnemyType, spawnPoint.transform.position);
            }
        }
    }
}