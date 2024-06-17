using System.Collections.Generic;
using UnityEngine;

namespace FpvDroneSimulator.Common.Utilities.AudioSystem
{
    public class AudioSourcesPool
    {
        public AudioSourcesPool(Transform poolItemsParent)
        {
            _poolItemsParent = poolItemsParent;
            _freeAudioSourcesStack = new Stack<AudioSource>();
            CreatePrefab();
        }

        private readonly Transform _poolItemsParent;
        private readonly Stack<AudioSource> _freeAudioSourcesStack;
        private AudioSource _itemPrefab;
        private int _itemsCounter = 0;


        public AudioSource GetAudioSource()
        {
            AudioSource audioSource = TryGetAudioSource();
            audioSource.gameObject.SetActive(true);
            return audioSource;
        }

        public void ReleaseAudioSource(AudioSource audioSource)
        {
            audioSource.clip = null;
            audioSource.loop = false;
            audioSource.volume = 0.5f;
            audioSource.spatialBlend = 0f;
            audioSource.transform.parent = _poolItemsParent;
            audioSource.gameObject.SetActive(false);
            _freeAudioSourcesStack.Push(audioSource);
        }

        private void CreatePrefab()
        {
            _itemPrefab = new GameObject().AddComponent<AudioSource>();
            _itemPrefab.name = "AudioSourcePrefab";
            _itemPrefab.transform.parent = _poolItemsParent;
            _itemPrefab.gameObject.SetActive(false);
        }

        private AudioSource TryGetAudioSource()
        {
            if (_freeAudioSourcesStack.TryPop(out var audioSourceResult))
            {
                return audioSourceResult;
            }

            audioSourceResult = Object.Instantiate(_itemPrefab, _poolItemsParent);
            audioSourceResult.name = "AudioSourceItem_" + _itemsCounter++;
            audioSourceResult.playOnAwake = false;

            return audioSourceResult;
        }
    }
}