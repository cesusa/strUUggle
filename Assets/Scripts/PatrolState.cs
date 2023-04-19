using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{
    float timer;
    private List<Transform> allWaypoints = new List<Transform>();
    private List<Transform> waypoints;
    NavMeshAgent agent;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        timer = 0;

        GameObject go = GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in go.transform)
        {
            allWaypoints.Add(t);
        }

        waypoints = new List<Transform>(allWaypoints);
        var waypoint = waypoints[Random.Range(0, waypoints.Count)];
        waypoints.Remove(waypoint);

        agent.SetDestination(waypoint.position);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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

        timer += Time.deltaTime;
        if (timer > 5)
           animator.SetBool("isPatrolling", false);
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
