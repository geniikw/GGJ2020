using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_SPINE
using Spine.Unity;
#endif
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class CC
{
    /// <typeparam name="TOwner">보간을 값을 가지고 있는 대상</typeparam>
    /// <typeparam name="TValue">보간할 값의 타입</typeparam>
    /// <returns></returns>
    public static IEnumerator TweenRoutine<TOwner, TValue>(
        TOwner target,
        Func<TValue> end,
        Func<TOwner, TValue> startGetter,
        Action<TOwner, TValue> setter,
        Func<TValue, TValue, float, TValue> lerpFunc,
        TimeContainer tc,
        AnimationCurve curve = null)
    {
        if (curve == null)
            curve = AnimationCurve.Linear(0, 0, 1, 1);
        TValue start = default(TValue);

        if (startGetter != null)
            start = startGetter(target);
        while (tc.t < 1f)
        {
            tc.t += Time.deltaTime / tc.time;
            if (setter != null && lerpFunc != null)
                setter(target, lerpFunc(start, end(), curve.Evaluate(tc.t)));
            yield return null;
        }
        if (setter != null)
            setter(target, end());
    }

    public static Coroutine Scale(this MonoBehaviour runner, Vector3 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end, t => t.localScale, (t, e) => t.localScale = e, Vector3.LerpUnclamped, time, curve));
    }
    public static Coroutine Scale(this MonoBehaviour runner, Vector3 start, Vector3 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end, t => start, (t, e) => t.localScale = e, Vector3.LerpUnclamped, time, curve));
    }
    public static Coroutine Move(this MonoBehaviour runner, Vector3 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end, t => t.position, (t, e) => t.position = e, Vector3.Lerp, time, curve));
    }

    public static Coroutine Move(this MonoBehaviour runner, Transform end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end.position, t => t.position, (t, e) => t.position = e, Vector3.Lerp, time, curve));
    }
    public static Coroutine Stay(this MonoBehaviour runner, Transform end, TimeContainer time)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end.position, t => t.position, (t, e) => t.position = e, (a, b, t) => b, time));
    }

    public static Coroutine Move(this MonoBehaviour runner, Vector3 start, Vector3 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end, t => start, (t, e) => t.position = e, Vector3.Lerp, time, curve));
    }

    public static Coroutine MoveUI(this MonoBehaviour runner, Vector2 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.GetComponent<RectTransform>(), () => end, t => t.anchoredPosition, (t, e) => t.anchoredPosition = e, Vector2.Lerp, time, curve));
    }

    public static Coroutine MoveUI(this MonoBehaviour runner, Vector2 start, Vector2 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.GetComponent<RectTransform>(), () => end, t => start, (t, e) => t.anchoredPosition = e, Vector2.Lerp, time, curve));
    }

    public static Coroutine ChangeUISize(this MonoBehaviour runner, Vector2 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.GetComponent<RectTransform>(), () => end, t => t.sizeDelta, (t, e) => t.sizeDelta = e, Vector2.Lerp, time, curve));
    }

    public static Coroutine MoveLocal(this MonoBehaviour runner, Vector3 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end, t => t.localPosition, (t, e) => t.localPosition = e, Vector3.Lerp, time, curve));
    }
    public static Coroutine MoveLocal(this MonoBehaviour runner, Vector3 start, Vector3 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end, t => start, (t, e) => t.localPosition = e, Vector3.Lerp, time, curve));
    }
    public static Coroutine MoveRelatively(this MonoBehaviour runner, Vector3 dis, TimeContainer time, AnimationCurve curve = null)
    {
        var end = runner.transform.position + dis;
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end, t => t.position, (t, e) => t.position = e, Vector3.Lerp, time, curve));
    }
    public static Coroutine Rotation(this MonoBehaviour runner, Vector3 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end, t => t.eulerAngles, (t, e) => t.eulerAngles = e, Vector3.Lerp, time, curve));
    }
    public static Coroutine Rotation(this MonoBehaviour runner, Vector3 start, Vector3 end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner.transform, () => end, t => start, (t, e) => t.eulerAngles = e, Vector3.Lerp, time, curve));
    }
    public static Coroutine Wait(this MonoBehaviour runner, TimeContainer wait, AnimationCurve curve = null)
    {
        //2번째 인자와 템플린 인자는 필요없긴함.
        return runner.StartCoroutine(TweenRoutine<object, object>(null, null, null, null, null, wait));
    }

#if UNITY_SPINE

    public static Coroutine AlphaTween (this SkeletonAnimation runner, float end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner, () => end, sp => sp.skeleton.a, (sp, a) => sp.skeleton.a = a, Mathf.Lerp, time, curve));
    }
    public static Coroutine AlphaTween (this SkeletonAnimation runner, float start, float end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner, () => end, sp => start, (sp, a) => sp.skeleton.a = a, Mathf.Lerp, time, curve));
    }
