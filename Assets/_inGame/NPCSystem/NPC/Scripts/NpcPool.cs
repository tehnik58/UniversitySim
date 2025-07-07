using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPool : MonoBehaviour
{
    public static NpcPool Instance { get; private set; }

    [Header("Pool Settings")]
    public GameObject studentPrefab;
    public GameObject teacherPrefab;
    public int initialPoolSize = 25;
    public int maxPoolSize = 50;

    private LinkedList<GameObject> studentPool = new LinkedList<GameObject>();
    private LinkedList<GameObject> teacherPool = new LinkedList<GameObject>();
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
        if (studentPrefab == null || teacherPrefab == null)
        {
            Debug.LogError("One or both NPC prefabs are not assigned in NpcPool!");
            return;
        }

        // Calculate initial counts based on ratio (2 teachers per 10 students)
        int totalInitial = initialPoolSize;
        int teacherCount = Mathf.RoundToInt(totalInitial * 2f / 12f);
        int studentCount = totalInitial - teacherCount;

        for (int i = 0; i < studentCount; i++)
        {
            CreateNewNpc(studentPrefab, studentPool, false);
        }
        for (int i = 0; i < teacherCount; i++)
        {
            CreateNewNpc(teacherPrefab, teacherPool, true);
        }

        isInitialized = true;
    }

    private GameObject CreateNewNpc(GameObject prefab, LinkedList<GameObject> pool, bool isTeacher)
    {
        GameObject npc = Instantiate(prefab, transform);
        npc.SetActive(false);
        NpcType typeComp = npc.GetComponent<NpcType>();
        if (typeComp == null)
        {
            typeComp = npc.AddComponent<NpcType>();
        }
        typeComp.isTeacher = isTeacher;
        pool.AddLast(npc); // Добавляем в конец при инициализации
        return npc;
    }

    public GameObject GetNpc(bool isTeacher)
    {
        if (!isInitialized) return null;

        LinkedList<GameObject> targetPool = isTeacher ? teacherPool : studentPool;
        GameObject prefab = isTeacher ? teacherPrefab : studentPrefab;

        if (targetPool.Count == 0 && activeCount < maxPoolSize)
        {
            // Создаём новый NPC и добавляем в конец
            GameObject newNpc = CreateNewNpc(prefab, targetPool, isTeacher);
            newNpc.SetActive(false);
        }

        if (targetPool.Count == 0) return null;

        // Берём первый элемент из очереди
        GameObject npc = targetPool.First.Value;
        targetPool.RemoveFirst();
        npc.SetActive(true);
        activeCount++;
        return npc;
    }

    public void ReturnNpc(GameObject npc)
    {
        if (npc == null) return;

        npc.SetActive(false);
        npc.transform.SetParent(transform);

        NpcType typeComp = npc.GetComponent<NpcType>();
        if (typeComp != null && typeComp.isTeacher)
        {
            teacherPool.AddFirst(npc); // Добавляем преподавателя в начало очереди
        }
        else
        {
            studentPool.AddFirst(npc); // Добавляем студента в начало очереди
        }
        activeCount--;
    }

    public int GetActiveCount() => activeCount;
}
