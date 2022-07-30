using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] public bool canRegenerate = false;
    [SerializeField] private float regeneration = 0.01f;
    private float cRegTick = 1;
    private int cHealth = 0;
    [SerializeField] private bool isAlive = true;

    [SerializeField] private Transform theOne;
    // Start is called before the first frame update
    void Start()
    {
        theOne = transform.parent.transform;
        cHealth = maxHealth;
        isAlive = true;
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
    public void Kill()
    {
        cHealth = 0;
        isAlive = false;
        theOne.gameObject.SetActive(false);
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
}
