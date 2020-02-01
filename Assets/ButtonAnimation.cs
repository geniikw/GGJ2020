using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
  
    public List<Button> buttonList;
    public Image back;
    private void OnEnable() {
        foreach(var bt in buttonList){
            var p = bt.GetComponent<RectTransform>().anchoredPosition;
            bt.MoveUI(Vector2.zero, p, 0.2f); 
        }
        this.StartChain().Wait(0.2f).Call(()=>  back.raycastTarget = true);
    }
    private void OnDisable(){
        back.raycastTarget = false;
    }

    
}
