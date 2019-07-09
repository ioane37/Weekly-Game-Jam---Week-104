using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

public class Generator : MonoBehaviour
{
    [SerializeField] PlayableDirector doorCutscene = null;
    [SerializeField] AudioClip repairedAudio = null;

    [SerializeField] Camera cutsceneCamera = null;

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

        //Playing Door Cutscene
        doorCutscene.Play();

        GameObject canvas = GameObject.FindWithTag("Canvas");
        GameObject player = GameObject.FindWithTag("Player");
        GameObject enemy = GameObject.FindWithTag("Enemy");

        doorCutscene.stopped += (o) =>
        {
            canvas.SetActive(true);
            player.SetActive(true);
            enemy.SetActive(true);

            cutsceneCamera.depth = -2;

            //Don't needed because we're turning off camera
            //Destroy(cutsceneCamera.GetComponent<AudioListener>());
            cutsceneCamera.gameObject.SetActive(false);
        };

        canvas.SetActive(false);
        player.SetActive(false);
        enemy.SetActive(false);
    }
}