using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class: MazeSolver
/// <summary>
/// 미로를 탐색하고, 플레이어를 경로에 따라 이동시키는 클래스
/// - BFS 알고리즘으로 최단 경로 탐색
/// - 탐색한 경로를 따라 플레이어 이동
/// </summary>
public class MazeSolver : MonoBehaviour
{
    #region Maze Data
    private GameObject[,] tileObjects;   // 미로 타일 오브젝트 배열
    private int[,] mazeMap;               // 미로 정보 배열 (0: 길, 1: 벽)
    #endregion

    #region Player Settings
    [Header("Player Settings")]
    public GameObject player;             // 이동할 플레이어 오브젝트
    public Vector2Int start;              // 시작 좌표
    public Vector2Int goal;               // 목표 좌표
    #endregion

    #region Public Methods
    /// <summary>
    /// Start 버튼 클릭 시 탐색 및 이동 시작
    /// </summary>
    public void OnClickStartButton()
    {
        LoadMazeData();

        // 플레이어를 시작 위치로 이동
        player.transform.position = tileObjects[start.y, start.x].transform.position;

        // BFS 탐색으로 경로 찾기
        List<Vector2Int> path = FindPathByBFS(start, goal);
        if (path.Count > 0)
            StartCoroutine(MoveAlongPath(path));
    }
    #endregion

    #region Maze Operations
    /// <summary>
    /// MazeManager에서 타일과 맵 데이터 불러오기
    /// </summary>
    private void LoadMazeData()
    {
        tileObjects = MazeManager.Instance.tileObjects;
        mazeMap = MazeManager.Instance.mazeMap;
    }
    #endregion

    #region Path Finding (BFS)
    /// <summary>
    /// BFS 알고리즘으로 최단 경로 탐색
    /// </summary>
    /// <param name="start">시작 위치</param>
    /// <param name="goal">목표 위치</param>
    /// <returns>경로 리스트</returns>
    private List<Vector2Int> FindPathByBFS(Vector2Int start, Vector2Int goal)
    {
        Queue<Vector2Int> queue = new();
        Dictionary<Vector2Int, Vector2Int> parent = new();

        queue.Enqueue(start);
        parent[start] = start;

        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (current == goal)
                break;

            foreach (var dir in directions)
            {
                Vector2Int next = current + dir;

                if (IsInsideMaze(next) && mazeMap[next.y, next.x] == 0 && !parent.ContainsKey(next))
                {
                    queue.Enqueue(next);
                    parent[next] = current;
                }
            }
        }

        return BuildPath(parent, start, goal);
    }

    /// <summary>
    /// 부모 정보를 기반으로 경로 생성
    /// </summary>
    private List<Vector2Int> BuildPath(Dictionary<Vector2Int, Vector2Int> parent, Vector2Int start, Vector2Int goal)
    {
        List<Vector2Int> path = new();

        if (!parent.ContainsKey(goal))
        {
            Debug.LogError("경로를 찾을 수 없습니다!");
            return path;
        }

        Vector2Int current = goal;
        while (current != start)
        {
            path.Add(current);
            current = parent[current];
        }
        path.Add(start);
        path.Reverse();

        return path;
    }

    /// <summary>
    /// 주어진 좌표가 미로 내부인지 체크
    /// </summary>
    private bool IsInsideMaze(Vector2Int pos)
    {
        return pos.y >= 0 && pos.y < mazeMap.GetLength(0) && pos.x >= 0 && pos.x < mazeMap.GetLength(1);
    }
    #endregion

    #region Player Movement
    /// <summary>
    /// 경로를 따라 플레이어 이동
    /// </summary>
    private IEnumerator MoveAlongPath(List<Vector2Int> path)
    {
        foreach (var point in path)
        {
            Vector3 targetPos = tileObjects[point.y, point.x].transform.position;

            while (Vector3.Distance(player.transform.position, targetPos) > 0.01f)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, Time.deltaTime * 5f);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion
}
#endregion
