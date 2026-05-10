using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    float realTimeScale = 20000f; // 实时与游戏时间的比例因子
    float gameTime = 0f;
    int virtualDays = 1;
    float virtualHour = 0;
    GameObject timeClock;
    float angle;

    public int GetVirtualDays()
    {
        return virtualDays;
    }

    public void SetVirtualDays(int value)
    {
        if (virtualDays!= value)
        {
            virtualDays = value;
            DayChange();
        }
  
    }

    public delegate void ValueChangedEventHandler(object sender, EventArgs e);

    public event ValueChangedEventHandler ValueChanged;

    public void DayChange() {
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    // Start is called before the first frame update
    void Start()
    {
        timeClock = GameObject.Find("TimeClock");
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
        gameTime += Time.deltaTime * realTimeScale;
        SetVirtualDays((int)(gameTime / (24f * 3600f))+1); // 每24小时为1虚拟天
        virtualHour = (gameTime /  3600f); // 每24小时为1虚拟天
        angle=(virtualHour / 24f) * 360f;

        timeClock.transform.Find("HourImage").rotation = Quaternion.Euler(0,0, -angle);
        timeClock.transform.Find("Day").GetComponent<Text>().text = (GetVirtualDays() ).ToString();
        Debug.Log("第 "+ gameTime / (60f) +" 分钟");
        Debug.Log("第 " + virtualHour + " 小时 旋转 "+angle);
    }


}
