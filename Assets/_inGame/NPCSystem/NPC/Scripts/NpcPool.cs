using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPool : MonoBehaviour
{
    public static NpcPool Instance { get; private set; }

    [Header("Pool Settings")]
    public GameObject personPrefab;
    public int initialPoolSize = 25;
    public int maxPoolSize = 50;

    private Queue<GameObject> pool = new Queue<GameObject>();
    private int activeCount = 0;
    private bool isInitialized = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        if (personPrefab == null)
        {
            Debug.LogError("Person prefab is not assigned in PersonPool!");
            return;
        }

        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewNpc();
        }
        isInitialized = true;
    }

    private GameObject CreateNewNpc()
    {
        GameObject person = Instantiate(personPrefab, transform);
        person.SetActive(false);
        pool.Enqueue(person);
        return person;
    }

    public GameObject GetNpc()
    {
        if (!isInitialized) return null;

        if (pool.Count == 0 && activeCount < maxPoolSize)
        {
            CreateNewNpc();
        }

        if (pool.Count == 0) return null;

        GameObject person = pool.Dequeue();
        person.SetActive(true);
        activeCount++;
        return person;
    }

    public void ReturnNpc(GameObject person)
    {
        if (person == null) return;

        person.SetActive(false);
        person.transform.SetParent(transform);
        pool.Enqueue(person);
        activeCount--;
    }

    public int GetActiveCount() => activeCount;
}
