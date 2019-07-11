using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TeleType : MonoBehaviour
{

    [SerializeField] Text text;
    [SerializeField] GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(AppearingText());
    }

    // Update is called once per frame
    void Update()
    {

    }

   IEnumerator AppearingText()
    {
        text.text = "We";
        yield return new WaitForSeconds(0.5f);
        text.text = "We would";
        yield return new WaitForSeconds(0.5f);
        text.text = "We would like";
        yield return new WaitForSeconds(0.5f);
        text.text = "We would like to";
        yield return new WaitForSeconds(0.5f);
        text.text = "We would like to thank";
        yield return new WaitForSeconds(0.5f);
        text.text = "We would like to thank you";
        yield return new WaitForSeconds(0.5f);
        text.text = "We would like to thank you for";
        yield return new WaitForSeconds(0.5f);
        text.text = "We would like to thank you for trying";
        yield return new WaitForSeconds(0.5f);
        text.text = "We would like to thank you for trying our";
        yield return new WaitForSeconds(0.5f);
        text.text = "We woudd like to thank you for trying our game.";
        yield return new WaitForSeconds(1f);
        text.text = "";
     
        button.SetActive(true);
    
        
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }

}
