using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseOnOff : MonoBehaviour
{

    [SerializeField] int status;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject ControlMenu;

    [SerializeField] GameObject player;
    [SerializeField] GameObject cam;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(status == 0)
            {
                status = 1;
                PauseMenu.SetActive(true);
                player.GetComponent<PlayerController>().enabled = false;
                cam.GetComponent<CameraController>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                PauseMenu.SetActive(false);
                status = 0;
                player.GetComponent<PlayerController>().enabled = true;
                cam.GetComponent<CameraController>().enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoBack()
    {
        PauseMenu.SetActive(true);
        ControlMenu.SetActive(false);
    }

    public void GoControl()
    {
        ControlMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }
}
