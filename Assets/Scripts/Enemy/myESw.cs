using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myESw : MonoBehaviour
{
    [SerializeField] public CapsuleCollider m_BladeCollider;
    public void EnableAttack()
    {
        m_BladeCollider.enabled = true;
    }

    public void DisableAttack()
    {
        m_BladeCollider.enabled = false;
    }
}
