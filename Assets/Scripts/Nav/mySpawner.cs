using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EI2
{
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
            InvokeRepeating(nameof(Spawn_2), 0f, spawnTime);
        }

        private void OnEnable()
        {
            //StartCoroutine(Spawn());
        }

        private void OnDisable()
        {
            //StopCoroutine(Spawn());
            //Destroy(gameObject);
            CancelInvoke(nameof(Spawn_2));
        }

        // Update is called once per frame
        void Update()
        {
            //if (Time.fixedTime > lastSpawn + spawnTime)
            // {
            // ��������� �����

            // ������������� �������
            //     lastSpawn = Time.fixedTime;
            //}
        }
        private IEnumerator Spawn()
        {
            while (enabled)
            {
                Instantiate(spawnItem, transform.position + transform.up + transform.up, transform.rotation);
                yield return new WaitForSeconds(spawnTime);
            }
            yield return null;
        }

        private void Spawn_2()
        {
            Instantiate(spawnItem, transform.position + transform.up + transform.up, transform.rotation);
        }
    }
}