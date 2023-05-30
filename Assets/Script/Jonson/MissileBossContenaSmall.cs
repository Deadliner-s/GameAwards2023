using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MissileBossContenaSmall : MonoBehaviour
{
    Vector3 ToPos;              //î≠éÀêÊ 
    Vector3 StartPoint;
    Vector3 EndPoint;
    Vector3 ControlPoint;
    Vector3 Move;
    Vector3 Point;
    GameObject BossFlg;
    public bool First = false;
    System.Random rand = new System.Random();


    float time;
    // Start is called before the first frame update
    void Start()
    {
        BossFlg = GameObject.Find("BossManager");
        if (!BossFlg.GetComponent<MainBossHp>().BreakFlag)
        {
            time = 0.0f;
            ToPos = GameObject.Find("Player").transform.position;

            StartPoint = transform.position;
            EndPoint = ToPos;
            ControlPoint = new Vector3(StartPoint.x - 1.0f + (rand.Next(10) * 0.2f), EndPoint.y + (rand.Next(12) * 0.1f), StartPoint.z - 0.2f - (rand.Next(10) * 0.1f));

            if (!First)
                EndPoint.z = ToPos.z - rand.Next(8) * 0.4f;
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        if(BossFlg.GetComponent<MainBossHp>().BreakFlag)
        {
            Destroy(gameObject,0.0f);
        }

        time += 0.01f * Time.timeScale;
        float u = 1f - time;
        float uu = u * u;
        float tt = time * time;

        if (time <= 0.9f && !BossFlg.GetComponent<MainBossHp>().BreakFlag)
        {
            Point = uu * StartPoint;
            Point += 2f * u * time * ControlPoint;
            Point += tt * EndPoint;
            Move = Point - transform.position;  
        }
        else
        {
            Destroy(gameObject, 1.0f);
        }
        if(Time.timeScale != 0)
        {
            Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), Move);
            transform.rotation = rot;
            transform.position += Move * Time.timeScale;
        }
    }
}