using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SetItem : MonoBehaviour
{
    public static SetItem i;

    public Transform inven;


    public GameObject slotPrefab;
    public GameObject itemPrefab;

    public List<int> _itemList;
    public int _cost;

    public Text costText;

    private void Awake()
    {
        i = this;
    }

    public void Initialize(List<int> itemIdx, int cost)
    {
        _itemList = itemIdx;
        _cost = cost;
        costText.text = _cost.ToString("N0");
        for (int i = 0; i < 15; i++)
        {
            var go = Instantiate(slotPrefab);
            go.transform.SetParent(inven);
            go.GetComponent<Slot>().slotIdx = i;
        }
    }

    public void UpdateCostLabel()
    {
        costText.text = _cost.ToString("N0");
    }

    [TestMethod]
    public void TestINit()
    {
        var testInven = Enumerable.Repeat(0, 15);
        Initialize(testInven.ToList(), 10000);
    }

    private void OnEnable()
    {
        Initialize(GameManager.i.GetInven(), GameManager.i.GetMyCost());
    }

    public void OnComplete()
    {
        GameManager.SendEvent(PK.CompleteItem, new object[] { MonoPlayer.i._myIdx, _itemList.ToList() });
        gameObject.SetActive(false);
    }
}
