using FpvDroneSimulator.Logic;
using UnityEngine;

namespace FpvDroneSimulator.Services.LevelInfoHolder
{
    public class LevelInfoHolder : ILevelInfoHolder
    {
        public GameObject LevelInstance { get; set; }
        public GameObject DroneInstance { get; set; }
        public GameLevel GameLevel { get; set; }
        public GameLevelMode GameLevelMode { get; set; }
    }
}