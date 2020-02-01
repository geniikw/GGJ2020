using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public Text score;

    public SetItem setItem;

    

    public void ResetGame(){
        setItem.Initalize();
        BGManager.i.ScoreReset();
        gameObject.SetActive(false);
    }   
}
