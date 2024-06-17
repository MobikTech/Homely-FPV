using UnityEngine;

namespace FpvDroneSimulator.Common.Core
{
    public class MonoBehaviourCached : MonoBehaviour
    {
        private string? _cachedName;
        private GameObject? _cachedGameObject;
        private Transform? _cachedTransform;

        
        public new string name
        {
            get => _cachedName ??= base.name;
            set
            {
                if (string.CompareOrdinal(_cachedName, value) == 0) 
                    return;

                _cachedName ??= base.name = value;
            }
        }
        public new GameObject gameObject => _cachedGameObject ??= base.gameObject;
        public new Transform transform => _cachedTransform ??= base.transform;
    }
}