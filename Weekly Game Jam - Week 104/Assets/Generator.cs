using UnityEngine;
using System.Collections.Generic;

public class Generator : MonoBehaviour
{
    [SerializeField] AudioClip repairedAudio = null;

    //TODO Make class item
    public List<GameObject> items = new List<GameObject>();

    public float RepairPercentage { get; set; } = 0;

    public bool Repaired { get; set; } = false;

    public void Activate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        AudioSource audioSource = GetComponent<AudioSource>();

        audioSource.clip = repairedAudio;
        audioSource.Play();
    }
}