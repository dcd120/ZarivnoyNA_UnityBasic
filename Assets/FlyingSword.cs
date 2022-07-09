using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSword : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    CapsuleCollider m_Collider;
    Vector3 addV;
    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<CapsuleCollider>();
        m_Rigidbody = GetComponent<Rigidbody>();
        addV = new Vector3(0, 0.2f, 0);
        m_Rigidbody.AddForce(transform.forward + addV, ForceMode.Impulse);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (transform.position.y < 0.05)
        {
            m_Collider.isTrigger = false;
        }
    }
}
