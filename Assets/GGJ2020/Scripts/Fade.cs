using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade instance;

    public float fadeTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        instance.GetComponent<Image>().color = Color.black;
        In();
    }

    public static Coroutine Out()
    {
        return instance.GetComponent<Image>().AlphaTween(0f, 1f, instance.fadeTime);
    }

    public static Coroutine In()
    {
        return instance.GetComponent<Image>().AlphaTween(1f, 0f, instance.fadeTime);
    }


}
