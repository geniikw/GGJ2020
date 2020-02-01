using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData ins;

    public Color empty;
    public Color anchorCell;
    public Color defender;
    public Color motherShip;

    public int initialCost = 1000;

    public Vector2Int phoneSize = new Vector2Int(3, 5);

    // Start is called before the first frame update
    void Awake()
    {
        ins = this;
    }


}
