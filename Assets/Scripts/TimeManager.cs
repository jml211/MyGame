using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    float realTimeScale = 2000f; // 实时与游戏时间的比例因子
    float gameTime = 0f;
    int virtualDays = 1;
    float virtualHour = 0;
    float startHour = 6;
    GameObject timeClock;
    float angle;

    int Dayhour = 6;
    int Duskhour = 12;
    int Nighthour = 20;

    bool init=true;
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

    public Transform Sun;

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
        if (!init)
        {
            virtualHour += (gameTime / 3600f); // 每24小时为1虚拟天
            init = true;
        }
        virtualHour = (gameTime /  3600f)+ startHour; // 每24小时为1虚拟天
        Debug.Log("virtualHour "+ virtualHour);
        angle=(virtualHour / 24f) * 360f;

        timeClock.transform.Find("HourImage").rotation = Quaternion.Euler(0,0, -angle);
        timeClock.transform.Find("Day").GetComponent<Text>().text = (GetVirtualDays() ).ToString();
        //Debug.Log("第 "+ gameTime / (60f) +" 分钟");
        //Debug.Log("第 " + virtualHour + " 小时 旋转 "+angle);

        //二十四小时制 当天的xx点
      //  Debug.Log((virtualHour % 24).ToString());
        int hourInDay = Convert.ToInt32((virtualHour % 24).ToString("0"));
        //  float hourInDay = float.Parse( (virtualHour % 24).ToString().Substring());
        Debug.Log("始终角度 "+ angle);
        //6 - 0  20 - 180
        Sun.rotation = Quaternion.Euler(angle-90, Sun.rotation.y, Sun.rotation.z);
    }


}
