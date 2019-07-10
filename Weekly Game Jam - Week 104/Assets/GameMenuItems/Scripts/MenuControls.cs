using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControls : MonoBehaviour
{
   
     GameObject gamestarts;
     GameObject credit;
     GameObject options;
     GameObject logo;
     GameObject button;
     GameObject clickAnywhere;
    GameObject QuitGame;
    MenuControls menu;
    [SerializeField] GameObject LoadingGame;

    // Start is called before the first frame update
    void Start()
    {
        gamestarts = GameObject.FindGameObjectWithTag("StartGame");
        credit = GameObject.FindGameObjectWithTag("Credits");
        options = GameObject.FindGameObjectWithTag("Options");
        logo = GameObject.FindGameObjectWithTag("Logo");
        button = GameObject.FindGameObjectWithTag("Button");
        clickAnywhere = GameObject.FindGameObjectWithTag("Anywhere");
        QuitGame = GameObject.FindGameObjectWithTag("QuitGame");



        //   gamestarts = GameObject.FindGameObjectWithTag("Options");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        gamestarts.GetComponent<Animation>().Play();
        credit.GetComponent<Animation>().Play();
        options.GetComponent<Animation>().Play();
        logo.GetComponent<Animation>().Play();
        QuitGame.GetComponent<Animation>().Play();

        button.SetActive(false);
        clickAnywhere.SetActive(false);
       
    }

    public void NewGame()
    {
        LoadingGame.SetActive(true);
        SceneManager.LoadScene(1); 
        menu.GetComponent<AudioSource>().Stop();
    }
}
