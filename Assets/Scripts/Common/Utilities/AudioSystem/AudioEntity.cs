using System.Threading.Tasks;
using UnityEngine;

namespace FpvDroneSimulator.Common.Utilities.AudioSystem
{
    internal class AudioEntity
    {
        internal AudioEntity(AudioSource audioSource, AudioEntitySettings audioEntitySettings)
        {
            _audioSource = audioSource;
            _audioSource.clip = audioEntitySettings.AudioClip;
            _audioSource.loop = audioEntitySettings.IsLooping;
            _audioSource.volume = audioEntitySettings.Volume;
            AudioEntityID = audioEntitySettings.AudioEntityID;
        }

        internal AudioEntity(AudioSource audioSource, AudioEntitySettings audioEntitySettings, SpatialAudioParameters spatialAudioParameters) : this(audioSource, audioEntitySettings)
        {
            _soundSourceTransform = spatialAudioParameters.SoundSource;
            _audioSource.spatialBlend = 1f;
            _isSpatial = true;
        }

        internal AudioSource AudioSource => _audioSource;
        internal uint AudioEntityID { get; }

        private readonly AudioSource _audioSource;
        private readonly bool _isSpatial = false;
        private readonly Transform _soundSourceTransform;


        internal async Task Play()
        {
            _audioSource.Play();
            while (CheckAudioIsPlaying())
            {
                if (_isSpatial)
                {
                    _audioSource.transform.position = _soundSourceTransform.position;
                }
                await Task.Yield();
            }
        }

        internal void StopPlaying()
        {
            _audioSource.Stop();
        }

        private bool CheckAudioIsPlaying()
        {
            return _audioSource != null && _audioSource.isPlaying && _audioSource.gameObject.activeSelf &&
                   (!_isSpatial || (_isSpatial && _soundSourceTransform != null));
        }
    }
}