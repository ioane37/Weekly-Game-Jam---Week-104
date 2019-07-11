using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fuel : MonoBehaviour
{
    public GameObject destoroyedFloor = null;

    public float TheDistance;

    public Text ActionText;

    public GameObject PickUpAction;
    public GameObject Cursor;
    public GameObject[] ObjectivePanel;
    public Text Objective3;
    public Button[] Buttons;

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
            ActionText.text = "Get the Fuel Item";
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (TheDistance <= 3)
                {
                    Objective3.text = "FINISHED";
                    Objective3.color = Color.yellow;
                    Cursor.SetActive(true);
                    PickUpAction.SetActive(false);
                    RepairSystem.Fuel = true;
                    ActionText.text = "";
                    Destroy(this.gameObject);
                    ObjectivePanel[3].SetActive(true);
                    ObjectivePanel[0].SetActive(false);
                    ObjectivePanel[1].SetActive(false);
                    ObjectivePanel[2].SetActive(false);
                    Buttons[0].interactable = false;
                    Buttons[1].interactable = false;
                    Buttons[2].interactable = false;


                    destoroyedFloor.SetActive(false);
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
