using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ItemType
{
    ATK_Speed,
    ATK_Count,
    Move_Speed,
    HP_UP
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
        items[0] = (ItemType)Random.Range(0, 4);
        
        do
        {
            items[1] = (ItemType)Random.Range(0, 4);
        }
        while (items[1] == items[0]);
    
        for(int i = 0; i < cubes.Length; i++)
        {
            cubes[i].GetComponent<Renderer>().material = materials[(int)items[i]];
            cubes[i].tag = items[i].ToString();
            texts[i].text = GetItemText(items[i]);
        }
    }

    string GetItemText(ItemType type)
    {
        switch(type)
        {
            case ItemType.ATK_Count:
                return "ATTACK\nCOUNT";
            case ItemType.ATK_Speed:
                return "ATTACK\nSPEED";
            case ItemType.Move_Speed:
                return "MOVE\nSPEED";
            case ItemType.HP_UP:
                return "HP\nUP";
            default:
                return "";
        }
    }

    void Update()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;
    }
}
