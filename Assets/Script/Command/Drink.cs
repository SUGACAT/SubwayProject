using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDrinkCommand : ICommand
{
    private Drink theDrink;
    public GetDrinkCommand(Drink theDrink)
    {
        this.theDrink = theDrink;
    }

    public void execute()
    {
        theDrink.GetDrink();
    }
}

public class Drink : MonoBehaviour
{
    public void GetDrink()
    {
        Debug.Log("음료 얻음");
    }
}
