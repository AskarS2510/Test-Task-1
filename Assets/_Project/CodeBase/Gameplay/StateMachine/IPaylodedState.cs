using Cysharp.Threading.Tasks;

namespace _Project.CodeBase.Gameplay.StateMachine
{
    public interface IPaylodedState<TPayload> : IExitableState
    {
        UniTask Enter(TPayload payload);
    }
}