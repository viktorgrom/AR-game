using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    private PlacementIndicator _placementIndicator;
    private Button _button;
    private GameObject _beam;
    private Rigidbody _rb;
    [SerializeField] private float _force;
    void Start()
    {
        _placementIndicator = FindObjectOfType<PlacementIndicator>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Fire);
    }

    private void Fire()
    {
        _beam = GameObject.Find("Beam");
        _rb = _beam.GetComponent<Rigidbody>();

        if (!_placementIndicator.recharging)
        {
            _rb.AddForce(_rb.transform.up * _force, ForceMode.Impulse);
            _placementIndicator.recharging = true;
        }
        
    }
}
