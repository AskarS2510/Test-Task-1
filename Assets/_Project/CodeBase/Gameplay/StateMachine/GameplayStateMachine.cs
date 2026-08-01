using _Project.CodeBase.Gameplay.StateMachine.States;
using Zenject;

namespace _Project.CodeBase.Gameplay.StateMachine
{
    public class GameplayStateMachine : StateMachine, IInitializable
    {
        private readonly StatesFactory _statesFactory;

        public GameplayStateMachine(StatesFactory statesFactory)
        {
            _statesFactory = statesFactory;
        }

        public void Initialize()
        {
            RegisterState(_statesFactory.Create<LoadLevelState>());
            RegisterState(_statesFactory.Create<GameplayState>());

            Enter<LoadLevelState>();
        }
    }
}