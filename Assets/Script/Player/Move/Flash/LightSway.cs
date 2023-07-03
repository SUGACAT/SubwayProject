using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSway : MonoBehaviour
{
    private GameObject M_Camera;

    [Header("FlashRotate")]
    private Vector3 offSet;
    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        M_Camera = Camera.main.gameObject;

        offSet = transform.position - M_Camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.position = M_Camera.transform.position + offSet;
        transform.rotation = Quaternion.Slerp(transform.rotation, M_Camera.transform.rotation, speed * Time.deltaTime);
    }
}
