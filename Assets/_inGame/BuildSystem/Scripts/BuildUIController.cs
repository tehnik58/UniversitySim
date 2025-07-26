using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildUIController : MonoBehaviour
{
    [SerializeField] private Building_SO buildingSO;
    [SerializeField] private GameObject Build_btn;
    public UnityAction<int> Refrash;
    private List<GameObject> UIprefabs = new List<GameObject>();
    public int targetIndex = 0;
    
    private void BtnEventClick(int i)
    {
        targetIndex = i;
        Refrash?.Invoke(i);
    }
    
    private void BtnEventHover(int i)
    {
        Refrash?.Invoke(i);
    }
    
    private void Awake()
    {
        for (int i = 0; i < buildingSO.Buildings.Count; i++)
        {
            GameObject build_btn = Instantiate(Build_btn);
            build_btn.GetComponentInChildren<TMP_Text>().SetText($"{buildingSO.Buildings[i].buildingName}\n{buildingSO.Buildings[i].price}");
            build_btn.transform.SetParent(transform);
            UIprefabs.Add(build_btn);
            
            int _i = i;
            build_btn.GetComponent<Button>().onClick.AddListener(() => BtnEventClick(_i));
        }
    }
}
