using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnRadius = 2f;
    [SerializeField] private int maxActiveNpcs = 35;
    [SerializeField] private float spawnDelay = 1f; // Задержка между спавнами

    [Header("Gizmo Settings")]
    [SerializeField] private Color spawnPointColor = Color.green;
    [SerializeField] private float spawnPointRadius = 0.5f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
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

            // Задержка перед следующей проверкой
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnNpc()
    {
        GameObject npc = NpcPool.Instance.GetNpc();
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
