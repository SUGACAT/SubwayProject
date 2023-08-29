using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoamingPoint : MonoBehaviour
{
  public bool isOut;

  private void OnTriggerEnter(Collider other)
  {
	Debug.Log(other.transform.gameObject);

	if (other.transform.CompareTag("Monster"))
	{
	  isOut = false;

	  Debug.Log("Contact");
	  CatMove theCatMove = other.GetComponent<CatMove>();
	  theCatMove.Roaming();
	  StartCoroutine(recallCoroutine(theCatMove));
	}
  }

  private void OnTriggerExit(Collider other)
  {
	if (other.transform.CompareTag("Monster"))
	{
	  isOut = true;

	  CatMove theCatMove = other.GetComponent<CatMove>();

	  Debug.Log("Contact");
	  theCatMove.Roaming();
	}
  }

  IEnumerator recallCoroutine(CatMove cm)
  {
	yield return new WaitForSeconds(2f);

	if (!isOut)
	  cm.Roaming();
  }

}
