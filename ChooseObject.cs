using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseObject : MonoBehaviour
{
    private PlacementIndicator _placementIndicatorScript;

    public Button _button;
    public GameObject choosedGameObject;

    private void Start()
    {
        _placementIndicatorScript = FindObjectOfType<PlacementIndicator>();

        _button.onClick.AddListener(ChooseObjectToSpawn);
    }

    private void ChooseObjectToSpawn()
    {
        _placementIndicatorScript.objectToSpawn = choosedGameObject;
        _placementIndicatorScript.choosedObject = true;
        _placementIndicatorScript.scrollView.SetActive(false);
    }
}
