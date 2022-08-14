using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EI2
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 1;
        [SerializeField] public bool canRegenerate = false;
        [SerializeField] private float regeneration = 0.01f;
        private float cRegTick = 1;
        private int cHealth = 0;
        [SerializeField] private bool isAlive = true;

        [SerializeField] private Transform theOne;
        [SerializeField] private float removingTime = 2f;
        private float killingTime;
        // Start is called before the first frame update
        void Start()
        {
            theOne = transform;
            cHealth = maxHealth;
            isAlive = true;
        }

        public string GetHealthUIString()
        {
            return $"{cHealth}/{maxHealth}";
        }

        // Update is called once per frame
        void Update()
        {
            if (!isAlive) return;
            if (!canRegenerate) return;
            if (cHealth < maxHealth)
            {
                cRegTick -= Time.fixedTime * regeneration;
                if (cRegTick <= 0)
                {
                    cRegTick = 1;
                    cHealth++;
                }
            }
        }

        private void FixedUpdate()
        {
            if (!isAlive)
            {
                if (Time.fixedTime > killingTime + removingTime)
                    theOne.gameObject.SetActive(false);
            }
        }

        public bool IsWounded()
        {
            return cHealth < maxHealth;
        }
        public void Kill()
        {
            cHealth = 0;
            isAlive = false;
            killingTime = Time.fixedTime;
        }

        public void GetDamage(int damage)
        {
            cHealth -= damage;
            if (cHealth <= 0) Kill();
        }

        public void Healing(int heal)
        {
            cHealth += heal;
            if (cHealth > maxHealth) cHealth = maxHealth;
        }

        public bool IsAlive()
        {
            return isAlive;
        }
    }

}