using UnityEngine;

namespace FpvDroneSimulator.Services.SceneUIRootHolder
{
    public class SceneRootsHolder : MonoBehaviour
    {
        [field: SerializeField] public Transform UIRootObject;
        [field: SerializeField] public Transform EnvironmentRootObject;
        [field: SerializeField] public Transform PlayerRootObject;
    }
}