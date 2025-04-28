using UnityEngine;
using UnityEngine.UI;

#region Class: WallButton
/// <summary>
/// 버튼을 클릭하면 MazeManager에 벽을 생성 요청하고, 버튼 색을 변경하는 클래스
/// </summary>
[RequireComponent(typeof(Button), typeof(Image))]
public class WallButton : MonoBehaviour
{
    #region Serialized Fields
    [Header("Wall Tile Coordinates")]
    [Tooltip("X 좌표 (열)")]
    public int x;

    [Tooltip("Y 좌표 (행)")]
    public int y;
    #endregion

    #region Cached Components
    private Button button;    // 버튼 컴포넌트
    private Image image;      // 이미지 컴포넌트
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
    /// 필요한 컴포넌트들을 캐싱
    /// </summary>
    private void CacheComponents()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    /// <summary>
    /// 버튼 클릭 시 벽 생성 요청 및 색 변경
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
