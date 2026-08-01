using _Project.CodeBase.Curtain;
using _Project.CodeBase.Gameplay.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.StateMachine.States
{
    public class LoadLevelState : IState
    {
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly PlayerFactory _playerFactory;

        public LoadLevelState(GameplayStateMachine gameplayStateMachine, ILoadingCurtain loadingCurtain, PlayerFactory playerFactory)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _loadingCurtain = loadingCurtain;
            _playerFactory = playerFactory;
        }

        public async void Enter()
        {
            Debug.Log("LoadLevelState enter");

            await CreateWorld();

            _loadingCurtain.Hide();

            _gameplayStateMachine.Enter<GameplayState>();
        }

        public void Exit()
        {
        }

        private async UniTask CreateWorld()
        {
            await _playerFactory.Create();
        }
    }
}