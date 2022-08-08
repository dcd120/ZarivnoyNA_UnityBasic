using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EI2
{

    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]

    public class FlyingSword : MonoBehaviour
    {
        private CapsuleCollider m_CapsuleCollider;
        private Rigidbody m_Rigidbody;
        private Transform targetTransform;
        private float currentForce;
        private Vector3 new_Position;

        /// <summary>
        /// значение, на которое будет сдвинут объект от "взрыва" в первый кадр
        /// затухает на 0,5 каждый кадр
        /// </summary>
        [SerializeField] public float startingPower = 3f;
        [SerializeField] private float fadingOfPower = 0.5f;

        [SerializeField] public int damage = 1;

        [SerializeField] public AudioSource throwSound;
        [SerializeField] public AudioSource boomSound;

        [SerializeField] public float lifeTime = 10f;
        [SerializeField] public float deathTime = 3f;

        [SerializeField] public float flyStartPos = 0.1f;

        private float createTime;
        private float boomTime;
        private Vector3 addV;
        private bool m_isTrigged;
        // Start is called before the first frame update
        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_CapsuleCollider = GetComponent<CapsuleCollider>();
            addV = new Vector3(0, flyStartPos, 0);
            new_Position = new Vector3();
            m_Rigidbody.AddForce(transform.forward + addV, ForceMode.Impulse);
            throwSound.Play();
            createTime = Time.fixedTime;
            m_isTrigged = false;

            //Debug.Log($"Создан меч: {createTime}");
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider other)
        {
            if ((other.tag == "Enemy") && (!m_isTrigged))
            {
                //other.gameObject.SetActive(false);
                boomSound.Play();
                boomTime = Time.fixedTime;
                m_isTrigged = true;
                m_CapsuleCollider.isTrigger = false;

                other.GetComponent<Health>().GetDamage(damage);
                // добавим "отталкивание" объектов из-за взрыва
                targetTransform = other.GetComponent<Transform>();
                currentForce = startingPower;
                StartCoroutine(ForceMove());

                //Debug.Log($"Мечь затупился о врага: {boomTime}");
            }
        }

        private IEnumerator ForceMove()
        {
            targetTransform.LookAt(transform);
            targetTransform.Rotate(180, 180, 180);
            new_Position = targetTransform.forward;
            new_Position *= currentForce;
            targetTransform.Translate(new_Position);
            currentForce -= fadingOfPower;

            if (currentForce > fadingOfPower)
                yield return new WaitForEndOfFrame();
            else
                yield return null;
        }

        private void FixedUpdate()
        {
            if (m_isTrigged)
            {
                if (boomTime + deathTime < Time.fixedTime)
                {
                    //Debug.Log($"Мечь убран по истечению времени уничтожения: {boomTime + deathTime} из {Time.fixedTime}");
                    transform.gameObject.SetActive(false);
                }
            }
            else
            {
                if (createTime + lifeTime < Time.fixedTime)
                {
                    m_CapsuleCollider.isTrigger = false;
                    m_isTrigged = true;
                    boomSound.Play();
                    boomTime = Time.fixedTime;
                    //Debug.Log($"Мечь отжил свое, запускаем самоуничтожение: {createTime + lifeTime} из {Time.fixedTime}");
                }
            }
        }
    }

}