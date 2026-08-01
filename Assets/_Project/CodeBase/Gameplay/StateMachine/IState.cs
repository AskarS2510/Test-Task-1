namespace _Project.CodeBase.Gameplay.StateMachine
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}