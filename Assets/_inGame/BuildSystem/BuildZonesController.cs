using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildZonesController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> buildZones = new List<GameObject>();
    public bool isClicked = false;
    public bool UILock = false;
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
    
    public void OnBuildZoneSelect(GameObject _buildZone)
    {
        isClicked = true;
        foreach (GameObject __buildZone in buildZones)
            if (__buildZone.TryGetComponent<FantomObjController>(out FantomObjController fantom))
                fantom.SetFantomStatusActive(false, false);
        if (_buildZone.TryGetComponent<FantomObjController>(out FantomObjController _fantom))
        {
            _fantom.SetFantomStatusActive(false, true);
            isClicked = true;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isClicked && !isHoverOnBuildZone)
        {
            foreach (GameObject _buildZone in buildZones)
                if (_buildZone.TryGetComponent<FantomObjController>(out FantomObjController fantom))
                {
                    fantom.SetFantomStatusActive(true, false);
                    isClicked = false;
                }
        }
    }
}
