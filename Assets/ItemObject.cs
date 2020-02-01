using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ItemObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isClonable = true;//참이면 복사해서 원래 자리에 놓는다.
    //참이면 코스트가 부족할시 드래그를 하지 않는다.
    
    public int itemIdx;

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
