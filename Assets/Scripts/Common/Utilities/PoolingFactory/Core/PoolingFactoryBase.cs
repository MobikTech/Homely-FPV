using System.Collections.Generic;
using FpvDroneSimulator.Common.Utilities.PoolingFactory.Abstr;
using UnityEngine;

namespace FpvDroneSimulator.Common.Utilities.PoolingFactory.Core
{
    public abstract class PoolingFactoryBase<TItemBase, TCreationOptions> : IPoolingFactory<TItemBase, TCreationOptions> 
        where TItemBase : MonoBehaviour, IPoolItem
        where TCreationOptions : ICreationOptions<TItemBase>
    {
        protected PoolingFactoryBase()
        {
            _poolOfItems = new Dictionary<string, Stack<TItemBase>>();
        }

        protected virtual int InitialStackCapacity => 300;
        private readonly Dictionary<string, Stack<TItemBase>> _poolOfItems;

        public virtual void Delete(TItemBase unit)
        {
            if (!_poolOfItems.ContainsKey(unit.GetItemTypeKey))
                _poolOfItems.Add(unit.GetItemTypeKey, new Stack<TItemBase>(InitialStackCapacity));
            
            _poolOfItems[unit.GetItemTypeKey].Push(unit);
            unit.gameObject.SetActive(false);
        }

        public virtual TItemBase Create(TCreationOptions creationOptions)
        {
            if (!_poolOfItems.ContainsKey(creationOptions.Prefab.GetItemTypeKey))
                _poolOfItems.Add(creationOptions.Prefab.GetItemTypeKey, new Stack<TItemBase>(InitialStackCapacity));

            TItemBase unit = _poolOfItems[creationOptions.Prefab.GetItemTypeKey].Count == 0 
                ? Object.Instantiate(creationOptions.Prefab)
                : _poolOfItems[creationOptions.Prefab.GetItemTypeKey].Pop();

            creationOptions.SetupCreationOptions(unit);
            OnCreationFinished(unit);
            unit.gameObject.SetActive(true);
            return unit;
        }

        protected virtual void OnCreationFinished(TItemBase item)
        {
            
        }
    }
}