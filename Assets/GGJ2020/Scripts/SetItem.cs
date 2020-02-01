using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItem : MonoBehaviour
{
    public Transform inven;

    public GameObject slotPrefab;

    public List<int> _itemList;
    public int _cost;

    public void Initialize(List<int> itemIdx, int cost)
    {
        _itemList = itemIdx;
        _cost = cost;
    }

    private void OnEnable()
    {
        Initialize(GameManager.i.GetInven(), GameManager.i.GetMyCost());
    }

    public void OnComplete()
    {
        
    }
}
