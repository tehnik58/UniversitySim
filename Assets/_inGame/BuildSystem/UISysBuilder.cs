using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISysBuilder : MonoBehaviour
{
    [SerializeField] private Buildings_SO buildings_SO;
    [SerializeField] private GameObject BtnPanel;
    [SerializeField] private GameObject BtnPrefab;
    public List<GameObject> BtnsBuilding = new List<GameObject>();
    public SerializebleAction OnHoverEvent;
    public SerializebleAction OnExitEvent;
    public GameObjectUnityEvent onСlick;

    void Start()
    {
        foreach (var building in buildings_SO.buildings)
        {
            BtnsBuilding.Add(Instantiate(BtnPrefab, BtnPanel.transform));
            BtnsBuilding[BtnsBuilding.Count - 1].GetComponent<ButtonTextSetter>().SetText(building.buildingName);
            int _id = BtnsBuilding.Count - 1;
            BtnsBuilding[BtnsBuilding.Count - 1].GetComponent<Button>().onClick.AddListener(() => onСlick.Invoke(buildings_SO.buildings[_id].buildingPref));
        }
        gameObject.SetActive(false);
        var eventTrigger = GetComponent<EventTrigger>() ?? 
                           gameObject.AddComponent<EventTrigger>();

        // Создаем обработчик для наведения
        var pointerEnterEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        pointerEnterEntry.callback.AddListener(OnPointerEnterDynamic);
        eventTrigger.triggers.Add(pointerEnterEntry);

        // Создаем обработчик для ухода
        var pointerExitEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        pointerExitEntry.callback.AddListener(OnPointerExitDynamic);
        eventTrigger.triggers.Add(pointerExitEntry);
    }

    private void OnPointerEnterDynamic(BaseEventData data)
    {
        OnHoverEvent.Invoke();
    }

    private void OnPointerExitDynamic(BaseEventData data)
    {
        OnExitEvent.Invoke();
    }
}
