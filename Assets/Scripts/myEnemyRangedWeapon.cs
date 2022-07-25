using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEnemyRangedWeapon : MonoBehaviour
{
    [SerializeField] public Component fire;
    private Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fire()
    {
        Instantiate(fire, m_Rigidbody.position + transform.forward + transform.up, m_Rigidbody.rotation);
    }
}
