#nullable enable
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FpvDroneSimulator.Common.Utilities.SceneLoading
{
    public class SceneSwitcher : ISceneSwitcher
    {
        public event SceneLoadingStart? SceneLoadingStarted;
        public event EventCompletion? SceneLoadingEnded;

        public async Task<LoadingResult> SwitchSceneAsync(string name)
        {
            LoadingResult loadingResult = new LoadingResult();
            LoadingProgress loadingProgress = new LoadingProgress();

            SceneLoadingStarted?.Invoke(loadingProgress);

            if (IsSceneAlreadyActive(name))
            {
                loadingResult.IsCompletedSuccessfully = false;
                loadingResult.Message = $"Scene \"{name}\" already active";
            }
            else
            {
                try
                {
                    AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(name);

                    while (loadingOperation.progress < 1.0f)
                    {
                        loadingProgress.ProgressNormalized = loadingOperation.progress;
                        await Task.Delay(100);
                    }

                    loadingResult.Message = null;
                    loadingResult.IsCompletedSuccessfully = false;
                }
                catch (Exception e)
                {
                    loadingResult.Message = e.Message;
                    loadingResult.IsCompletedSuccessfully = false;
                }
            }


            loadingResult.SceneName = name;

            SceneLoadingEnded?.Invoke(loadingResult);

            return loadingResult;
        }

        private bool IsSceneAlreadyActive(string sceneName)
        {
            return SceneManager.GetActiveScene().name == sceneName;
        }
    }
}