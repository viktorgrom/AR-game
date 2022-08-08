using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    private Button _button;
    private PlacementIndicator _placementIndicator;
    void Start()
    {
        _placementIndicator = FindObjectOfType<PlacementIndicator>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(RorateFunc);
    }

    private void RorateFunc()
    {
        if (_placementIndicator.rotation)
        {
            _placementIndicator.rotation = false;
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            _placementIndicator.rotation = true;
            GetComponent<Image>().color = Color.green;
        }
    }
}
