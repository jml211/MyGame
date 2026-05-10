using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //飞机的Transform
    public Transform plane;
    //飞机和摄像机之间的距离的向量
    public Vector3 direct;

    void Start()
    {
        //通过Tab标签，找到飞机模型
        plane = GameObject.FindWithTag("Player").transform;
        Debug.Log(plane.name);
        //飞机模型的位置减去摄像机的位置，得出它们之间的距离
        direct = plane.position - transform.position;
    }


    void Update()
    {
        //更新摄像机的位置，使飞机移动时，摄像机一直与它保持固定的距离
        transform.position = plane.position - direct;
    }
}