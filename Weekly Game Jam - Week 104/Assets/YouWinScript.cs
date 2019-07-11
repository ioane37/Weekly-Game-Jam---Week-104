using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class YouWinScript : MonoBehaviour
{

    [SerializeField] Text text;
    [SerializeField] GameObject[] button;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(startText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator startText()
    {
        text.text = "C";
        yield return new WaitForSeconds(0.2f);
        text.text = "Co";
        yield return new WaitForSeconds(0.2f);
        text.text = "Con";
        yield return new WaitForSeconds(0.2f);
        text.text = "Cong";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congr";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congra";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congrat";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratu";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratul";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratula";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulat";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulati";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulatio";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulation";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations ";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations o";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on ";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on y";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on yo";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on you";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your ";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your s";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your su";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your suc";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your succ";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your succe";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your succes";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success.";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. ";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. Y";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. Yo";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You ";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You ha";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You hav";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have ";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have m";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have ma";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have mad";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made ";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made u";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made us";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made us ";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made us a";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made us al";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made us all";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made us all ";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made us all p";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made us all pr";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made us all pro";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made us all prou";
        yield return new WaitForSeconds(0.2f);
        text.text = "Congratulations on your success. You have made us all proud.";


        button[0].SetActive(true);
        button[1].SetActive(true);
    }

    
}


