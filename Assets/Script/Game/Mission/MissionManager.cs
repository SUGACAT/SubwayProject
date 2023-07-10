using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    /* Mission list
     * 
     * 사다리 이용해서 2층 천장에 두꺼비집? 켜기
     * for 빛 or 전기 이용, -> 두꺼비집 켜기(높은 곳 위치) -> 높은 곳에 올라가기 위한 물품 필요 -> 쌓기!
     * 
     * 괴물 등에 달린 열쇠 획득해서 관리실 잠금해제
     * for 두꺼비집 켜기 -> 관리실 잠김! -> 괴물 움직일 때 마다 짤랑짤랑 -> 열쇠가 있음!
     * 
     * 지하 2층 선로에 있는 레버 4개 올리기
     * for what? -> 불 키기?
     */

    

    //public GameObject missionUi;
    public GameObject[] missions;
    public GameObject[] clearedMissions;

    private void Update()
    {
        LoadMissionChecker();
    }

    private void LoadMissionChecker()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene("MissionScene");
        }
        
    }
}
