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

    public List<int> _itemList;

    public Button complete;

    public static List<ItemObject> tempList;

    private void Awake()
    {
        i = this;
    }

    public void Initalize()
    {
        complete.gameObject.SetActive(true);
        inven.position = setupPosition.position;
    }

    public void OnComplete()
    {
        foreach (var s in FindObjectsOfType<Slot>())
        {
            if(s.item != null)
                s.item.transform.SetParent(s.transform);
        }

        foreach(var item in FindObjectsOfType<ItemObject>().Where(i=>i.isClonable))
            Destroy(item.gameObject);

        tempList =  FindObjectsOfType<ItemObject>().Where(i=>!i.isClonable).ToList();
        foreach(var item in FindObjectsOfType<ItemObject>()){
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
        // foreach (var s in FindObjectsOfType<Slot>())
        // {
        //     if (s.item == null)
        //     {
        //         isView = false;
        //         break;
        //     }
        // }
        complete.interactable = isView;
    }


}
