using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChocobarCommand : ICommand
{
    private Chocobar theChocobar;

    public GetChocobarCommand(Chocobar theChocobar)
    {
        this.theChocobar = theChocobar;
    }

    public void execute()
    {
        theChocobar.GetChocobar();
    }
}

public class Chocobar : MonoBehaviour
{
    public void GetChocobar()
    {
        Debug.Log("초코바 얻음");
    }
}
