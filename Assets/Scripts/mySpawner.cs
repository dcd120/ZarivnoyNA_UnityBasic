using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mySpawner : MonoBehaviour
{
    [SerializeField] public float spawnTime = 5f;
    [SerializeField] public Component spawnItem;

    private float lastSpawn;
    // Start is called before the first frame update
    void Start()
    {
        //Spawn();
        //lastSpawn = Time.fixedTime;
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopCoroutine(Spawn());
        //Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.fixedTime > lastSpawn + spawnTime)
       // {
            // выпускаем зверя
            
            // перезапускаем счетчик
       //     lastSpawn = Time.fixedTime;
        //}
    }
    private IEnumerator Spawn()
    {
        while (enabled)
        {
            Instantiate(spawnItem, transform.position + transform.up + transform.up, transform.rotation);
        }
        yield return null;
    }
}
