using System.Linq;
using FpvDroneSimulator.Services.SceneUIRootHolder;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.DI.Installers
{
    public class SceneSetupInstaller : MonoInstaller
    {
        [SerializeField] private SceneRootsHolder sceneRootsHolder;

        public override void InstallBindings()
        {
            Container.ParentContainers.First().Bind<SceneRootsHolder>().ToSelf().FromInstance(sceneRootsHolder).AsSingle().NonLazy();
        }
    }
}