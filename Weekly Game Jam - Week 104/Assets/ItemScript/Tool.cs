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
    public GameObject ObjectiveButton3;
    public GameObject Item3;

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
                    ObjectiveButton3.SetActive(true);
                    Item3.SetActive(true);
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
