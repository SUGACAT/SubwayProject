using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFlashCommand : ICommand
{
  private FlashBox theFlashBox;
  public GetFlashCommand(FlashBox theFlashBox)
  {
	this.theFlashBox = theFlashBox;
  }

  public void execute(Interact i)
  {
	theFlashBox.GetFlash(i);
  }
}

public class FlashBox
{
  public void GetFlash(Interact i)
  {
	Debug.Log("플래시라이트 얻음");
	i.Command_Flash();
  }
}
