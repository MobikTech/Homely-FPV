using UnityEngine;

namespace FpvDroneSimulator.Common.Utilities.PoolingFactory.Abstr
{
    public interface ICreationOptions<TItem> where TItem : MonoBehaviour, IPoolItem
    {
        public TItem Prefab { get; }
        public void SetupCreationOptions(TItem item);
    }
}