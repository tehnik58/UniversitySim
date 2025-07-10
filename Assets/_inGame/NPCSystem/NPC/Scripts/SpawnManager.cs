using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnRadius = 2f;
    [SerializeField] private int maxActiveNpcs = 35;
    [SerializeField] private float spawnDelay = 1f;

    [Header("Gizmo Settings")]
    [SerializeField] private Color spawnPointColor = Color.green;
    [SerializeField] private float spawnPointRadius = 0.5f;

    private bool hasStartedSpawning = false;

    private void OnEnable()
    {
        GamplayStaticController.PauseEvent += HandlePauseEvent;
    }

    private void OnDisable()
    {
        GamplayStaticController.PauseEvent -= HandlePauseEvent;
    }
    private void HandlePauseEvent(bool isPaused)
    {
        
        if (!isPaused && !hasStartedSpawning)
        {
            StartCoroutine(SpawnRoutine());
            hasStartedSpawning = true;
        }
    }


    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (NpcPool.Instance == null)
            {
                yield return new WaitForSeconds(spawnDelay);
                continue;
            }

            if (NpcPool.Instance.GetActiveCount() < maxActiveNpcs)
            {
                SpawnNpc();
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnNpc()
    {
        // Determine NPC type with 1/6 probability for teacher
        bool isTeacher = Random.value < 1f / 10f;

        GameObject npc = NpcPool.Instance.GetNpc(isTeacher);
        if (npc == null || spawnPoint == null) return;

        Vector3 spawnPos = spawnPoint.position + Random.insideUnitSphere * spawnRadius;
        spawnPos.y = 0;

        npc.transform.position = spawnPos;
        npc.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        if (spawnPoint != null)
        {
            Gizmos.color = spawnPointColor;
            Gizmos.DrawSphere(spawnPoint.position, spawnPointRadius);
        }
    }
}
