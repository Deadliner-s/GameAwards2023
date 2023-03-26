using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActiveSwitch : MonoBehaviour
{
    [Header("�A�N�e�B�u��Ԃ�؂�ւ���G�t�F�N�g")]
    [SerializeField] GameObject EffectObj;
    // �؂�ւ��鎞��
    public float SwitchTime = 10.0f;
    private float currenttime;

    // Start is called before the first frame update
    void Start()
    {
        currenttime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        bool IsActive = EffectObj.activeSelf;
        currenttime += Time.deltaTime;

        if(currenttime > SwitchTime)
        {
            EffectObj.SetActive(!IsActive);
            currenttime = 0.0f;
        }
    }
}
