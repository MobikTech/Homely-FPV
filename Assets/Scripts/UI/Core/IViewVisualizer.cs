using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using UnityEngine;

namespace FpvDroneSimulator.UI.Core
{
    public interface IViewVisualizer
    {
        public Task<TView> Visualize<TView, TOptions>(TOptions options, CancellationToken cancellationToken, bool useAnimation = false)
            where TView : MonoBehaviour, IView
            where TOptions : IOptions;

        public Task Hide<TView, TOptions>(TOptions options, CancellationToken cancellationToken, bool useAnimation = false)
            where TView : MonoBehaviour, IView
            where TOptions : IOptions;

        public TView GetVisualizedView<TView>()
            where TView : MonoBehaviour, IView;
    }
}