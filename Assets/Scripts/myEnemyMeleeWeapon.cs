using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEnemyMeleeWeapon : MonoBehaviour
{
    [SerializeField] public CapsuleCollider m_BladeCollider;
    private Rigidbody m_Rigidbody;
    private bool m_BladeIsActive = false;
    private float m_TimeToStop = 0f;
    [SerializeField] public float m_SlashTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        m_BladeCollider.enabled = false;
        m_BladeIsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_BladeIsActive)
        { 
            m_TimeToStop -= Time.fixedTime;
            if (m_TimeToStop <= 0f)
            {
                m_BladeCollider.enabled = false;
                m_BladeIsActive = false;
            }
        }
    }
    public void Slash()
    {
        m_BladeCollider.enabled = true;
        m_BladeIsActive = true;
        m_TimeToStop = m_SlashTime*1000;
    }
}
