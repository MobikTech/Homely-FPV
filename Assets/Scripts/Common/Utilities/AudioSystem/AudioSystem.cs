using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ILogger = FpvDroneSimulator.Common.Utilities.Logging.ILogger;
using Object = UnityEngine.Object;

namespace FpvDroneSimulator.Common.Utilities.AudioSystem
{
    public class AudioSystem : IAudioSystem
    {
        public AudioSystem(ILogger logger, AudioSettings audioSettings)
        {
            _logger = logger;
            _audioSettings = audioSettings;
            _audioSourcesPool = new AudioSourcesPool(CreateAudioSystemTransform());
            _activeLoopedAudioEntities = new List<AudioEntity>();
        }

        private readonly ILogger _logger;
        private readonly AudioSettings _audioSettings;
        private readonly AudioSourcesPool _audioSourcesPool;
        private readonly List<AudioEntity> _activeLoopedAudioEntities;

        public async void PlayAudio(uint audioID, SpatialAudioParameters? spatialAudioParameters = null)
        {
            if (!TryFindAudioEntitySettings(audioID, out AudioEntitySettings? audioEntitySettings))
            {
                return;
            }

            var audioEntity = spatialAudioParameters.HasValue
                ? new AudioEntity(_audioSourcesPool.GetAudioSource(), audioEntitySettings!.Value, spatialAudioParameters.Value)
                : new AudioEntity(_audioSourcesPool.GetAudioSource(), audioEntitySettings!.Value);

            if (spatialAudioParameters.HasValue)
            {
                _activeLoopedAudioEntities.Add(audioEntity);
            }

            await audioEntity.Play();
            if (audioEntity.AudioSource != null)
            {
                _audioSourcesPool.ReleaseAudioSource(audioEntity.AudioSource);
            }
        }

        public void StopAudio(uint audioID)
        {
            AudioEntity entity = _activeLoopedAudioEntities.Find(audioEntity => audioEntity.AudioEntityID == audioID);
            if (entity != null)
            {
                entity.StopPlaying();
            }
        }

        private bool TryFindAudioEntitySettings(uint soundID, out AudioEntitySettings? audioEntitySettings)
        {
            IEnumerable<AudioEntitySettings> allAudioEntitySettings = _audioSettings.SoundEffects.Concat(_audioSettings.AmbientAudio.Concat(_audioSettings.BackgroundMusic));
            audioEntitySettings = new AudioEntitySettings();

            try
            {
                audioEntitySettings = allAudioEntitySettings.First(settings => settings.AudioEntityID == soundID);
                return true;
            }
            catch (Exception _)
            {
                _logger.LogError($"Audio with ID {soundID} was not registered", nameof(AudioSystem));
                return false;
            }
        }

        private Transform CreateAudioSystemTransform()
        {
            GameObject systemGameObject = new GameObject("[AudioSystem]");
            Object.DontDestroyOnLoad(systemGameObject);
            return systemGameObject.transform;
        }
    }
}