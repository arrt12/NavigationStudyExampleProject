using UnityEngine;
using UnityEngine.UI;

#region Class: WallButton
/// <summary>
/// ��ư�� Ŭ���ϸ� MazeManager�� ���� ���� ��û�ϰ�, ��ư ���� �����ϴ� Ŭ����
/// </summary>
[RequireComponent(typeof(Button), typeof(Image))]
public class WallButton : MonoBehaviour
{
    #region Serialized Fields
    [Header("Wall Tile Coordinates")]
    [Tooltip("X ��ǥ (��)")]
    public int x;

    [Tooltip("Y ��ǥ (��)")]
    public int y;
    #endregion

    #region Cached Components
    private Button button;    // ��ư ������Ʈ
    private Image image;      // �̹��� ������Ʈ
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        CacheComponents();
    }

    private void Start()
    {
        if (button != null)
            button.onClick.AddListener(OnClickWallButton);
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// �ʿ��� ������Ʈ���� ĳ��
    /// </summary>
    private void CacheComponents()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    /// <summary>
    /// ��ư Ŭ�� �� �� ���� ��û �� �� ����
    /// </summary>
    private void OnClickWallButton()
    {
        if (MazeManager.Instance != null)
            MazeManager.Instance.SetWall(y, x);

        if (image != null)
            image.color = Color.black;
    }
    #endregion
}
#endregion
