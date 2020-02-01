using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler
{

    public int x;
    public int y;

    public bool isConfirm = false;
    /// <summary>
    /// 0은 빈공간
    /// 1은 마더쉽
    /// 2는 플레이어
    /// 3는 앵커
    /// </summary>
    public int state = 0;

    public void SetState(int state)
    {
        this.state = state;

        switch (state)
        {
            case 0:
                color = GameData.ins.empty;
                break;
            case 1:
                isConfirm = true;
                color = GameData.ins.motherShip;
                break;
            case 2:
                color = GameData.ins.defender;
                break;
            case 3:
                color = GameData.ins.anchorCell;
                break;
        }

    }

    Selector _selector;
    Image _image;


    private void Start()
    {
        _selector = GetComponentInParent<Selector>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        _selector.SetXY(new Vector2Int(x,y));
    }

    public Color color
    {
        set
        {
            if (_image == null)
                _image = GetComponent<Image>();
            _image.color = value;
        }
    }
}