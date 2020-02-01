using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Guide : MonoBehaviour, IPointerClickHandler
{
    public Text text;
    public int count = 3;

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

            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
