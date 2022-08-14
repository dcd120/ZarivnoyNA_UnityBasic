using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EI2
{
    public class myHero : MonoBehaviour
    {
        private Vector3 m_Movement;
        private Vector3 m_LookUp;
        private Animator m_Animator;
        private Rigidbody m_Rigidbody;
        private AudioSource m_FootSteps;
        private Health m_Health;
        private bool isGrounded;

        bool isWalking = false;

        /// <summary>
        /// готовность открыть огонь, true - готов открыть, false - не готов
        /// </summary>
        private bool readyToFire;
        private float timeToRechargeFire;

        [SerializeField] public string m_HeroName = "Leonard";
        [SerializeField] public string m_groundTag = "Titles";
        [SerializeField] public string m_isWalkingKey = "IsWalking";
        [SerializeField] public TMP_Text m_HeroInfoUI;
        [SerializeField] public Transform m_Pointer;

        [SerializeField] public float m_LowerSteps = 0.75f;
        [SerializeField] public float m_StrongSteps = 1.5f;

        /// <summary>
        /// Время "остывания" метательного оружия героя
        /// </summary>
        [SerializeField] public float rechargeTime = 1f;
        //private TMP_Text m_HeroInfo;

        [SerializeField] public float turnSpeed = 20f; // скорость поворота героя
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
            readyToFire = true;
            timeToRechargeFire = 0f;

            //m_HeroInfo = m_HeroInfoUI.GetComponent<TextMeshPro>();
        }

        private void UpdateInfo()
        {
            m_HeroInfoUI.text = $"{m_HeroName}\nHP:{m_Health.GetHealthUIString()}";
        }

        private void UpdateMove()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            m_Movement.Set(horizontal, 0f, vertical);
            Debug.DrawRay(transform.position, m_Movement * 5, Color.red);

            m_Movement += transform.forward * 0.001f;
            // надо скорректировать относительно поворота персонажа!
            Debug.Log($"horizontal: {horizontal} / vertical: {vertical}");
            Debug.DrawRay(transform.position, m_Movement * 3, Color.blue);

            m_Movement.Normalize();
            Debug.DrawRay(transform.position, m_Movement, Color.yellow, 0.5f);

            bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
            bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

            isWalking = hasHorizontalInput || hasVerticalInput;
            m_Animator.SetBool(m_isWalkingKey, isWalking);


        }

        private void Jumping()
        {
            float jump = Input.GetAxis("Jump");
            bool is_jump = !Mathf.Approximately(jump, 0f);

            if ((is_jump) && (isGrounded))
            {
                m_Rigidbody.AddForce(0, jumpPower, 0, ForceMode.Impulse);
                Debug.Log("Jump!");
            }
        }

        private void Fire()
        {
            if (!readyToFire)
            {
                if (Time.fixedTime > rechargeTime + timeToRechargeFire)
                {
                    readyToFire = true;
                }
            }

            if (Input.GetButtonDown("Fire1") && (readyToFire))
            {
                //Debug.Log($"m_LookUp: {m_LookUp} / Hero forward: {transform.forward} / desiredForward: {desiredForward}");
                Instantiate(fire, m_Rigidbody.position + transform.forward + transform.up, m_Rigidbody.rotation);
                readyToFire = false;
                timeToRechargeFire = Time.fixedTime;
                // Создаем _mine в точке _mineSpawnPlace
            }
        }

        private void RotateUpdate()
        {
            //Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            //m_Rotation = Quaternion.LookRotation(desiredForward);
            // сделаем слежение за мышкой
            //m_Movement.Set(Input.mousePosition.x, 0f, Input.mousePosition.z);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(transform.position, Input.mousePosition, Color.blue);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                m_LookUp = hit.point;
                m_LookUp.y = transform.up.y;
                m_Pointer.position = m_LookUp;
            }
            //Debug.DrawRay(m_Rigidbody.position, m_LookUp, Color.red);
            //Debug.DrawRay(m_Rigidbody.position, transform.forward * 5, Color.blue);

            m_LookUp = m_LookUp - m_Rigidbody.position;
            //Debug.DrawRay(m_Rigidbody.position, m_LookUp, Color.green, 0.5f);

            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_LookUp, turnSpeed * Time.deltaTime, 0f);
            //Debug.DrawRay(transform.position, desiredForward, Color.magenta, 1f, false);

            m_Rotation = Quaternion.LookRotation(desiredForward);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateInfo();
            UpdateMove();
            RotateUpdate();
            Jumping();
            Fire();
            PlayAudio();
        }

        private void PlayAudio()
        {
            if (isWalking)
            {
                if (!m_FootSteps.isPlaying)
                {
                    m_FootSteps.Play();
                    StartCoroutine(routine: PitchSwitcher());
                }
            }
            else
            {
                if (m_FootSteps.isPlaying)
                {
                    m_FootSteps.Stop();
                    //StopCoroutine(routine: PitchSwitcher());
                }
            }
        }

        // добавим усиление громкости шагов, при заряженном оружии
        private IEnumerator PitchSwitcher()
        {
            while (m_FootSteps.isPlaying)
            {
                m_FootSteps.pitch = readyToFire ? m_StrongSteps : m_LowerSteps;
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
        void OnAnimatorMove()
        {
            //Debug.DrawRay(m_Rigidbody.position, m_Rigidbody.position + m_Movement, Color.black);
            
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
}
