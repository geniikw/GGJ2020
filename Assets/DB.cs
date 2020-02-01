using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB : MonoBehaviour
{
    public static DB i;

    public int fireScore = 50;
    public int destroyCoreScore = -100;
    // Start is called before the first frame update
    void Start()
    {
        i = this;
    }
}
