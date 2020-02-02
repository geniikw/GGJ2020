using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Guide : MonoBehaviour, IPointerClickHandler
{
    public Text text;
    public int count = 3;

    public GameObject score;

    public Transform inventory;

    public Transform rightPosition;
    public Transform leftPosition;

    public int _currentPosition = 0;


    public void MoveRight()
    {
        _currentPosition = Mathf.Min(1, _currentPosition + 1);
        UpdatePosition();
    }

    public void MoveLeft()
    {
        _currentPosition = Mathf.Max(-1, _currentPosition - 1);
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        if (_currentPosition == -1)
            inventory.position = rightPosition.position;
        else if (_currentPosition == 0)
            inventory.localPosition = Vector3.zero;
        else if (_currentPosition == 1)
            inventory.position = leftPosition.position;

    }

    public void OnEnable()
    {
        count = 3;
        text.text = "3";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        count--;

        text.text = count.ToString();

        if (count <= 0)
        {
            foreach (var item in SetItem.tempList)
            {
                item.gameObject.SetActive(true);
                item.enabled = false;
            }

            foreach (var t in SetItem.tempList)
            {
                t.menu.enabled = true;
            }

            gameObject.SetActive(false);
            score.SetActive(true);
        }
    }
}
