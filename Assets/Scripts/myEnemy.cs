using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEnemy : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    public Quaternion m_Rotation = Quaternion.identity;
    public bool m_isPlayerVisible;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_isPlayerVisible = false;
    }
    void OnAnimatorMove()
    {
        if (m_isPlayerVisible)
        {
            m_Animator.SetBool("IsWalking", true);
            m_Rigidbody.MovePosition(m_Rigidbody.position + direction * m_Animator.deltaPosition.magnitude);
            m_Rigidbody.MoveRotation(m_Rotation);
            //Debug.Log($"Enemy mP:{m_Rigidbody.position + direction * m_Animator.deltaPosition.magnitude}");
            //Debug.Log($"Enemy mR:{m_Rotation}");
        }
        else 
        {
            m_Animator.SetBool("IsWalking", false);
        }
    }
}
