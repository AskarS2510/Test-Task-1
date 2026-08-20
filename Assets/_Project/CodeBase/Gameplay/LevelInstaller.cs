using _Project.CodeBase.Gameplay.Player;
using _Project.CodeBase.UI;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("LevelInstaller InstallBindings");

            Bind<CameraProvider>();
            Bind<PlayerFactory>();
            Bind<UIFactory>();
            Bind<InputService>();
            BindInterfacesAndSelfTo<Level>();
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