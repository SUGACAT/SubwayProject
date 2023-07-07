using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool interact;
    private float gauge;
    private KeyCode preKey;
    private KeyCode pressedKey;

    public string objectCode = "lever";
    public bool complete = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            // UI 호출 -> E키를 눌러 interact -> QTE, gauge increase, AD (실패시 gauge decrease) -> done = true
            if (Input.GetKeyDown(KeyCode.E))
            {
                //UI 호출

            }
        }
    }

    private void QTEsys()       // 키보드 누름 정보 읽어내어 array에 저장 -> A>D>A>D -> 순에 맞지 않는다면 gauge decrease, 맞다면 gauge increase
    {
        if (Input.GetKeyDown(KeyCode.A) && preKey == KeyCode.D)
        {
            preKey = pressedKey;
            gauge += 1;
            pressedKey = KeyCode.A;
        }
        else if (Input.GetKey(KeyCode.D) && preKey == KeyCode.A)
        {
            preKey = pressedKey;
            gauge += 1;
            pressedKey = KeyCode.D;
        }
        else if (gauge >= 0 && !Input.anyKey)
        {
            gauge -= 0.2f;
        }
    }
}
