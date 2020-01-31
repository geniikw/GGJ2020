using System;
using UnityEngine;
using UnityEngine.UI;

public static class LerpUtil{


    public static long LongLerp(long a, long b, float t){
        return (long)(a * (1-t) + b * t);
    }
}

public static class UGUIUtil{
    public static void SetAlpha(this Graphic owner, float alpha){
        var color = owner.color;
        color.a = alpha;
        owner.color = color;
    }
}