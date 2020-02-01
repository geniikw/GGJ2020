using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public bool isClonable = true;
    //참이면 복사해서 원래 자리에 놓는다.
    //참이면 코스트가 부족할시 드래그를 하지 않는다.

    public int itemIdx;
    public int cost = 100;

    public Sprite wallSprite;
    public Sprite repairSprite;

    public void OnDrag(PointerEventData eventData)
    {
        if(isDrag)
            transform.position = eventData.position;
    }
    public bool isDrag = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        var rc = FindObjectOfType<GraphicRaycaster>();

        var result = new List<RaycastResult>();
        rc.Raycast(eventData, result);

        Slot fslot = null;
        foreach (var r in result)
        {
            fslot = r.gameObject.GetComponent<Slot>();
            if (fslot != null)
                break;
        }
        isDrag = true;

        if (isClonable)
        {
            if (SetItem.i._cost > cost)
            {
                isDrag= true;
                var clone = Instantiate(gameObject);
                clone.transform.SetParent(transform.parent);
                clone.transform.position = transform.position;
                isClonable = false;
                SetItem.i._cost -= cost;
                SetItem.i.UpdateCostLabel();
                if (fslot != null)
                    fslot.item = null;
            }
            else
                isDrag = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
        var rc = FindObjectOfType<GraphicRaycaster>();

        var result = new List<RaycastResult>();
        rc.Raycast(eventData, result);

        Slot fslot = null;
        foreach (var r in result)
        {
            fslot = r.gameObject.GetComponent<Slot>();
            if (fslot != null)
                break;
        }

        if (fslot != null && fslot.item == null)
        {//슬롯위에서 높음
            fslot.item = this;
            SetItem.i._itemList[fslot.slotIdx] = itemIdx;
            transform.position = fslot.transform.position;
        }
        else
        {

            Destroy(gameObject);
            SetItem.i._cost += cost;
            SetItem.i.UpdateCostLabel();
        }

        //슬롯위에 놔두면 들어감
        //슬롯위에 놔두지 않으면 바로 아이템을 버리고 코스트를 환불
    }
}
