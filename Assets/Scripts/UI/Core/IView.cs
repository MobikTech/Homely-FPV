using System;
using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;

namespace FpvDroneSimulator.UI.Core
{
    public interface IView
    {
        public event Action<IView> OnOpened;
        public event Action<IView> OnClosed;

        public Task Open<TOptions>(TOptions options, CancellationToken cancellationToken, bool useAnimation = false) 
            where TOptions : IOptions;

        public Task Close<TOptions>(TOptions options, CancellationToken cancellationToken, bool useAnimation = false)
            where TOptions : IOptions;
    }
}