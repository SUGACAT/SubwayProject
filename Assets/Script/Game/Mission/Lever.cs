using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    private bool hasInteracted;
    private float gauge;
    private KeyCode preKey;
    private KeyCode pressedKey;

    public string objectCode = "lever";
    public bool complete = false;
    public GameObject missionUi;
    public Slider gaugeSlider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Lever Touched");
            // UI 호출 -> E키를 눌러 interact -> QTE, gauge increase, AD (실패시 gauge decrease) -> done = true
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("E key pressed");
                missionUi.SetActive(true);
                QTEsys();
            }
        }
    }

    private void QTEsys()       // 키보드 누름 정보 읽어내어 array에 저장 -> A>D>A>D -> 순에 맞지 않는다면 gauge decrease, 맞다면 gauge increase
    {
        if (Input.GetKeyDown(KeyCode.A) && preKey == KeyCode.D)
        {
            preKey = pressedKey;
            gauge += 1;
            gaugeSlider.value = gauge;
            pressedKey = KeyCode.A;
        }
        else if (Input.GetKey(KeyCode.D) && preKey == KeyCode.A)
        {
            preKey = pressedKey;
            gauge += 1;
            gaugeSlider.value = gauge;
            pressedKey = KeyCode.D;
        }
        else if (gauge >= 0 && !Input.anyKey)
        {
            gauge -= 0.2f;
            gaugeSlider.value = gauge;
        }

        if (gauge < 0)
        {
            missionUi.SetActive(false);
            return;
        }
        else if (gauge >= 20)
        {
            complete = true;
            missionUi.SetActive(false);
            return;
        }
    }
}
