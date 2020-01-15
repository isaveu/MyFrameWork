﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLife : MonoBehaviour {


    //游戏进入后台时执行该方法 pause为true 切换回前台时pause为false
    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //切换到后台时执行
            MyLog.LogWithTag("离开游戏暂停开始", LogTag.UnityTag_Info);
        }
        else
        {
            //切换到前台时执行，游戏启动时执行一次
            MyLog.LogWithTag("进入游戏暂停结束", LogTag.UnityTag_Info);
        }
    }
    //游戏失去焦点也就是进入后台时 focus为false 切换回前台时 focus为true
    void OnApplicationFocus(bool focus)
    {

        if (focus)
        {
            //切换到前台时执行，游戏启动时执行一次
            MyLog.LogWithTag("进入游戏获得焦点", LogTag.UnityTag_Info);
        }
        else
        {
            //切换到后台时执行
            MyLog.LogWithTag("离开游戏失去焦点", LogTag.UnityTag_Info);
           
        }
    }

    //1:正常退，正常回调。
    //2:当前应用双击Home，然后Kill(IOS 有回调，android 没回调)
    //3:跳出当前应用，然后Kill(IOS和Android都没回调)
    void OnApplicationQuit()
    {
        MyLog.LogWithTag("退出游戏", LogTag.UnityTag_Info);
    }
}
