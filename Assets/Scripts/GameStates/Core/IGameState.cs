using FpvDroneSimulator.Common.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FpvDroneSimulator.GameStates.Core
{
    public interface IGameState : IDisposable
    {
        public Task Run(CancellationToken cancellationToken, IOptions options = null);
    }
}