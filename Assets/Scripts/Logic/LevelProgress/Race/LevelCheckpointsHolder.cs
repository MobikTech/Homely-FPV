using System.Collections.Generic;
using FpvDroneSimulator.Logic.LevelProgress.Models;
using UnityEngine;

namespace FpvDroneSimulator.Logic.LevelProgress
{
    public class LevelCheckpointsHolder : MonoBehaviour
    {
        [field:SerializeField] public List<LapInfo> LevelLaps { get; set; }
        [field:SerializeField] public GameObject CheckpointsObjectsParent { get; set; }
    }
}