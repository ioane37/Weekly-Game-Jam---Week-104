using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] GameObject winUI = null;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winUI.SetActive(true);

            other.GetComponent<PlayerController>().enabled = false;
        }
    }
}
