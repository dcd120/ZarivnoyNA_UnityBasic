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
        lastSpawn = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > lastSpawn + spawnTime)
        {
            // выпускаем зверя
            Instantiate(spawnItem, transform.position + transform.up + transform.up, transform.rotation);
            // перезапускаем счетчик
            lastSpawn = Time.fixedTime;
        }
    }
}
