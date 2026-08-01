using _Project.CodeBase.Gameplay.Player;
using UnityEngine;

namespace _Project.CodeBase.Gameplay.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly InputService _inputService;

        public GameplayState(InputService inputService)
        {
            _inputService = inputService;
        }

        public void Enter()
        {
            Debug.Log("GameplayState enter");

            _inputService.Enable();
        }

        public void Exit()
        {
            _inputService.Disable();
        }
    }
}