using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSword : MonoBehaviour
{
    CapsuleCollider m_CapsuleCollider;
    Rigidbody m_Rigidbody;

    [SerializeField] AudioSource throwSound;
    [SerializeField] AudioSource boomSound;

    [SerializeField] float lifeTime = 10f;
    [SerializeField] float deathTime = 3f;

    float createTime;
    float boomTime;
    Vector3 addV;
    bool m_isTrigged;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
        addV = new Vector3(0, 0.3f, 0);
        m_Rigidbody.AddForce(transform.forward + addV, ForceMode.Impulse);
        throwSound.Play();
        createTime = Time.fixedTime;
        m_isTrigged = false;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Enemy")&&(!m_isTrigged))
        {
            other.gameObject.SetActive(false);
            boomSound.Play();
            boomTime = Time.fixedTime;
            m_isTrigged = true;
            m_CapsuleCollider.isTrigger = false;
        }
    }

    private void FixedUpdate()
    {
        if (m_isTrigged)
        {
            if (boomTime + deathTime > Time.fixedTime)
            {
                transform.gameObject.SetActive(false);
            }
        } else {
            if (createTime + lifeTime > Time.fixedTime)
            {
                m_CapsuleCollider.isTrigger = false;
                m_isTrigged = true;
                boomSound.Play();
            }
        }
    }
}
