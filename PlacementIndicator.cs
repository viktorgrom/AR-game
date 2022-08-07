using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject visual;
    public GameObject objectToSpawn;

    public bool choosedObject;
    public GameObject scrollView;

    private Vector2 _touchPosition;
    [SerializeField] private Camera _ARCamera;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject _selectedGO;

    public bool rotation;
    private Quaternion _yRotation;

    public bool recharging;
    [SerializeField] private GameObject maketShell;

    void Start()
    {
        // get the components
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;

        // hide the placement visual
        visual.SetActive(false);
        scrollView.SetActive(false);
    }

    void Update()
    {
        if (choosedObject)
        {
            MarkerAndSpawn();
        }

        MoveObject();

        if (recharging)
        {
            maketShell.SetActive(false);
        }
        else
        {
            maketShell.SetActive(true);
        }
    }

    private void MarkerAndSpawn()
    {
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // if we hit an AR plane, update the position and rotation
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!visual.activeInHierarchy)
                visual.SetActive(true);

            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            
            Instantiate(objectToSpawn, transform.position, objectToSpawn.transform.rotation);

            maketShell = GameObject.Find("Shell");
            choosedObject = false;
            scrollView.SetActive(false);
        }
    }

    private void MoveObject()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            _touchPosition = touch.position;

            if(touch.phase == TouchPhase.Began)
            {
                Ray ray = _ARCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.collider.CompareTag("UnSelected"))
                    {
                        hitObject.collider.gameObject.tag = "Selected";
                    }
                }
            }

            _selectedGO = GameObject.FindWithTag("Selected");

            if (touch.phase == TouchPhase.Moved && Input.touchCount == 1)
            {
                if (rotation)
                {
                    _yRotation = Quaternion.Euler(0f, -touch.deltaPosition.x * 0.1f, 0f);
                    _selectedGO.transform.rotation = _yRotation * _selectedGO.transform.rotation;
                }
                else
                {
                    rayManager.Raycast(_touchPosition, hits, TrackableType.Planes);

                    _selectedGO.transform.position = hits[0].pose.position;
                }
                
            }

            if(Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];

                if(touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
                {
                    float distanceBetweenTouches = Vector2.Distance(touch1.position, touch2.position);
                    float prevDistanceBetweenTouches = Vector2.Distance(touch1.position  - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
                    float delta = distanceBetweenTouches - prevDistanceBetweenTouches;

                    if(Mathf.Abs(delta) > 0)
                    {
                        delta *= 0.1f;
                    }
                    else
                    {
                        distanceBetweenTouches = delta = 0;
                    }

                    _yRotation = Quaternion.Euler(0f, -touch.deltaPosition.x * delta, 0f);
                    _selectedGO.transform.rotation = _yRotation * _selectedGO.transform.rotation;
                }

                
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (_selectedGO.CompareTag("Selected"))
                {
                    _selectedGO.tag = "UnSelected";
                }
            }


        }


    }
}
