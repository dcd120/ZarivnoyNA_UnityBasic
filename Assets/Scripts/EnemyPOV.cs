using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPOV : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] myEnemy enemy;

    Vector3 m_Movement;
    [SerializeField]  Animator m_Animator;
    [SerializeField]  Rigidbody m_Rigidbody;

    [SerializeField] float turnSpeed = 60f;
    Quaternion m_Rotation = Quaternion.identity;
    Vector3 direction;

    bool m_IsPlayerInRange;
    bool m_isPlayerVisible;
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
            m_isPlayerVisible = false;
            enemy.m_isPlayerVisible = m_isPlayerVisible;
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            direction = player.position - transform.parent.position;
            Ray ray = new Ray(transform.parent.position + Vector3.up, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    m_isPlayerVisible = true;
                    // пусть солдат тогда бежит на игрока
                    

                    Vector3 desiredForward = Vector3.RotateTowards(transform.parent.forward, direction, turnSpeed * Time.deltaTime, 0f);
                    m_Rotation = Quaternion.LookRotation(desiredForward);

                    enemy.m_isPlayerVisible = m_isPlayerVisible;
                    enemy.m_Rotation = m_Rotation;
                    enemy.direction = direction;

                }
                else 
                {
                    m_isPlayerVisible = false;
                    enemy.m_isPlayerVisible = m_isPlayerVisible;
                }
            }
        }
    }

}
