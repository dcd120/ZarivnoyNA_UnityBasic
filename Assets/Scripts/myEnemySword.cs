using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEnemySword : MonoBehaviour
{
    [SerializeField] public int damage = 1;

    [SerializeField] public AudioSource slashSound;

    private bool canHit = true;
    [SerializeField] public float resetTime = 0.2f;
    private float timeToReset;

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (canHit) return;
        timeToReset -= Time.fixedTime;
        if (timeToReset <= 0) canHit = true;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && (canHit))
        {
            slashSound.Play();
            canHit = false;

            // проигрышь )
            other.GetComponent<Health>().GetDamage(damage);

            timeToReset = resetTime * 1000;
        }
    }
}