#endif
#if NGUI
    public static Coroutine AlphaTween (this UIWidget runner, float end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner, () => end, sp => sp.alpha, (sp, a) => sp.alpha = a, Mathf.Lerp, time, curve));
    }
    public static Coroutine AlphaTween (this UIWidget runner, float start, float end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner, () => end, sp => start, (sp, a) => sp.alpha = a, Mathf.Lerp, time, curve));
    }
    public static Coroutine AlphaTween (this UIPanel runner, float end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner, () => end, sp => sp.alpha, (sp, a) => sp.alpha = a, Mathf.Lerp, time, curve));
    }
    public static Coroutine AlphaTween (this UIPanel runner, float start, float end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner, () => end, sp => start, (sp, a) => sp.alpha = a, Mathf.Lerp, time, curve));
    }

    public static Coroutine ColorTween (this UISprite runner, Color start, Color end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner, () => end, sp => start, (sp, a) => sp.color = a, Color.Lerp, time, curve));
    }
        public static Coroutine ProgressTween (this UIProgressBar runner, float start, float end, TimeContainer time, AnimationCurve curve = null) {
        return runner.StartCoroutine (TweenRoutine (runner, () => end, r => start, (p, i) => p.value = i, Mathf.Lerp, time, curve));
    }
#endif

    public static Coroutine AlphaTween(this Graphic runner, float end, TimeContainer time, bool containChild = false, AnimationCurve curve = null)
    {
        if (containChild)
        {
            var child = runner.transform.GetComponentsInChildren<Graphic>();
            foreach (var c in child)
                c.AlphaTween(end, time, false, curve);
        }

        return runner.StartCoroutine(TweenRoutine(runner, () => end, sp => sp.color.a, UGUIUtil.SetAlpha, Mathf.Lerp, time, curve));
    }

    public static Coroutine ColorTween(this Graphic runner, Color end, TimeContainer time, bool containChild = false, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner, () => end, sp => sp.color, (o, c) => o.color = c, Color.Lerp, time, curve));
    }
    public static Coroutine AlphaTween(this Graphic runner, float start, float end, TimeContainer time, bool containChild = false, AnimationCurve curve = null)
    {
        if (containChild)
        {
            var child = runner.transform.GetComponentsInChildren<Graphic>();
            foreach (var c in child)
                c.AlphaTween(start, end, time, false, curve);
        }

        return runner.StartCoroutine(TweenRoutine(runner, () => end, sp => start, UGUIUtil.SetAlpha, Mathf.Lerp, time, curve));
    }

    public static Coroutine FillTween(this Image runner, float start, float end, TimeContainer time, AnimationCurve curve = null)
    {

        return runner.StartCoroutine(TweenRoutine(runner, () => end, r => start, (p, i) => p.fillAmount = i, Mathf.Lerp, time, curve));
    }

    public static Coroutine FillTween(this Image runner, float end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner, () => end, r => r.fillAmount, (p, i) => p.fillAmount = i, Mathf.Lerp, time, curve));
    }

    public static Coroutine NumberTween(this TextMeshProUGUI runner, long end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner, () => end, LongParse, (p, i) => p.text = i.ToString("N0"), LerpUtil.LongLerp, time, curve));
    }

    public static Coroutine NumberTween(this Text runner, float end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner, () => end, FloatPare, (p, i) => p.text = i.ToString("N0"), Mathf.Lerp, time, curve));
    }

    public static Coroutine NumberTween(this Text runner,float start, float end, TimeContainer time, AnimationCurve curve = null)
    {
        return runner.StartCoroutine(TweenRoutine(runner, () => end,f=> start, (p, i) => p.text = i.ToString("N0"), Mathf.Lerp, time, curve));
    }

    private static long LongParse(TextMeshProUGUI text)
    {
        var str = text.text;
        return long.Parse(str.Replace(",", ""));
    }

    private static float FloatPare(Text text)
    {
        var str = text.text;
        return float.Parse(str.Replace(",", ""));
    }


    public static Coroutine PingPongScale(this MonoBehaviour runner, float min, float max, float length, Func<bool> isPlaying)
    {
        return runner.StartCoroutine(PingPong(runner.transform, min, max, length, isPlaying));
    }

    public static IEnumerator PingPong(Transform trans, float min, float max, float length, Func<bool> isPlay)
    {
        var st = Time.realtimeSinceStartup;
        while (isPlay())
        {
            var t = Mathf.PingPong(Time.realtimeSinceStartup - st, length) / length;
            trans.localScale = Vector3.Lerp(Vector3.one * min, Vector3.one * max, t);
            yield return null;
        }
    }

    public static Coroutine PingPongColor(this Graphic runner, Color min, Color max, float length, Func<bool> isPlaying)
    {
        return runner.StartCoroutine(PingPong(runner, min, max, length, isPlaying));
    }

    public static IEnumerator PingPong(Graphic target, Color min, Color max, float length, Func<bool> isPlay)
    {
        var st = Time.realtimeSinceStartup;
        while (isPlay())
        {
            var t = Mathf.PingPong(Time.realtimeSinceStartup - st, length) / length;
            target.color = Color.Lerp(min, max, t);
            yield return null;
        }
    }

}