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

    public GameObject score;

    public GameObject itemPrefab;

    public ItemObject corePrefab;
    public Slot coreSlot;

    public List<int> _itemList;

    public Button complete;
    public List<GameObject> shopPad;

    public static List<ItemObject> tempList = new List<ItemObject>();
    public static List<ItemObject> shopList = new List<ItemObject>();

    public Text goldText;

    public int _gold;

    private void Awake()
    {
        i = this;

    }

    public void SetGold(int gold)
    {
        _gold = gold;
        goldText.text = _gold.ToString();
    }

    private void Start()
    {
        SetGold(DB.i.initGold);
    }

    public void Initalize()
    {
        SetGold(DB.i.initGold);
        goldText.gameObject.SetActive(true);

        tempList.Clear();
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
        goldText.gameObject.SetActive(false);

        foreach (var i in shopPad)
        {
            i.SetActive(false);
        }

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

        tempList.Clear();
        tempList = FindObjectsOfType<ItemObject>().Where(i => !i.isClonable).ToList();
        foreach (var item in FindObjectsOfType<ItemObject>())
        {
            item.gameObject.SetActive(false);
            item.enabled = false;
        }

        guide.SetActive(true);

        inven.GetComponent<Graphic>().MoveUI(Vector2.zero, 0.2f, AnimationCurve.EaseInOut(0, 0, 1, 1));

        this.Scale(Vector3.one * 1.4f, 0.2f, AnimationCurve.EaseInOut(0, 0, 1, 1));

        complete.gameObject.SetActive(false);
    }

    public void ValidateButton()
    {
        var isView = true;
        complete.interactable = isView;
    }

    public void Resetup()
    {
        SetGold(_gold + DB.i.addGold);
        Reposition();
    }

    public void Reposition()
    {
        foreach (var i in shopPad)
        {
            i.SetActive(true);
        }

        inven.GetComponent<Graphic>().Move(setupPosition, 0.2f, AnimationCurve.EaseInOut(0, 0, 1, 1));

        this.Scale(Vector3.one, 0.2f, AnimationCurve.EaseInOut(0, 0, 1, 1));

        score.SetActive(false);
        complete.gameObject.SetActive(true);
        goldText.gameObject.SetActive(true);

        foreach (var i in FindObjectsOfType<ButtonAnimation>())
        {
            i.gameObject.SetActive(false);
        }

        foreach (var s in shopList)
            s.gameObject.SetActive(true);
        tempList = FindObjectsOfType<ItemObject>().Where(i => !i.isClonable).ToList();
        foreach (var i in tempList)
        {
            i.menu.enabled = false;
            i.enabled = true;
            this.StartChain().Wait(0.21f).Call(() =>
            {
                i.transform.SetParent(transform);
                i.transform.SetAsLastSibling();
            });

        }
    }


}
