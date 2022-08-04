using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class myEnemy : MonoBehaviour
{
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;

    [SerializeField] public myEnemyRangedWeapon m_EnemyRangedWeapon;
    [SerializeField] public myEnemyMeleeWeapon m_EnemyMeleeWeapon;

    [SerializeField] public float maxSpeed = 10;

    [SerializeField] public Transform m_Target;

    // HACK: кривая реализация управления телом от информации из "сканера", нарушение инкапсуляции
    public Quaternion m_Rotation = Quaternion.identity;
    public bool m_isPlayerVisible;
    public Vector3 direction;

    [SerializeField] public bool have_ranged_weapon = false;
    [SerializeField] public bool have_mele_weapon = false;

    private float m_ReadyFire = 0; // перезарядка, готов ли юнит стрелять
    private float m_ReadySlash = 0; // перезарядка, готов ли юнит бить

    [SerializeField] public float m_CoolDownRanged = 5f;
    [SerializeField] public float m_CoolDownMele = 3f;
    [SerializeField] public float m_MeleeRange = 1f;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_isPlayerVisible = false;
    }

    void Ranged_Attack()
    {
        if (m_ReadyFire <= 0)
        {
            Fire();
            m_ReadyFire = m_CoolDownRanged * 1000;
        }
        else 
        {
            m_ReadyFire -= Time.fixedTime;
        }
    }

    void Slash()
    {
        Debug.Log($"Попытка ударить мечом! {m_ReadyFire}");
        m_Animator.SetTrigger("Slash");
        m_EnemyMeleeWeapon.Slash();
    }

    void Fire()
    {
        Debug.Log($"Попытка открыть огонь! {m_ReadyFire}");
        m_EnemyRangedWeapon.Fire();
    }
    void Mele_Attack()
    {
        if (m_ReadySlash <= 0)
        {
            // проверим, достанем ли мы ударом до врага
            if (Vector3.Distance(m_Target.position, transform.position) > m_MeleeRange) return;
            Slash();
            m_ReadySlash = m_CoolDownMele * 1000;
        }
        else
        {
            m_ReadySlash -= Time.fixedTime;
        }
    }
    void OnAnimatorMove()
    {
        if (m_isPlayerVisible)
        {
            m_Animator.SetBool("IsWalking", true);
            m_Rigidbody.MovePosition(m_Rigidbody.position + direction * m_Animator.deltaPosition.magnitude * maxSpeed / 100);
            m_Rigidbody.MoveRotation(m_Rotation);
            // если у врага есть оружие, надо попытаться им ударить
            if (have_ranged_weapon) Ranged_Attack();
            if (have_mele_weapon) Mele_Attack();
        }
        else 
        {
            m_Animator.SetBool("IsWalking", false);
        }
    }
}
