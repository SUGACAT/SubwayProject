using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSandBoxCommand : ICommand
{
    private SandBox theSandBox;

    public GetSandBoxCommand(SandBox theSandBox)
    {
        this.theSandBox = theSandBox;
    }

    public void execute()
    {
        theSandBox.HideInBox();
    }
}

public class SandBox
{
    public void HideInBox()
    {
        Debug.Log("숨음");
    }
}
