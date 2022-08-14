using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EI2
{
    public class EnemyPOV : MonoBehaviour
    {
        [SerializeField] public string playerTag = "Player";
        [SerializeField] private Transform target;
        private myEnemy enemy;

        private Vector3 m_Movement;
        [SerializeField] private Animator m_Animator;
        [SerializeField] private Rigidbody m_Rigidbody;
        [SerializeField] private NavMeshAgent navMeshAgent;

        [SerializeField] public float turnSpeed = 60f;
        private Quaternion m_Rotation = Quaternion.identity;
        private Vector3 direction;

        private bool m_IsPlayerInRange;
        private bool m_isPlayerVisible;

        private float m_lostContact = 0;
        [SerializeField] public float m_lostTime = 2f;
        void Start()
        {
            m_Animator = GetComponentInParent<Animator>();
            m_Rigidbody = GetComponentInParent<Rigidbody>();
            navMeshAgent = GetComponentInParent<NavMeshAgent>();
            enemy = GetComponentInParent<myEnemy>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == playerTag)
            {
                target = other.transform;
                m_IsPlayerInRange = true;
                m_lostContact = m_lostTime;
                navMeshAgent.enabled = false;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.transform.tag == playerTag)
            {
                m_lostContact = m_lostTime;

            }
        }

        void Update()
        {
            if (m_IsPlayerInRange || m_lostContact > 0)
            {
                if (!m_IsPlayerInRange) m_lostContact -= Time.fixedTime;

                direction = target.position - transform.parent.position;
                Ray ray = new Ray(transform.parent.position + Vector3.up, direction);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit))
                {
                    Debug.DrawRay(transform.parent.position + Vector3.up, direction, Color.red, 1);
                    if (raycastHit.collider.transform == target)
                    {
                        m_isPlayerVisible = true;
                        // пусть солдат тогда бежит на игрока


                        Vector3 desiredForward = Vector3.RotateTowards(transform.parent.forward, direction, turnSpeed * Time.deltaTime, 0f);
                        m_Rotation = Quaternion.LookRotation(desiredForward);

                        enemy.POV_SetTargetANDTransform(m_isPlayerVisible, m_Rotation, direction, target);
                    }
                    else
                    {
                        m_isPlayerVisible = false;
                        enemy.POV_SetTargetVisible(m_isPlayerVisible);
                        //enemy.m_isPlayerVisible = m_isPlayerVisible;
                    }
                }
            }
            else if (!m_IsPlayerInRange && m_lostContact <= 0)
            {
                m_lostContact = 0;
                m_IsPlayerInRange = false;
                m_isPlayerVisible = false;
                //enemy.m_isPlayerVisible = m_isPlayerVisible;
                navMeshAgent.enabled = true;
                enemy.POV_SetTargetVisible(m_isPlayerVisible);
            }
        }

    }
}
