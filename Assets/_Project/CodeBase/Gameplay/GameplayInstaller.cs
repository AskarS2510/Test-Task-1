using _Project.CodeBase.Gameplay.Player;
using _Project.CodeBase.Gameplay.StateMachine;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("GameplayInstaller InstallBindings");
            
            Bind<CameraProvider>();
            Bind<PlayerFactory>();
            Bind<StatesFactory>();
            Bind<InputService>();
            BindInterfacesAndSelfTo<GameplayStateMachine>();
        }

        private void BindInterfacesAndSelfTo<T>()
        {
            Container.BindInterfacesAndSelfTo<T>().AsSingle();
        }

        private void Bind<T>()
        {
            Container.Bind<T>().AsSingle();
        }
    }
}