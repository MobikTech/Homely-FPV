#nullable enable
namespace FpvDroneSimulator.Common.Utilities.SceneLoading
{
    public delegate void SceneLoadingStart(LoadingProgress loadingProgress);
    public delegate void EventCompletion(LoadingResult loadingResult);
}