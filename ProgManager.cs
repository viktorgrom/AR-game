using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgManager : MonoBehaviour
{
    [SerializeField] private GameObject planeMarkerPrefab;

    private ARRaycastManager arRaycastManagerScript;

    private Vector2 _touchPosition;

    [SerializeField] private GameObject _objectToSpawn;

    private PlacementIndicator _placementIndicator;


    void Start()
    {
        _placementIndicator = FindObjectOfType<PlacementIndicator>();
        arRaycastManagerScript = FindObjectOfType<ARRaycastManager>();

        planeMarkerPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
         ShowMarker();


       // SpawnCatapult();


    }

    private void ShowMarker()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        arRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);


        if (hits.Count > 0)
        {
           /* planeMarkerPrefab.transform.position = hits[0].pose.position;
            planeMarkerPrefab.transform.rotation = hits[0].pose.rotation;

            if (!planeMarkerPrefab.activeSelf)
                planeMarkerPrefab.SetActive(true);
            */

            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Instantiate(_objectToSpawn, hits[0].pose.position, _objectToSpawn.transform.rotation);
            }

        }
    }

    //spawn in touch place
    private void SpawnCatapult()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            /*List<ARRaycastHit> hits = new List<ARRaycastHit>();

            arRaycastManagerScript.Raycast(_touchPosition, hits, TrackableType.Planes);

            Touch touch = Input.GetTouch(0);
            _touchPosition = touch.position;*/


            Instantiate(_objectToSpawn, _placementIndicator.transform.position, _objectToSpawn.transform.rotation);
        }
    }
}
