using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject monsterPrefab; 
    public GameObject itemPrefab; 

    void Start()
    {
        StartCoroutine(MonsterSpawnCoroutine());
        StartCoroutine(ItemSpawnCoroutine());
    }

    IEnumerator MonsterSpawnCoroutine()
    {
        while (true)
        {
            float xPos = Random.Range(-4.5f, 4.5f);
            float zPos = Random.Range(33.5f, 55.5f);
            Instantiate(monsterPrefab, new Vector3(xPos, 0.05f, zPos), Quaternion.Euler(0, 178.0f, 0));

            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));

        }
    }

    IEnumerator ItemSpawnCoroutine()
    {
        var wait = new WaitForSeconds(3.0f);

        while (true)
        {
            float zPos = Random.Range(33.5f, 55.5f);
            Instantiate(itemPrefab, new Vector3(0.0f, 0.05f, zPos), Quaternion.identity);
            yield return wait;
        }
    }
}
