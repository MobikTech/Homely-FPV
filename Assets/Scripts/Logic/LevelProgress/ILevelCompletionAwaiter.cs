using System;
using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.GeneralInterfaces;

namespace FpvDroneSimulator.Logic.LevelProgress
{
    public interface ILevelCompletionAwaiter : IInitializable, IDisposable
    {
        public Task WaitLevelCompletion(CancellationToken cancellationToken);
    }
}