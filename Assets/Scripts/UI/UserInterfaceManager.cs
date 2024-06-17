using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.Common.Utilities.Logging;
using FpvDroneSimulator.Services.AssetLoadingService;
using FpvDroneSimulator.Services.SceneUIRootHolder;
using FpvDroneSimulator.UI.Core;
using UnityEngine;
using Zenject;
using ILogger = FpvDroneSimulator.Common.Utilities.Logging.ILogger;

namespace FpvDroneSimulator.UI
{
    public class UserInterfaceManager : IViewVisualizer, IInitializable, IDisposable
    {
        private readonly ILogger logger;
        private readonly IAssetLoadingService assetLoadingService;
        private readonly DiContainer container;

        private List<IView> spawnedViews;

        protected UserInterfaceManager(ILogger logger, IAssetLoadingService assetLoadingService, DiContainer container)
        {
            this.logger = logger;
            this.assetLoadingService = assetLoadingService;
            this.container = container;
        }

        public void Initialize()
        {
            spawnedViews = new List<IView>();
        }

        public void Dispose()
        {
            spawnedViews.Clear();
        }
        public async Task<TView> Visualize<TView, TOptions>(TOptions options, CancellationToken cancellationToken, bool useAnimation = false)
            where TView : MonoBehaviour, IView
            where TOptions : IOptions
        {
            TView spawnedView = await SpawnView<TView>();
            await spawnedView.Open(options, cancellationToken, useAnimation);
            spawnedViews.Add(spawnedView);
            return spawnedView;
        }

        public async Task Hide<TView, TOptions>(TOptions options, CancellationToken cancellationToken, bool useAnimation = false) 
            where TView : MonoBehaviour, IView
            where TOptions : IOptions
        {
            TView view = spawnedViews.FirstOrDefault(view => view is TView) as TView;
            if (view == null)
            {
                logger.LogError($"Cannot hide view because it is not spawned", LogCategory.UI);
                return;
            }

            await view.Close(options, cancellationToken, useAnimation);
            spawnedViews.Remove(view);
            DespawnView(view);
        }

        public TView GetVisualizedView<TView>() 
            where TView : MonoBehaviour, IView
        {
            TView view = spawnedViews.FirstOrDefault(view => view is TView) as TView;
            if (view == null)
            {
                logger.LogError($"Target view is not spawned", LogCategory.UI);
            }

            return view;
        }

        private async Task<TView> SpawnView<TView>() where TView : MonoBehaviour, IView
        {
            string assetName = typeof(TView).Name;
            SceneRootsHolder sceneRootsHolder = container.Resolve<SceneRootsHolder>();
            TView view = await assetLoadingService.InstantiateGameObjectForComponentAfterLoading<TView>(assetName, sceneRootsHolder.UIRootObject);
            view.transform.localPosition = Vector3.zero;
            return view;

        }

        private void DespawnView<TView>(TView viewInstance) where TView : MonoBehaviour, IView
        {
            assetLoadingService.DestroyAndUnloadObject(viewInstance);
        }
    }
}