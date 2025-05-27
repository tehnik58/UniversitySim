using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float baseSecondsPerYear = 1800f;
    [SerializeField] private bool showProgressInTime = false;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI monthText; // Новый элемент для отображения месяца
    [SerializeField] private Image progressCircle;

    private static readonly string[] months = new string[]
    {
        "Сентябрь", "Октябрь", "Ноябрь", "Декабрь",
        "Январь", "Февраль", "Март", "Апрель",
        "Май", "Июнь", "Июль", "Август"
    };

    private float currentSpeedMultiplier = 15f;
    private float currentYearTime = 0f;
    private int currentYear = 1;
    private bool isTimerRunning = true;

    void Start()
    {
        StartCoroutine(YearCycle());
        TogglePause(true);

        GamplayStaticController.PauseEvent += TogglePause;
    }
    private void OnDestroy()
    {
        GamplayStaticController.PauseEvent -= TogglePause;
    }

    IEnumerator YearCycle()
    {
        while (true)
        {
            if (isTimerRunning && !GamplayStaticController.CheckOnLose())
            {
                float scaledDeltaTime = Time.deltaTime * currentSpeedMultiplier;
                currentYearTime += scaledDeltaTime;

                UpdateUI();

                if (currentYearTime >= baseSecondsPerYear)
                {
                    //StaticEconomicInfo.OnStartYers.Invoke();
                    StaticEconomicInfo.AcceptEconomicIteration();
                    currentYear++;
                    currentYearTime = 0f;
                    Debug.Log($"Year {currentYear} started!");
                }
            }
            if (GamplayStaticController.CheckOnLose())
                print("LOSE");
            yield return null;
        }
    }

    private void UpdateUI()
    {
        float progress = currentYearTime / baseSecondsPerYear;

        // Обновление месяца
        if (monthText != null)
        {
            int monthIndex = (int)(progress * 12) % 12;
            monthText.text = months[monthIndex];
        }

        if (progressCircle != null)
        {
            progressCircle.fillAmount = progress;
        }

        if (showProgressInTime)
        {
            int totalSeconds = Mathf.FloorToInt(currentYearTime);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            timerText.text = $"Год {currentYear}\n{minutes:00}:{seconds:00}";
        }
        else
        {
            timerText.text = $"Год {currentYear}\n{Mathf.FloorToInt(progress * 100)}%";
        }
    }

    public void SetGameSpeed(float multiplier) => currentSpeedMultiplier = multiplier;
    public void TogglePause(bool pause) => isTimerRunning = !pause;
    public void ResetTimer()
    {
        currentYear = 1;
        currentYearTime = 0f;
        UpdateUI();
    }
}
