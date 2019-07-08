using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EneryItem : MonoBehaviour
{

    public float TheDistance;
   
    public Text ActionText;
 
    public GameObject PickUpAction;
    public GameObject Cursor;

    public Text Objective1;
 
    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    private void OnMouseOver()
    {
        if(TheDistance <= 3)
        {
            Cursor.SetActive(false);
            PickUpAction.SetActive(true);
            ActionText.text = "Get the Enery Item";
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(TheDistance <= 3)
                {
                    Objective1.text = "FINISHED";
                    Objective1.color = Color.yellow;
                    Cursor.SetActive(true);
                    PickUpAction.SetActive(false);
                    RepairSystem.EneryItem = true;
                    ActionText.text = "";
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void OnMouseExit()
    {
     
        ActionText.GetComponent<Text>().text = "";

    }


}
