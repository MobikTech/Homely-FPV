using System;
using System.Collections.Generic;
using UnityEngine;

namespace FpvDroneSimulator.Common.Utilities.AudioSystem
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "AudioSettings", order = 0)]
    public class AudioSettings : ScriptableObject
    {
        [SerializeField, Tooltip("Reserved ID's: 0-10_000")] private List<AudioEntitySettings> _soundEffects;
        [SerializeField, Tooltip("Reserved ID's: 10_000-20_000")] private List<AudioEntitySettings> _ambientAudio;
        [SerializeField, Tooltip("Reserved ID's: 20_000-30_000")] private List<AudioEntitySettings> _backgroundMusic;

        public List<AudioEntitySettings> SoundEffects => _soundEffects;

        public List<AudioEntitySettings> AmbientAudio => _ambientAudio;

        public List<AudioEntitySettings> BackgroundMusic => _backgroundMusic;
    }

    [Serializable]
    public struct AudioEntitySettings
    {
        public AudioClip AudioClip;
        public uint AudioEntityID;
        public string Description;
        public AudioType AudioType;
        public bool IsLooping;
        public float Volume;
    }

    public enum AudioType
    {
        SoundFX,
        Ambient,
        BackgroundMusic
    }
}