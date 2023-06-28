using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSway : MonoBehaviour
{
    private Vector3 originPos;

    private Vector3 currentPos;

    [SerializeField]
    private Vector3 limitPos;

    [SerializeField]
    private Vector3 smoothSway;

    // Start is called before the first frame update
    void Start()
    {
        originPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        TrySway();
    }

    private void TrySway()
    {
        if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
        {
            Swaying();
        }
        else
        {
            BackToOriginPos();
        }
    }

    private void Swaying()
    {
        float _MoveX = Input.GetAxisRaw("Mouse X");
        float _MoveY = Input.GetAxisRaw("Mouse Y");

        currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_MoveX, smoothSway.x), -limitPos.x, limitPos.x),
                       Mathf.Clamp(Mathf.Lerp(currentPos.y, -_MoveY, smoothSway.y), -limitPos.y, limitPos.y),
                       originPos.z);

        transform.localPosition = currentPos;
    }

    private void BackToOriginPos() 
    {
        currentPos = Vector3.Lerp(currentPos, originPos, smoothSway.x);
        transform.localPosition = currentPos;
    }
}
