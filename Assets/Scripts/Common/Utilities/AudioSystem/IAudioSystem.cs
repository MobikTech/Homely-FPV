namespace FpvDroneSimulator.Common.Utilities.AudioSystem
{
    public interface IAudioSystem
    {
        public void PlayAudio(uint audioID, SpatialAudioParameters? spatialAudioParameters = null);
        public void StopAudio(uint audioID);
    }
}