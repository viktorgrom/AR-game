using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowShell : MonoBehaviour
{
    private Trajectory _trajectory;
    private PlacementIndicator _placementIndicator;
    [SerializeField] private GameObject _bulletPrefab;
    private GameObject _bullet;
    private Rigidbody _rb;
    private Vector3 _speed;

    private GameObject _fieldObject;
    private InputField _field;
    private string _forceString;
    private int _force;


    void Start()
    {
        _trajectory = FindObjectOfType<Trajectory>();
        _placementIndicator = FindObjectOfType<PlacementIndicator>();
        _fieldObject = GameObject.Find("InputField");
        _field = _fieldObject.GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        _forceString = _field.text;
        _force = Int32.Parse(_forceString);

        _speed = transform.forward * 2 + transform.up * _force;
        _trajectory.ShowTrajectory(transform.position + new Vector3(0, 0.25f, 0), _speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _bullet = Instantiate(_bulletPrefab, transform.position + new Vector3(0, 0.25f, -0.05f), _bulletPrefab.transform.rotation);
        _rb = _bullet.GetComponent<Rigidbody>();
        _rb.AddForce(_speed, ForceMode.Impulse);

        _rb = collision.rigidbody;
        _rb.AddForce(_rb.transform.up * -1, ForceMode.Impulse);

        _placementIndicator.recharging = true;
    }
}
