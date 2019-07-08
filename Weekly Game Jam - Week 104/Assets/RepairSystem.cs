using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RepairSystem : MonoBehaviour
{
    [SerializeField] RectTransform repairSlider = null;
    [SerializeField] TextMeshProUGUI repairText = null;

    [SerializeField] float repairRadius = 3f;

    [SerializeField] float repairSpeed = 0.001f;

    Transform cameraTransform;
    public  CameraController cam;
    bool generatorFound = false;
    public static bool hasItem = false;
    public static bool EneryItem, Fuel, Tool = false;
    public GameObject FinalPanel;
    void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        FindGenerator();

        if (!generatorFound)
        {
            if (repairText.gameObject.activeSelf)
            {
                repairText.text = "";
                repairText.gameObject.SetActive(false);
            }

            if(repairSlider.gameObject.activeSelf)
            {
                repairSlider.gameObject.SetActive(false);
            }
        }
    }

    private void FindGenerator()
    {
        RaycastHit hit;
        generatorFound = Physics.Raycast(transform.position, cameraTransform.forward, out hit, repairRadius, LayerMask.GetMask("Generator"));

        if (generatorFound)
        {
            Generator generator = hit.transform.GetComponent<Generator>();

            if (!generator.Repaired)
            {
                if(EneryItem == true && Fuel == true && Tool == true)
                {
                    FinalPanel.SetActive(true);
                    hasItem = true;
                    if (hasItem)
                    {
                        //If player has everything
                        repairText.gameObject.SetActive(true);
                        repairText.text = "Press Mouse 1 To Start Repairing";
                        //Else


                        if (Input.GetMouseButton(0) /*TODO And Have All Tools*/)
                        {
                            cam.enabled = false;
                            repairText.text = "";
                            repairText.gameObject.SetActive(false);

                            RepairGenerator(generator);
                        }

                        if (Input.GetMouseButtonUp(0))
                        {
                            

                            if (repairSlider.gameObject.activeSelf)
                            {
                                repairSlider.gameObject.SetActive(false);
                            }
                        }
                    }
                    
                }
                else
                {

                    repairText.gameObject.SetActive(true);
                    repairText.text = "Find More Tools";
                                 
                }

               


            }
            else
            {
                if (!repairText.gameObject.activeSelf)
                    repairText.gameObject.SetActive(true);

                repairText.text = "Generator Repaired!";
                cam.enabled = true;
            }
        }
    }

    private void RepairGenerator(Generator generator)
    {
        if (!repairSlider.gameObject.activeSelf)
            repairSlider.gameObject.SetActive(true);

        generator.RepairPercentage = Mathf.MoveTowards(generator.RepairPercentage, 1f, repairSpeed);
        Vector3 targetScale = repairSlider.localScale;
        targetScale.x = generator.RepairPercentage;
        repairSlider.localScale = targetScale;

        if(generator.RepairPercentage == 1f)
        {
            repairSlider.gameObject.SetActive(false);

            repairText.gameObject.SetActive(true);
            repairText.text = "Generator Repaired!";

            generator.Repaired = true;

            //generator.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Camera.main.transform.forward * repairRadius);
    }
}