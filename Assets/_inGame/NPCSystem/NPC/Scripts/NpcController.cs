using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class NpcController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.8f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float waypointThreshold = 0.5f;

    private Transform currentTarget;
    private List<Vector3> path = new List<Vector3>();
    private int currentWaypointIndex = 0;
    private bool isFirstTarget = true;

    void OnEnable()
    {
        StartCoroutine(DelayedInitialize());
    }

    private IEnumerator DelayedInitialize()
    {
        while (WaypointManager.Instance == null)
            yield return null;

        InitializeBehavior();
    }

    private void InitializeBehavior()
    {
        if (WaypointManager.Instance == null)
        {
            Debug.LogError("WaypointManager не найден! NPC будет деактивирован.");
            gameObject.SetActive(false);
            return;
        }

        currentTarget = GetNextTarget(); // Получаем первую цель

        path = GetPath(transform.position, currentTarget.position);

        if (path.Count == 0)
        {
            StartCoroutine(RetryPathfinding());
            return;
        }

        currentWaypointIndex = 0;
    }

    private Transform GetNextTarget()
    {
        if (isFirstTarget)
        {
            isFirstTarget = false; // Первый раз всегда выбираем случайную точку
            return WaypointManager.Instance.GetRandomWaypoint();
        }
        else
        {
            // После первого выбора - 5% шанс выбрать выход
            if (Random.Range(0f, 1f) < 0.05f)
            {
                return WaypointManager.Instance.GetExitPoint();
            }
            else
            {
                return WaypointManager.Instance.GetRandomWaypoint();
            }
        }
    }
    public void ResetNpc() // Метод для сброса состояния при возврате в пул
    {
        isFirstTarget = true;
        // Сброс других состояний, если нужно
    }
    private IEnumerator RetryPathfinding()
    {
        yield return new WaitForSeconds(0.5f); 
        InitializeBehavior();
    }

    void Update()
    {
        if (currentTarget == null || path.Count == 0) return;

        FollowPath();
    }

    private void FollowPath()
    {
        if (currentWaypointIndex >= path.Count) return;

        Vector3 targetPosition = path[currentWaypointIndex];
        Vector3 direction = (targetPosition - transform.position).normalized;

        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        
        transform.position += direction * moveSpeed * Time.deltaTime;

       
        if (Vector3.Distance(transform.position, targetPosition) < waypointThreshold)
        {
            currentWaypointIndex++;

            // Если путь завершён
            if (currentWaypointIndex >= path.Count)
            {
                OnPathCompleted();
            }
        }
    }

    private void OnPathCompleted()
    {
        if (currentTarget == WaypointManager.Instance.GetExitPoint())
        {
            NpcPool.Instance.ReturnNpc(gameObject);
        }
        else
        {
            
            InitializeBehavior();
        }
    }

    private List<Vector3> GetPath(Vector3 start, Vector3 end)
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(start, end, NavMesh.AllAreas, path))
        {
            return SimplifyPath(path.corners.ToList());
        }
        return new List<Vector3>();
    }

    private List<Vector3> SimplifyPath(List<Vector3> path, float minDistance = 0.5f)
    {
        List<Vector3> simplified = new List<Vector3>();
        if (path.Count == 0) return simplified;

        simplified.Add(path[0]);
        for (int i = 1; i < path.Count; i++)
        {
            if (Vector3.Distance(simplified[^1], path[i]) > minDistance)
                simplified.Add(path[i]);
        }
        return simplified;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (path.Count > 0)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i], path[i + 1]);
            }
        }
    }
}
