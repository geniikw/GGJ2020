﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public bool isClonable = true;
    public int itemIdx;

    public void OnDrag(PointerEventData eventData)
    {
        if (isDrag)
            transform.position = eventData.position;
    }
    public bool isDrag = false;
    public bool isRotate = false;

    public MenuSelector menu;

    private void Start()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isRotate)
            return;

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
            if (SetItem.i._gold == 0)
            {
                isDrag = false;
                return;
            }

            SetItem.i.SetGold(SetItem.i._gold - 1);
            var clone = Instantiate(gameObject);
            clone.transform.SetParent(transform.parent);
            clone.transform.position = transform.position;
            clone.transform.localScale = Vector3.one;
            isClonable = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isRotate)
            return;

        if (!isDrag)
            return;

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

        if (fslot != null && fslot.item == this)
        {
            Debug.Log("r");
            transform.position = fslot.transform.position;
            if (itemIdx == 2)
            {
                var euler = transform.eulerAngles;
                euler.z += 90f;
                isRotate = true;
                this.Rotation(euler, 0.2f);
                this.StartChain().Wait(0.2f).Call(() => isRotate = false);

            }
            return;
        }

        if (fslot != null && fslot.item == null)
        {
            AudioManager.i.PlaySound(5);
            var slot = FindObjectsOfType<Slot>().FirstOrDefault(s => s.item == this);
            if (slot != null)
                slot.item = null;

            fslot.item = this;
            transform.position = fslot.transform.position;

        }
        else
        {
            if (itemIdx == 3)
            {
                var slot = FindObjectsOfType<Slot>().First(s => s.item == this);
                transform.position = slot.transform.position;
            }
            else
            {
                SetItem.i.SetGold(SetItem.i._gold + 1);
                var slot = FindObjectsOfType<Slot>().FirstOrDefault(s => s.item == this);
                if (slot != null)
                    slot.item = null;
                Destroy(gameObject);
            }
        }
        SetItem.i.ValidateButton();
    }
}
