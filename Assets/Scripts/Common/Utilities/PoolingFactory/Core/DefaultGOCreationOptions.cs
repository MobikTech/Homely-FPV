using FpvDroneSimulator.Common.Utilities.PoolingFactory.Abstr;
using UnityEngine;

namespace FpvDroneSimulator.Common.Utilities.PoolingFactory.Core
{
    public struct DefaultGOCreationOptions<TItem> : ICreationOptions<TItem> where TItem : MonoBehaviour, IPoolItem
    {
        public TItem Prefab { get; }
        public Vector3 SpawnPoint;
        public Quaternion Rotation;
        public Transform Parent;

        public DefaultGOCreationOptions(TItem prefab, Vector3 spawnPoint, Quaternion rotation, Transform parent)
        {
            Prefab = prefab;
            SpawnPoint = spawnPoint;
            Rotation = rotation;
            Parent = parent;
        }

        public void SetupCreationOptions(TItem item)
        {
            item.transform.position = SpawnPoint;
            item.transform.rotation = Rotation;
            item.transform.parent = Parent;
        }
    }
}