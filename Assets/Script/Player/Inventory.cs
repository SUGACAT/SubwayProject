using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    drink, chocobar, battery, empty
}

public class Inventory : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] int currentSlotNumber;

    [SerializeField] GameObject[] parrentList;

    [SerializeField] GameObject[] ObjectSlot;
    [SerializeField] Item[] typeSlot;

    [SerializeField] GameObject[] arrowSlot;

    private Item items;

    [Header("Prefabs")]
    [SerializeField] GameObject Chocobar;
    [SerializeField] GameObject Drink;
    [SerializeField] GameObject Battery;

    [Header("Scripts")]
    public PlayerManager thePlayerManager;

    private void Update()
    {
        SelectItem();
        ConfirmItem();
    }

    public void AddItemInInventory(Item item)
    {
        FindAvailableSlot(item);
    }

    public void FindAvailableSlot(Item item)
    {
        for (int i = 0; i < 3; i++)
        {
            if (parrentList[i].transform.childCount == 0)
            {
                ObjectSlot[i] = Instantiate(TransformValueToItem(item), parrentList[i].transform.position, Quaternion.identity, parrentList[i].transform.parent);
                typeSlot[i] = item;
                ObjectSlot[i].transform.parent = parrentList[i].transform;

                Debug.Log($"{item} is Assigned in {parrentList[i]}");
                return;
            }
            else
            {
                Debug.Log($"{parrentList[i]} is already filled with {parrentList[i].transform.name}");
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

    public void SelectItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSlotNumber = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSlotNumber = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSlotNumber = 2;
        }

        ShowArrow();
    }

    public void ConfirmItem()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            switch (currentSlotNumber) 
            {
                case 0:
                    Execute(typeSlot[0]);

                    Destroy(ObjectSlot[0]);
                    typeSlot[0] = Item.empty;
                    break;
                case 1:
                    Execute(typeSlot[1]);

                    Destroy(ObjectSlot[1]);
                    typeSlot[1] = Item.empty;
                    break;
                case 2:
                    Execute(typeSlot[2]);

                    Destroy(ObjectSlot[2]);
                    typeSlot[2] = Item.empty;
                    break;
            }
        }
    }

    public void ShowArrow()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == currentSlotNumber)
            {
                arrowSlot[currentSlotNumber].SetActive(true);
            }
            else
            {
                arrowSlot[i].SetActive(false);
            }
        }
    }

    public void Execute(Item item)
    {
        switch (item)
        {
            case Item.drink:
                thePlayerManager.IncreaseSpeed();
                break;
            case Item.chocobar:
                thePlayerManager.AddStaminas();
                break;
            case Item.battery:
                thePlayerManager.AddBattery();
                break;
        }
    }
}
