using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace FpvDroneSimulator.Common.Extensions
{
    public static class AnimatorExtensions
    {
        public static async Task TriggerAndWaitStateEnd(this Animator animator, string animationTriggerName, CancellationToken cancellationToken, int layerIndex = 0)
        {
            int lastStateHash = animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash;
            animator.SetTrigger(animationTriggerName);
            await TaskExtensions.WaitWhile(() => animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash == lastStateHash, cancellationToken: cancellationToken);
            await TaskExtensions.WaitWhile(() => animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime < 0.99f, cancellationToken: cancellationToken);
        }

        public static async Task WaitCurrentStateEnd(this Animator animator, CancellationToken cancellationToken, int layerIndex = 0)
        {
            int currentStateHash = animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash;

            await TaskExtensions.WaitWhile(() =>
            {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);

                return stateInfo.shortNameHash == currentStateHash && !animator.IsInTransition(layerIndex) && stateInfo.normalizedTime < 1f;
            }, cancellationToken: cancellationToken);
        }
        public static async Task WaitTargetStateStart(this Animator animator, string animatorStateName, CancellationToken cancellationToken, int layerIndex = 0)
        {
            int targetStateHash = Animator.StringToHash(animatorStateName);
            await TaskExtensions.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash == targetStateHash, cancellationToken: cancellationToken);
        }

        public static async Task WaitTargetStateEnd(this Animator animator, string animatorStateName, CancellationToken cancellationToken, int layerIndex = 0)
        {
            await animator.WaitTargetStateStart(animatorStateName, cancellationToken);
            await animator.WaitCurrentStateEnd(cancellationToken);
        }

        public static async Task WaitNextStateEnd(this Animator animator, CancellationToken cancellationToken, int layerIndex = 0)
        {
            await animator.WaitNextStateStart(cancellationToken);
            await animator.WaitCurrentStateEnd(cancellationToken);
        }

        public static async Task WaitNextStateStart(this Animator animator, CancellationToken cancellationToken, int layerIndex = 0)
        {
            int currentStateHash = animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash;
            await TaskExtensions.WaitWhile(() => animator.GetCurrentAnimatorStateInfo(layerIndex).shortNameHash == currentStateHash, cancellationToken: cancellationToken);
        }
    }
}