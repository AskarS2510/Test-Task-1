using _Project.CodeBase.Gameplay.Enemy;
using _Project.CodeBase.Gameplay.Logic;
using _Project.CodeBase.UI;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Gameplay
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Hud _hud;
        [SerializeField] private CameraProvider _cameraProvider;
        [SerializeField] private WayPoints _wayPoints;

        public override void InstallBindings()
        {
            Debug.Log("LevelInstaller InstallBindings");

            BindHud();
            BindCameraProvider();
            BindWayPoints();

            Bind<PlayerFactory>();
            Bind<UIFactory>();
            Bind<InputService>();
            Bind<HealthPresenter>();
            Bind<Updater>();
            Bind<Pauser>();

            BindInterfacesAndSelfTo<Level>();
        }

        private void BindCameraProvider() => Container.Bind<CameraProvider>().FromInstance(_cameraProvider).AsSingle();

        private void BindHud() => Container.Bind<Hud>().FromInstance(_hud).AsSingle();

        private void BindWayPoints() => Container.Bind<WayPoints>().FromInstance(_wayPoints).AsSingle();

        private void BindInterfacesAndSelfTo<T>() => Container.BindInterfacesAndSelfTo<T>().AsSingle();

        private void Bind<T>() => Container.Bind<T>().AsSingle();
    }
}