using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    public GameObject bulletPrefab;

    List<GameObject> bulletList;
    Queue<KeyValuePair<int, float>> clearQueue;
    int size = 500;
    int idx = 0;
    float clearTime = 3f;
    private void Start()
    {
        bulletList = new();
        clearQueue = new();
        bulletList.Capacity = size;
        for (int i = 0; i < size; i++)
        {
            bulletList.Add(Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity));
            bulletList[i].SetActive(false);
        }
        StartCoroutine(ClearBullet());
    }

    public void MakeBullet(Vector3 position)
    {
        bulletList[idx].transform.position = position;
        bulletList[idx].SetActive(true);
        clearQueue.Enqueue(new KeyValuePair<int, float>(idx, Time.time));
        idx = (idx + 1) % size;
    }

    IEnumerator ClearBullet()
    {
        var wait = new WaitForSeconds(0.1f);
        while (true)
        {
            while (clearQueue.Count > 0)
            {
                var top = clearQueue.Peek();
                if (top.Value + clearTime < Time.time)
                {
                    bulletList[top.Key].SetActive(false);
                    clearQueue.Dequeue();
                }
                else break;
            }
            yield return wait;
        }
    }
}
