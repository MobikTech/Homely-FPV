using System.Threading.Tasks;
using UnityEngine;

namespace FpvDroneSimulator.Services.AssetLoadingService
{
    public interface IAssetLoadingService
    {
        public Task<TAsset> LoadAsset<TAsset>(string assetId) 
            where TAsset : Object;
        
        public void UnloadAsset<TAsset>(TAsset asset) 
            where TAsset : Object;

        public Task<GameObject> InstantiateGameObjectAfterLoading(string assetId, Transform parent, Vector3 position, Quaternion rotation, bool inject = true);
        public Task<GameObject> InstantiateGameObjectAfterLoading(string assetId, Transform parent = null, bool inject = true);

        public Task<TComponent> InstantiateGameObjectForComponentAfterLoading<TComponent>(string assetId, Transform parent, Vector3 position, Quaternion rotation, bool inject = true)
            where TComponent : Component;

        public Task<TComponent> InstantiateGameObjectForComponentAfterLoading<TComponent>(string assetId, Transform parent = null, bool inject = true)
            where TComponent : Component;

        public void DestroyAndUnloadObject<TInstance>(TInstance instance)
            where TInstance : Object;
    }
}