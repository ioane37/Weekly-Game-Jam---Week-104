using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestTake : MonoBehaviour
{

    public float TheDistance;
    public GameObject ActionDisplay;
    public Text ActionText;
    public GameObject Cube;
    
    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    private void OnMouseOver()
    {
        if(TheDistance <= 3)
        {
            ActionDisplay.SetActive(true);
            ActionText.GetComponent<Text>().text = "Get the Item";
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(TheDistance <= 3)
                {
                    ActionDisplay.SetActive(false);
                    ActionText.GetComponent<Text>().text = "";
                    Cube.SetActive(false);
                }
            }
        }
    }

    private void OnMouseExit()
    {
        ActionDisplay.SetActive(false);
        ActionText.GetComponent<Text>().text = "";

    }
}
