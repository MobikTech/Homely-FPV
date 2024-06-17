using System.Collections.Generic;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Utilities.Logging;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using ILogger = FpvDroneSimulator.Common.Utilities.Logging.ILogger;

namespace FpvDroneSimulator.Services.AssetLoadingService
{
    public class AssetLoadingService : IAssetLoadingService
    {
        private readonly ILogger logger;
        private readonly DiContainer diContainer;
        private readonly Dictionary<int, InstanceInfo> trackedInstances = new();

        public AssetLoadingService(ILogger logger, DiContainer diContainer)
        {
            this.logger = logger;
            this.diContainer = diContainer;
        }

        public async Task<TAsset> LoadAsset<TAsset>(string assetId)
            where TAsset : Object
        {
            return await Addressables.LoadAssetAsync<TAsset>(assetId).Task;
        }

        public void UnloadAsset<TAsset>(TAsset asset)
            where TAsset : Object
        {
            Addressables.Release(asset);
        }

        public async Task<GameObject> InstantiateGameObjectAfterLoading(string assetId, Transform parent, Vector3 position, Quaternion rotation, bool inject = true)
        {
            GameObject asset = await Addressables.LoadAssetAsync<GameObject>(assetId).Task;
            GameObject instance = Object.Instantiate(asset, position, rotation, parent);
            trackedInstances.Add(instance.GetInstanceID(), new (asset, instance));
            if (inject)
            {
                diContainer.InjectGameObject(instance);
            }
            return instance;
        }

        public async Task<GameObject> InstantiateGameObjectAfterLoading(string assetId, Transform parent = null, bool inject = true)
        {
            return await InstantiateGameObjectAfterLoading(assetId, parent, Vector3.zero, Quaternion.identity, inject);
        }

        public async Task<TComponent> InstantiateGameObjectForComponentAfterLoading<TComponent>(string assetId, Transform parent, Vector3 position, Quaternion rotation, bool inject = true) 
            where TComponent : Component
        {
            GameObject asset = await Addressables.LoadAssetAsync<GameObject>(assetId).Task;
            TComponent instance = Object.Instantiate(asset, position, rotation, parent).GetComponent<TComponent>();
            trackedInstances.Add(instance.GetInstanceID(), new(asset, instance.gameObject));
            if (inject)
            {
                diContainer.InjectGameObject(instance.gameObject);
            }
            return instance;
        }

        public async Task<TComponent> InstantiateGameObjectForComponentAfterLoading<TComponent>(string assetId, Transform parent = null, bool inject = true) 
            where TComponent : Component
        {
            return await InstantiateGameObjectForComponentAfterLoading<TComponent>(assetId, parent, Vector3.zero, Quaternion.identity, inject);
        }

        public void DestroyAndUnloadObject<TInstance>(TInstance instance)
            where TInstance : Object
        {
            if (!trackedInstances.TryGetValue(instance.GetInstanceID(), out InstanceInfo trackedInstanceInfo))
            {
                logger.LogError($"Provided instance is not tracked as loaded asset instance", LogCategory.AssetLoading);
            }

            Object.Destroy(trackedInstanceInfo.Instance);
            Addressables.Release(trackedInstanceInfo.Asset);
        }

        private struct InstanceInfo
        {
            public readonly Object Asset;
            public readonly GameObject Instance;

            public InstanceInfo(Object asset, GameObject instance)
            {
                Asset = asset;
                Instance = instance;
            }
        }
    }
}