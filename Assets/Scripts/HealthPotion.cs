using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] public int power = 2;
    [SerializeField] public string playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            if (!other.GetComponent<Health>().IsWounded()) return;
            other.GetComponent<Health>().Healing(power);
            transform.parent.gameObject.SetActive(false);
        }
    }

}
