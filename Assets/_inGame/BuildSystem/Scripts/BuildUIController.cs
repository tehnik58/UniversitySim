using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuildUIController : MonoBehaviour
{
    [FormerlySerializedAs("buildingSO")] [SerializeField] private Building_SO buildingSo;
    [FormerlySerializedAs("Build_btn")] [SerializeField] private GameObject buildBtn;
    public UnityAction<int> Refrash;
    private List<GameObject> _uIprefabs = new List<GameObject>();
    public int targetIndex = 0;
    public bool IsHovered = false;
    
    private void BtnEventClick(int i)
    {
        targetIndex = i;
        Refrash?.Invoke(i);
    }

    public void SetUIPosition(Vector3 pos)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
    }
    
    private void Awake()
    {
        for (int i = 0; i < buildingSo.Buildings.Count; i++)
        {
            GameObject build_btn = Instantiate(buildBtn);
            build_btn.GetComponentInChildren<TMP_Text>().SetText($"{buildingSo.Buildings[i].buildingName}\n{buildingSo.Buildings[i].price}");
            build_btn.transform.SetParent(transform);
            _uIprefabs.Add(build_btn);
            
            int _i = i;
            build_btn.GetComponent<Button>().onClick.AddListener(() => BtnEventClick(_i));
            EventTrigger trigger = build_btn.gameObject.AddComponent<EventTrigger>();
        
            var pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data); });
            trigger.triggers.Add(pointerEnter);
        
            var pointerExit = new EventTrigger.Entry();
            pointerExit.eventID = EventTriggerType.PointerExit;
            pointerExit.callback.AddListener((data) => { OnPointerExitDelegate((PointerEventData)data); });
            trigger.triggers.Add(pointerExit);
        }
    }

    private void OnPointerEnterDelegate(PointerEventData data)
    {
        IsHovered = true;
    }
    
    private void OnPointerExitDelegate(PointerEventData data)
    {
        IsHovered = false;
    }
}
