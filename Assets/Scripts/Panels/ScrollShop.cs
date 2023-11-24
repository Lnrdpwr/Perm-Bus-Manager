using UnityEngine;

public class ScrollShop : MonoBehaviour
{
    [SerializeField] private RectTransform _lastPanel;
    [SerializeField] private RectTransform _panelsGroup;

    private Vector2 _rightCorner;

    private void Start()
    {
        _rightCorner = -_lastPanel.position;
    }

    public void SetPosition(float value)
    {
        _panelsGroup.position = Vector2.Lerp(Vector2.zero, _rightCorner, value);
    }
}
