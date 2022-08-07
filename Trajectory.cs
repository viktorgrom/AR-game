using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private GameObject _catapult;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _catapult = GameObject.Find("Catapult(Clone)");
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100];
        lineRenderer.positionCount = points.Length;

        for(int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;

            points[i] = origin + speed * time + Physics.gravity * time * time / 2;

            if(points[i].y < _catapult.gameObject.transform.position.y - 1)
            {
                lineRenderer.positionCount = i;
                break;
            }
        }
        lineRenderer.SetPositions(points);
    }
}
