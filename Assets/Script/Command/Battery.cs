using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBatteryCommand : ICommand
{
    public Battery theBattery;

    public GetBatteryCommand(Battery theBattery)
    {
        this.theBattery = theBattery;
    }

    public void execute()
    {
        theBattery.GetBattery();
    }
}

public class Battery : MonoBehaviour
{
    public void GetBattery()
    {
        Debug.Log("배터리 얻음");
    }
}
