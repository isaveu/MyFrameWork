﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 封装了一些常用的功能
/// </summary>
public class MyTools : MonoBehaviour {

    /// <summary>
    /// 在list范围内选出n个不同对象
    /// </summary>
    /// <param name="list"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static List<int> RandomCount(List<int> list, int n)
    {
        if (list.Count <= n) return null;
        List<int> newList = new List<int>();
        int m = UnityEngine.Random.Range(0, list.Count);
        newList.Add(list[m]);
        while (newList.Count < n)
        {
            int a = UnityEngine.Random.Range(0, list.Count);
            int num = list[a];
            if (!newList.Contains(num))
                newList.Add(num);
        }
        return newList;
    }
    /// <summary>
    /// 大额数字转成带单位的
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public static string GetLongString(long money)
    {
        if (money < Mathf.Pow(10, 3))
        {
            return money.ToString();
        }
        else if (money >= Mathf.Pow(10, 3) && money < Mathf.Pow(10, 6))
        {
            return (money / (Mathf.Pow(10, 3) + 0.0f)).ToString("f1") + "K";
        }
        else if (money >= Mathf.Pow(10, 6) && money < Mathf.Pow(10, 9))
        {
            return (money / (Mathf.Pow(10, 6) + 0.0f)).ToString("f1") + "M";
        }
        else if (money >= Mathf.Pow(10, 9) && money < Mathf.Pow(10, 12))
        {
            return (money / (Mathf.Pow(10, 9) + 0.0f)).ToString("f1") + "B";
        }
        string B = (money / (Mathf.Pow(10, 9) + 0.0f)).ToString("f1") + "B";
        return B;
    }
    /// <summary>
    /// 判断是否点击在Ui上面
    /// </summary>
    /// <returns></returns>
    public static bool IsPointerOverGameObject()
    {
        PointerEventData eventData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;
        List<RaycastResult> list = new List<RaycastResult>();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, list);
        return list.Count > 0;
    }
    /// <summary>
    /// 判断手机语言
    /// </summary>
    /// <returns></returns>
    public static string GetUserLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.ChineseSimplified:
                return "CN";
            case SystemLanguage.ChineseTraditional:
                return "TW";
            case SystemLanguage.English:
                return "EN";
            case SystemLanguage.Japanese:
                return "JP";
            case SystemLanguage.Korean:
                return "KO";
            case SystemLanguage.German:
                return "DE";
            case SystemLanguage.French:
                return "FR";
            case SystemLanguage.Russian:
                return "RU";
            case SystemLanguage.Swedish:
                return "SV";
            case SystemLanguage.Portuguese:
                return "PT";
            case SystemLanguage.Spanish:
                return "ES";
            case SystemLanguage.Thai:
                return "TH";
            case SystemLanguage.Unknown:
                return "EN";
            default:
                return "EN";
        };
    }

    /// <summary>
    /// 是否为同一天，每次打开判定一次
    /// </summary>
    /// <returns></returns>
    public static bool IsToday()
    {
        System.DateTime currenTime = new System.DateTime();
        currenTime = System.DateTime.Now;
        int Month = currenTime.Month;
        int Day = currenTime.Day;
        if (!PlayerPrefs.HasKey("DayTime"))
        {
            PlayerPrefs.SetInt("DayTime", Day);
            PlayerPrefs.SetInt("MonthTime", Month);
        }
        if (Day == PlayerPrefs.GetInt("DayTime") && Month == PlayerPrefs.GetInt("MonthTime"))
        {
            //Debug.LogError("Daytime" + Day+"MonthTime"+Month);
            return true;
        }
        else
        {
            Debug.LogError("Daytime111" + Day + "MonthTime" + Month);
            PlayerPrefs.SetInt("DayTime", Day);
            PlayerPrefs.SetInt("MonthTime", Month);
            return false;
        }

    }


}
