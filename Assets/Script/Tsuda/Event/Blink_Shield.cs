using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink_Shield : MonoBehaviour
{
    public GameObject activeObject;
    public GameObject inactiveObject;
    public float switchInterval = 0.2f;
    public float Delay = 1.0f;
    public float LaserTime = 3.0f;

    private float timer = 0f;
    private float timer2 = 0f;    

    private void Start()
    {
        // 最初はactiveObjectがアクティブで、inactiveObjectが非アクティブとする
        activeObject.SetActive(true);
        inactiveObject.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= Delay && timer <= Delay + LaserTime)
        {
            timer2 += Time.deltaTime;

            // 指定した周期ごとにオブジェクトのアクティブ状態を切り替える
            if (timer2 >= switchInterval)
            {
                SwitchObjects();
                timer2 = 0f;
            }
        }

        if(timer >= Delay + LaserTime)
        {
            activeObject.SetActive(false);
            inactiveObject.SetActive(true);
        }
    }

    private void SwitchObjects()
    {
        // activeObjectとinactiveObjectのアクティブ状態を入れ替える
        if (activeObject != null)
        {
            activeObject.SetActive(!activeObject.activeSelf);
        }
        if (inactiveObject != null)
        {
            inactiveObject.SetActive(!inactiveObject.activeSelf);
        }
    }
}
