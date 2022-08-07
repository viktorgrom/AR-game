using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    private PlacementIndicator placementIndicator;
    private Button _button;
    private GameObject _beam;
    private Rigidbody rb;
    [SerializeField] private float _force;
    void Start()
    {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Fire);
    }

    private void Fire()
    {
        _beam = GameObject.Find("Beam");
        rb = _beam.GetComponent<Rigidbody>();

        if (!placementIndicator.recharging)
        {
            rb.AddForce(rb.transform.up * _force, ForceMode.Impulse);
            placementIndicator.recharging = true;
        }
        
    }
}
