using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    drink, chocobar, battery
}

public class Inventory : MonoBehaviour
{
    [Header("Values")]
    public int currentSlotNumber;

    public GameObject[] slot;

    private Item items;

    [Header("Prefabs")]
    [SerializeField] GameObject Chocobar;
    [SerializeField] GameObject Drink;
    [SerializeField] GameObject Battery;

    [Header("Scripts")]
    public PlayerManager thePlayerManager;

    public void AddItemInInventory(Item item)
    {
        FindAvailableSlot(item);
    }

    public void FindAvailableSlot(Item item)
    {
        for(int i = 0; i < 3; i++)
        {
            if(slot[i].transform.childCount == 0)
            {
                GameObject itemImg = Instantiate(TransformValueToItem(item), slot[i].transform.position, Quaternion.identity, slot[i].transform.parent);
                itemImg.transform.parent = slot[i].transform;

                Debug.Log($"{item} is Assigned in {slot[i]}");

                return;
            }
            else
            {
                Debug.Log($"{slot[i]} is already filled with {slot[i].transform.name}");
            }
        }
    }
    
    public GameObject TransformValueToItem(Item item)
    {
        switch (item)
        {
            case Item.drink:
                return Drink;
            case Item.chocobar:
                return Chocobar;
            case Item.battery:
                return Battery;
        }

        return null;
    }

    public void Execute(Item item)
    {
        switch(item)
        {
            case Item.drink:
                thePlayerManager.IncreaseSpeed();
                break;
            case Item.chocobar:
                thePlayerManager.AddStamina();
                break;
            case Item.battery:
                thePlayerManager.AddBattery();
                break;
        }
    }
}
