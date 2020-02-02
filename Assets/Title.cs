using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Image fade;



    public void OnNormal()
    {
        fade.ColorTween(Color.black, 1f);

        this.StartChain().Wait(1f).Call(() =>
            SceneManager.LoadScene("Simple")
        );
    }

    public void OnMJ()
    {
        fade.ColorTween(Color.black, 1f);
        this.StartChain().Wait(1f).Call(() =>
           SceneManager.LoadScene("Simple3x3")
       );
    }
}
