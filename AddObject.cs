using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddObject : MonoBehaviour
{
    private PlacementIndicator _placementIndicatorScript;
    private Button _button;
    void Start()
    {
        _placementIndicatorScript = FindObjectOfType<PlacementIndicator>();

        _button = GetComponent<Button>();
        _button.onClick.AddListener(AddObjectFunction);
    }

    private void AddObjectFunction()
    {
        _placementIndicatorScript.scrollView.SetActive(true);
    }
}
