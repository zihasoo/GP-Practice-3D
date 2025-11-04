using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ItemType
{
    ATK_Speed,
    ATK_Count
}

public class Item : MonoBehaviour
{
    public float speed;
    public GameObject[] cubes;
    public Material[] materials;
    public TextMeshProUGUI[] texts;

    ItemType[] items = new ItemType[2];

    private void Start()
    {
        items[0] = (ItemType)Random.Range(0, 2);
        items[1] = items[0] == ItemType.ATK_Count ? ItemType.ATK_Speed : ItemType.ATK_Count;
    
        for(int i = 0; i < cubes.Length; i++)
        {
            cubes[i].GetComponent<Renderer>().material = materials[(int)items[i]];
            cubes[i].tag = items[i].ToString();
            texts[i].text = items[i] == ItemType.ATK_Count ? "ATACK\nCOUNT" : "ATACK\nSPEED";
        }
    }

    void Update()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;
    }
}
