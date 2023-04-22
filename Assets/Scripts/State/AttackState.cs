using UnityEngine;

namespace State
{
    public class AttackState: StateMachineBehaviour
    {
        [SerializeField] private int _damage;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var player = GameObject.FindWithTag("Player");
            
            animator.transform.LookAt(player.transform);

            var playerCpnt = player.GetComponentInChildren<PlayerCpnt>();
            playerCpnt.TakeDamage(_damage);

            if (playerCpnt.Health <= 0)
            {
                animator.SetBool("isPatrolling", false);
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttacking", false);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isAttacking", false);
        }
    }
}