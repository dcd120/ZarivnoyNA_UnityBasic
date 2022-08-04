using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class myHero : MonoBehaviour
{
    private Vector3 m_Movement;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private AudioSource m_FootSteps;
    private Health m_Health;
    private bool isGrounded;

    [SerializeField] public string m_HeroName = "Leonard";
    [SerializeField] public string m_groundTag = "Titles";
    [SerializeField] public TMP_Text m_HeroInfoUI;
    //private TMP_Text m_HeroInfo;

    [SerializeField] public float turnSpeed = 20f;
    [SerializeField] public float jumpPower = 1.5f;
    private Quaternion m_Rotation = Quaternion.identity;

    [SerializeField] public Component fire;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_FootSteps = GetComponent<AudioSource>();
        m_Health = GetComponent<Health>();
        isGrounded = true;
        //m_HeroInfo = m_HeroInfoUI.GetComponent<TextMeshPro>();
    }

    private void UpdateInfo()
    {
        m_HeroInfoUI.text = $"{m_HeroName}\nHP:{m_Health.GetHealthUIString()}";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float jump = Input.GetAxis("Jump");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool is_jump = !Mathf.Approximately(jump, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_FootSteps.isPlaying)
            {
                m_FootSteps.Play();
            }
        }
        else
        {
            m_FootSteps.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        if ((is_jump) && (isGrounded))
        {
            m_Rigidbody.AddForce(0, jumpPower, 0, ForceMode.Impulse);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(fire, m_Rigidbody.position + transform.forward + transform.up, m_Rigidbody.rotation);
            // Создаем _mine в точке _mineSpawnPlace
        }

    }
    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
        //Debug.Log($"Hero rot:{m_Rotation}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = collision.gameObject.CompareTag(m_groundTag);
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = !collision.gameObject.CompareTag(m_groundTag);
    }
}
