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
        // �ŏ���activeObject���A�N�e�B�u�ŁAinactiveObject����A�N�e�B�u�Ƃ���
        activeObject.SetActive(true);
        inactiveObject.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= Delay && timer <= Delay + LaserTime)
        {
            timer2 += Time.deltaTime;

            // �w�肵���������ƂɃI�u�W�F�N�g�̃A�N�e�B�u��Ԃ�؂�ւ���
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
        // activeObject��inactiveObject�̃A�N�e�B�u��Ԃ����ւ���
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
