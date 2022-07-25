using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPOV : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public myEnemy enemy;

    private Vector3 m_Movement;
    [SerializeField]  public Animator m_Animator;
    [SerializeField]  public Rigidbody m_Rigidbody;

    [SerializeField] public float turnSpeed = 60f;
    private Quaternion m_Rotation = Quaternion.identity;
    private Vector3 direction;

    private bool m_IsPlayerInRange;
    private bool m_isPlayerVisible;

    private float m_lostContact = 0;
    [SerializeField] public float m_lostTime = 2f;
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
            m_lostContact = m_lostTime;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_lostContact = m_lostTime;

        }
    }

    void Update()
    {
        if (m_IsPlayerInRange|| m_lostContact>0)
        {
            if (!m_IsPlayerInRange) m_lostContact -= Time.fixedTime;

            direction = player.position - transform.parent.position;
            Ray ray = new Ray(transform.parent.position + Vector3.up, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                Debug.DrawRay(transform.parent.position + Vector3.up, direction, Color.red, 1);
                if (raycastHit.collider.transform == player)
                {
                    m_isPlayerVisible = true;
                    // пусть солдат тогда бежит на игрока


                    Vector3 desiredForward = Vector3.RotateTowards(transform.parent.forward, direction, turnSpeed * Time.deltaTime, 0f);
                    m_Rotation = Quaternion.LookRotation(desiredForward);

                    enemy.m_isPlayerVisible = m_isPlayerVisible;
                    enemy.m_Rotation = m_Rotation;
                    enemy.direction = direction;
                    enemy.m_Target = player;

                }
                else
                {
                    m_isPlayerVisible = false;
                    enemy.m_isPlayerVisible = m_isPlayerVisible;
                }
            }
        }
        else if (!m_IsPlayerInRange&& m_lostContact<=0)
        {
            m_lostContact = 0;
            m_IsPlayerInRange = false;
            m_isPlayerVisible = false;
            enemy.m_isPlayerVisible = m_isPlayerVisible;
        }
    }

}
