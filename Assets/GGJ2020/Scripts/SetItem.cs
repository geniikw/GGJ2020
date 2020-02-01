using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SetItem : MonoBehaviour
{
    public static SetItem i;

    public GameObject guide;

    public Transform inven;
    public Transform setupPosition;

    public GameObject itemPrefab;

    public ItemObject corePrefab;
    public Slot coreSlot;

    public List<int> _itemList;

    public Button complete;

    public static List<ItemObject> tempList;
    public static List<ItemObject> shopList;

    private void Awake()
    {
        i = this;
    }

    public void Initalize()
    {
        tempList = FindObjectsOfType<ItemObject>().Where(i => !i.isClonable).ToList();
        foreach (var item in FindObjectsOfType<ItemObject>())
        {
            Destroy(item.gameObject);
        }

        complete.gameObject.SetActive(true);
        inven.position = setupPosition.position;
        transform.localScale = Vector3.one;
        foreach (var s in shopList)
            s.gameObject.SetActive(true);





        var go = Instantiate(corePrefab);
        go.transform.SetParent(coreSlot.transform.parent);
        go.transform.position = coreSlot.transform.position;
        go.transform.localScale = Vector3.one;
        coreSlot.item = go.GetComponent<ItemObject>();

    }

    public void OnComplete()
    {
        foreach (var s in FindObjectsOfType<Slot>())
        {
            if (s.item != null)
                s.item.transform.SetParent(s.transform);
        }
        shopList = FindObjectsOfType<ItemObject>().Where(i => i.isClonable).ToList();
        foreach (var item in shopList)
        {
            item.gameObject.SetActive(false);
        }


        tempList = FindObjectsOfType<ItemObject>().Where(i => !i.isClonable).ToList();
        foreach (var item in FindObjectsOfType<ItemObject>())
        {
            item.gameObject.SetActive(false);
            item.enabled = false;
        }

        guide.SetActive(true);



        inven.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        this.Scale(Vector3.one * 1.4f, 0.2f);

        complete.gameObject.SetActive(false);
    }

    public void ValidateButton()
    {
        var isView = true;
        complete.interactable = isView;
    }


}
