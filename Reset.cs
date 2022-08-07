using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    private PlacementIndicator placementIndicator;
    // Start is called before the first frame update
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        placementIndicator.recharging = false;
    }
}
