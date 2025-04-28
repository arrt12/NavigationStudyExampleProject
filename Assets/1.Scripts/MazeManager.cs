using System.Collections;
using UnityEngine;

#region Class: MazeManager
/// <summary>
/// 미로를 생성하고 관리하는 매니저 클래스
/// - 타일 생성 및 초기화
/// - 벽 설정 기능 제공
/// </summary>
public class MazeManager : MonoBehaviour
{
    #region Singleton
    public static MazeManager Instance { get; private set; }
    #endregion

    #region Serialized Fields
    [Header("Maze Settings")]
    public GameObject tilePrefab;        // 미로 타일 프리팹
    public int rows = 5;                 // 미로 행 수
    public int cols = 5;                 // 미로 열 수
    #endregion

    #region Maze Data
    [HideInInspector] public GameObject[,] tileObjects; // 타일 오브젝트 배열
    [HideInInspector] public int[,] mazeMap;             // 미로 정보 배열 (0: 길, 1: 벽)
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        InitializeSingleton();
        InitializeMaze();
    }
    #endregion

    #region Initialization
    /// <summary>
    /// 싱글톤 초기화
    /// </summary>
    private void InitializeSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 미로 타일 초기화
    /// </summary>
    private void InitializeMaze()
    {
        tileObjects = new GameObject[rows, cols];
        mazeMap = new int[rows, cols];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector3 spawnPos = new Vector3(x, -y, 0);
                GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity, transform);
                tileObjects[y, x] = tile;

                mazeMap[y, x] = 0; // 기본은 길(0)

                SetTileColor(y, x);
            }
        }
    }
    #endregion

    #region Tile Control
    /// <summary>
    /// 특정 타일을 벽으로 설정
    /// </summary>
    /// <param name="y">행 인덱스</param>
    /// <param name="x">열 인덱스</param>
    public void SetWall(int y, int x)
    {
        mazeMap[y, x] = 1;

        SpriteRenderer sr = tileObjects[y, x].GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.black; // 벽은 검정색
    }

    /// <summary>
    /// 타일 색깔을 설정 (시작/목표/일반)
    /// </summary>
    /// <param name="y">행 인덱스</param>
    /// <param name="x">열 인덱스</param>
    private void SetTileColor(int y, int x)
    {
        SpriteRenderer sr = tileObjects[y, x].GetComponent<SpriteRenderer>();
        if (sr == null)
            return;

        if (y == 0 && x == 0)
            sr.color = Color.red;    // 시작 지점
        else if (y == rows - 1 && x == cols - 1)
            sr.color = Color.blue;   // 목표 지점
        else
            sr.color = Color.white;  // 기본 길
    }
    #endregion
}
#endregion
