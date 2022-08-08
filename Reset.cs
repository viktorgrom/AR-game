using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    private PlacementIndicator _placementIndicator;
    // Start is called before the first frame update
    void Start()
    {
        _placementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _placementIndicator.recharging = false;
    }
}
