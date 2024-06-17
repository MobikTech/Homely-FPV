using System;
using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.Common.Extensions;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.UI.Core
{
    public abstract class ViewBase : MonoBehaviourCached, IView
    {
        [Inject] protected IViewVisualizer viewVisualizer;

        public event Action<IView> OnOpened;
        public event Action<IView> OnClosed;

        protected IOptions viewOpenOptions;
        protected IOptions viewCloseOptions;
        
        [SerializeField] private Animator animator;
        private static readonly int AnimationOpenTriggerHash = Animator.StringToHash("Open");
        private static readonly int AnimationCloseTriggerHash = Animator.StringToHash("Close");

        public async Task Open<TOptions>(TOptions options, CancellationToken cancellationToken, bool useAnimation = false) 
            where TOptions : IOptions
        {
            viewOpenOptions = options;
            await Initialize();
            if (useAnimation)
            {
                await WaitOpenAnimation(cancellationToken);
            }
            OnOpened?.Invoke(this);
        }

        public async Task Close<TOptions>(TOptions options, CancellationToken cancellationToken, bool useAnimation = false) 
            where TOptions : IOptions
        {
            viewCloseOptions = options;
            Dispose();
            if (useAnimation)
            {
                await WaitCloseAnimation(cancellationToken);
            }
            OnClosed?.Invoke(this);
        }

        protected abstract Task Initialize();
        protected abstract void Dispose();

        private async Task WaitOpenAnimation(CancellationToken cancellationToken)
        {
            if (animator == null)
            {
                return;
            }

            animator?.SetTrigger(AnimationOpenTriggerHash);
            await animator.WaitNextStateEnd(cancellationToken);
        }

        private async Task WaitCloseAnimation(CancellationToken cancellationToken)
        {
            if (animator == null)
            {
                return;
            }

            animator?.SetTrigger(AnimationCloseTriggerHash);
            await animator.WaitNextStateEnd(cancellationToken);
        }
    }
}
