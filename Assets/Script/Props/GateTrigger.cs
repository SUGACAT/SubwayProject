using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public bool isIn = false;

    public GameObject blockCollider;

    private void Awake()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isIn = true;

            blockCollider.SetActive(false);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!other.transform.GetComponent<PlayerManager>().isCrouching)
            {
                Debug.Log("Cought");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isIn = false;

            blockCollider.SetActive(true);
        }
    }

}
