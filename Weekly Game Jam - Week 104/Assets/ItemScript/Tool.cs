using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tool : MonoBehaviour
{

    public float TheDistance;

    public Text ActionText;

    public GameObject PickUpAction;
    public GameObject Cursor;

    public Text Objective2;

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    private void OnMouseOver()
    {
        if (TheDistance <= 3)
        {
            Cursor.SetActive(false);
            PickUpAction.SetActive(true);
            ActionText.text = "Get the ToolBox";
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (TheDistance <= 3)
                {
                    Objective2.text = "FINISHED";
                    Objective2.color = Color.yellow;
                    Cursor.SetActive(true);
                    PickUpAction.SetActive(false);
                    RepairSystem.Tool = true;
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
