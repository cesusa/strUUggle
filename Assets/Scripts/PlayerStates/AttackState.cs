using UnityEngine;
using UnityEngine.Animations;

namespace PlayerStates
{
    public class AttackState: StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            animator.GetComponent<PlayerCpnt>().isAttacking = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<PlayerCpnt>().isAttacking = false;
        }
    }
}