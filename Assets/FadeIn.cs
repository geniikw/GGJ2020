using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().ColorTween(Color.clear, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
