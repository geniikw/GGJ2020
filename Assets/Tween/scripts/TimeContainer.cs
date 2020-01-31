using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// float으로 치환된다.(time container가 필요한 곳에 1f 이런식으로 넣어도 됨.))
/// </summary>
public class TimeContainer
{
    public float t = 0f;
    public float time = 1f;
    public static Dictionary<string, List<TimeContainer>> st_allList = new Dictionary<string, List<TimeContainer>>();
    public string _id;
    private bool m_cancel = false;
    private bool m_neverCencel = false;
    public TimeContainer(string id, float time,bool neverCancel= false)
    {
        m_neverCencel = neverCancel;
        _id = id;
        t = 0;
        this.time = time;
        if (!st_allList.ContainsKey(id))
            st_allList.Add(id, new List<TimeContainer>());
        st_allList[id].Add(this);
    }
    public static int GetCount(string m_listName)
    {
        if (st_allList.ContainsKey(m_listName))
            return st_allList[m_listName].Count;
        else
            return 0;
    }
    public static void Clear(string m_listName)
    {
        if (!st_allList.ContainsKey(m_listName))
            return;
        foreach (var timeContainer in st_allList[m_listName])
        {
            if(timeContainer.m_neverCencel)
                continue;

            timeContainer.t = 1f;
            timeContainer.m_cancel = true;
        }
    }

    public static void ContainClear(string m_listName)
    {
        foreach (var list in st_allList)
        {
            if (list.Key.Contains(m_listName))
            {
                foreach (var value in list.Value)
                {
                    value.t = 1f;
                    value.m_cancel = true;
                }
            }
        }
    }
    
    public void Reset()
    {
        if (m_cancel)
            return;
        t = 0;
    }
    public void Cancel()
    {
        m_cancel = true;
        t = 1f;
    }
    public void Complete()
    {
        st_allList[_id].Remove(this);
    }
    public static implicit operator TimeContainer(float time)
    {
        return new TimeContainer("AutoBinding", time);
    }
    public static void AllClear()
    {
        foreach (string listName in st_allList.Keys)
        {
            Clear(listName);
        }
    }
}