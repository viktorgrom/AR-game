using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowShell : MonoBehaviour
{
    private Trajectory trajectory;
    private PlacementIndicator placementIndicator;
    [SerializeField] private GameObject _bulletPrefab;
    private GameObject _bullet;
    private Rigidbody rb;
    private Vector3 speed;

    private GameObject _fieldObject;
    private InputField field;
    private string forceString;
    private int force;


    void Start()
    {
        trajectory = FindObjectOfType<Trajectory>();
        placementIndicator = FindObjectOfType<PlacementIndicator>();
        _fieldObject = GameObject.Find("InputField");
        field = _fieldObject.GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        forceString = field.text;
        force = Int32.Parse(forceString);

        speed = transform.forward * 2 + transform.up * force;
        trajectory.ShowTrajectory(transform.position + new Vector3(0, 0.25f, 0), speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _bullet = Instantiate(_bulletPrefab, transform.position + new Vector3(0, 0.25f, -0.05f), _bulletPrefab.transform.rotation);
        rb = _bullet.GetComponent<Rigidbody>();
        rb.AddForce(speed, ForceMode.Impulse);

        rb = collision.rigidbody;
        rb.AddForce(rb.transform.up * -1, ForceMode.Impulse);

        placementIndicator.recharging = true;
    }
}
