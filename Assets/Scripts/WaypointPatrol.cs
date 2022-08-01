using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] public Transform[] waypoints;
    [SerializeField] private Animator m_Animator;

    private int m_CurrentWaypointIndex;

    void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        m_Animator = GetComponentInParent<Animator>();
        if (waypoints.Length == 0) return;
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        if (waypoints.Length == 0) return;
        if (!navMeshAgent.isActiveAndEnabled) return;
        m_Animator.SetBool("IsWalking", true);
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
