using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoorScript : MonoBehaviour
{
    public float TheDistance;

    [SerializeField] GameObject Cursor;
    [SerializeField] GameObject PickUpAction;
    [SerializeField] Text ActionText;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] int status;
    // Start is called before the first frame update
    void Start()
    {
        leftDoor = GameObject.FindGameObjectWithTag("LeftDoor");
        rightDoor = GameObject.FindGameObjectWithTag("RightDoor");
    }

    // Update is called once per frame
    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    private void OnMouseOver()
    {
        if(TheDistance <= 3)
        {
      
            if (Input.GetKeyDown(KeyCode.E)){
                if(status == 0)
                {
                    status = 1;
                    leftDoor.GetComponent<Animation>().Play("LeftDoor");
                    rightDoor.GetComponent<Animation>().Play("RightDoor");


                    GetComponent<BoxCollider>().enabled = false;
                }
                else
                {
                    status = 0;
                    leftDoor.GetComponent<Animation>().Play("CloseLeftDoor");
                    rightDoor.GetComponent<Animation>().Play("CloseRightDoor");
                }
               
             
            }
        }
    }

    private void OnMouseExit()
    {
        PickUpAction.SetActive(false);

        Cursor.SetActive(true);

        ActionText.GetComponent<Text>().text = "";
    }

}
