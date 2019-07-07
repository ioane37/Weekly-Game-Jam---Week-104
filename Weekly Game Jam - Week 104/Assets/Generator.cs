using UnityEngine;
using System.Collections.Generic;

public class Generator : MonoBehaviour
{
    //TODO Make class item
    public List<GameObject> items = new List<GameObject>();

    public float RepairPercentage { get; set; } = 0;

    public bool Repaired { get; set; } = false;
}