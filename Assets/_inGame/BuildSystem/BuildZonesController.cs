using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildZonesController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> buildZones = new List<GameObject>();
    [SerializeField]
    private UISysBuilder _uiBuild;
    [SerializeField]
    private Canvas canvas;
    public bool isClicked = false;
    public bool isHoverOnUI = false;
    public bool isHoverOnBuildZone;
    
    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
            buildZones.Add(transform.GetChild(i).gameObject);
    }

    public void SetIsHoverOnBuildZone(bool _isHoverOnBuildZone)
    {
        isHoverOnBuildZone = _isHoverOnBuildZone;
    }
    
    public void SetIsHoverOnUI(bool _isHoverOnUI)
    {
        isHoverOnUI = _isHoverOnUI;
    }

    public void SwitchFantomModel(GameObject _fantomModel)
    {
        foreach (GameObject _buildZone in buildZones)
            if (_buildZone.TryGetComponent<FantomObjController>(out FantomObjController fantom))
                fantom.SetFantom(_fantomModel);
    }
    
    public void OnBuildZoneSelect(GameObject _buildZone)
    {
        if (isHoverOnUI)
            return;
        isClicked = true;
        foreach (GameObject __buildZone in buildZones)
            if (__buildZone.TryGetComponent<FantomObjController>(out FantomObjController fantom))
                fantom.SetFantomStatusActive(false, false);
        if (_buildZone.TryGetComponent<FantomObjController>(out FantomObjController _fantom))
        {
            _fantom.SetFantomStatusActive(false, true);
            isClicked = true;
            _uiBuild.gameObject.SetActive(true);
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                Input.mousePosition,
                null, // Для Screen Space - Overlay можно передать null
                out Vector2 localPoint
            );
            _uiBuild.gameObject.GetComponent<RectTransform>().anchoredPosition = localPoint + Vector2.up * 100;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isClicked && !isHoverOnBuildZone && !isHoverOnUI)
        {
            foreach (GameObject _buildZone in buildZones)
                if (_buildZone.TryGetComponent<FantomObjController>(out FantomObjController fantom))
                {
                    fantom.SetFantomStatusActive(true, false);
                    isClicked = false;
                    _uiBuild.gameObject.SetActive(false);
                }
        }
    }
}
