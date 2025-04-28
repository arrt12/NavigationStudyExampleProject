using System.Collections;
using UnityEngine;

#region Class: MazeManager
/// <summary>
/// �̷θ� �����ϰ� �����ϴ� �Ŵ��� Ŭ����
/// - Ÿ�� ���� �� �ʱ�ȭ
/// - �� ���� ��� ����
/// </summary>
public class MazeManager : MonoBehaviour
{
    #region Singleton
    public static MazeManager Instance { get; private set; }
    #endregion

    #region Serialized Fields
    [Header("Maze Settings")]
    public GameObject tilePrefab;        // �̷� Ÿ�� ������
    public int rows = 5;                 // �̷� �� ��
    public int cols = 5;                 // �̷� �� ��
    #endregion

    #region Maze Data
    [HideInInspector] public GameObject[,] tileObjects; // Ÿ�� ������Ʈ �迭
    [HideInInspector] public int[,] mazeMap;             // �̷� ���� �迭 (0: ��, 1: ��)
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
    /// �̱��� �ʱ�ȭ
    /// </summary>
    private void InitializeSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// �̷� Ÿ�� �ʱ�ȭ
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

                mazeMap[y, x] = 0; // �⺻�� ��(0)

                SetTileColor(y, x);
            }
        }
    }
    #endregion

    #region Tile Control
    /// <summary>
    /// Ư�� Ÿ���� ������ ����
    /// </summary>
    /// <param name="y">�� �ε���</param>
    /// <param name="x">�� �ε���</param>
    public void SetWall(int y, int x)
    {
        mazeMap[y, x] = 1;

        SpriteRenderer sr = tileObjects[y, x].GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.black; // ���� ������
    }

    /// <summary>
    /// Ÿ�� ������ ���� (����/��ǥ/�Ϲ�)
    /// </summary>
    /// <param name="y">�� �ε���</param>
    /// <param name="x">�� �ε���</param>
    private void SetTileColor(int y, int x)
    {
        SpriteRenderer sr = tileObjects[y, x].GetComponent<SpriteRenderer>();
        if (sr == null)
            return;

        if (y == 0 && x == 0)
            sr.color = Color.red;    // ���� ����
        else if (y == rows - 1 && x == cols - 1)
            sr.color = Color.blue;   // ��ǥ ����
        else
            sr.color = Color.white;  // �⺻ ��
    }
    #endregion
}
#endregion
