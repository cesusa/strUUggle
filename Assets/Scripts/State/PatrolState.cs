using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace State
{
    public class PatrolState : StateMachineBehaviour
    {
        float timer;
        private List<Transform> allWaypoints = new List<Transform>();
        private List<Transform> waypoints;
        NavMeshAgent agent;
        public LayerMask PlayerLayerMask;
        public float DetectArea;
        public Transform DetectAreaCenterTransform; // You should specify the transform if you want to search the player for specific area.
    
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent = animator.GetComponent<NavMeshAgent>();
            agent.speed = animator.GetComponent<EnemyAttributes>().PatrollingSpeed;
            timer = 0;

            allWaypoints = new List<Transform>(animator.GetComponent<EnemyWaypoints>().Waypoints);

            waypoints = new List<Transform>(allWaypoints);
            var waypoint = waypoints[Random.Range(0, waypoints.Count)];
            waypoints.Remove(waypoint);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var center = DetectAreaCenterTransform == null
                ? animator.transform.position
                : DetectAreaCenterTransform.position;
            
            var isPlayerCloseEnoughToChase = Physics.CheckSphere(center, DetectArea, PlayerLayerMask);

            if (isPlayerCloseEnoughToChase)
            {
                animator.SetBool("isChasing", true);
                animator.SetBool("isPatrolling", false);
            }
            

            if (agent.isStopped)
            {
                var waypoint = waypoints[Random.Range(0, waypoints.Count)];
                waypoints.Remove(waypoint);
                agent.SetDestination(waypoint.position);
            }
            
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (waypoints.Count <= 0)
                {
                    waypoints = new List<Transform>(allWaypoints);
                }

                var waypoint = waypoints[Random.Range(0, waypoints.Count)];
                waypoints.Remove(waypoint);
            
                agent.SetDestination(waypoint.position);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent.SetDestination(agent.transform.position);
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }

    public enum ChaseType
    {
        Area,
        Distance
    }
}
