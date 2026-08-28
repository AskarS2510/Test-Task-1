using _Project.CodeBase.AssetManagement;
using _Project.CodeBase.Curtain;
using _Project.CodeBase.Data;
using _Project.CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("GameInstaller InstallBindings");

            BindInfrastructureUI();
            Bind<SceneLoader>();
            Bind<ProgressService>();
            Bind<AssetProvider>();
            Bind<StaticDataService>();
            BindInterfacesAndSelfTo<EntryPoint>();
        }

        private void BindInfrastructureUI() =>
            Container.BindInterfacesAndSelfTo<LoadingCurtain>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CURTAIN)
                .AsSingle();

        private void BindInterfacesAndSelfTo<T>() => Container.BindInterfacesAndSelfTo<T>().AsSingle();

        private void Bind<T>() => Container.Bind<T>().AsSingle();
    }
}