using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyring : MonoBehaviour
{
    public GameObject keyring_obj;

    public float speed;
    public string alphabetKey;

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    public void Rotate()
    {
        keyring_obj.transform.Rotate(0, -Input.GetAxis("Mouse X") * speed, 0f, Space.World);
        keyring_obj.transform.Rotate(-Input.GetAxis("Mouse Y") * speed, 0f, 0f);
    }
}
