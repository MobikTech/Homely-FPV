using FpvDroneSimulator.Logic;
using UnityEngine;

namespace FpvDroneSimulator.Services.LevelInfoHolder
{
    public interface ILevelInfoHolder
    {
        public GameObject LevelInstance { get; set; }
        public GameObject DroneInstance { get; set; }
        public GameLevel GameLevel { get; set; }
        public GameLevelMode GameLevelMode { get; set; }
    }
}