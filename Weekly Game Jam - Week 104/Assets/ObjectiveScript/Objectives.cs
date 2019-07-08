using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    public GameObject Objective1Text;
    public GameObject Objective2Text;
    public GameObject Objective3Text;
    public GameObject OpenQuestPanel;
    public GameObject CloseQuestPanel;
    public GameObject ObjectivePanel1;
    public GameObject ObjectivePanel2;
    public GameObject ObjectivePanel3;
    public GameObject ObjectiveButton1;
    public GameObject ObjectiveButton2;
    public GameObject ObjectiveButton3;

    public int status;
    public  PlayerController myPlayer;
    public CameraController myCam;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(status == 0)
            {
                status = 1;
                OpenQuestPanel.SetActive(true);
                myPlayer.enabled = false;
                myCam.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void ClosePanel()
    {
        status = 0;
        OpenQuestPanel.SetActive(false);
        myPlayer.enabled = true;
        myCam.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Objective1()
    {
        ObjectivePanel1.SetActive(true);
        ObjectivePanel2.SetActive(false);
        ObjectivePanel3.SetActive(false);
    
    }
    public void Objective2()
    {
        ObjectivePanel1.SetActive(false);
        ObjectivePanel2.SetActive(true);
        ObjectivePanel3.SetActive(false);
      
    }
    public void Objective3()
    {
        ObjectivePanel1.SetActive(false);
        ObjectivePanel2.SetActive(false);
        ObjectivePanel3.SetActive(true);
    }
}
