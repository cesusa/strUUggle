using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace State
{
    public class ChaseState: StateMachineBehaviour
    {
        private Transform _playerTransform;
        private NavMeshAgent _agent;
        public float AttackRange = 0.75f;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            _agent = animator.GetComponent<NavMeshAgent>();
            _agent.speed = animator.GetComponent<EnemyAttributes>().ChasingSpeed;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            try
            {
                _agent.SetDestination(_playerTransform.position);
            }
            catch (Exception)
            {
                return;
            }

            if (Vector3.Distance(animator.transform.position, _playerTransform.position) <= AttackRange)
            {
                _agent.SetDestination(animator.transform.position);
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttacking", true);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    }
}