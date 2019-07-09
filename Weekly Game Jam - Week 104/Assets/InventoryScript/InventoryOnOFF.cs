using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOnOFF : MonoBehaviour
{

    public GameObject Inventory;
    public int status;
 

    private void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(status == 0)// off screan
            {
                status = 1;
                Inventory.SetActive(true);       
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Inventory.SetActive(false);
                status = 0;           
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        
    }
}
