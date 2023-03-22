using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public GameObject prefab; // �v���n�u�I�u�W�F�N�g
    [Header("�t�F�C�Y�m�F�p�I�u�W�F�N�g")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;
    public float Interval = 2.0f;
    public float Interval2 = 8.0f;

    private float timer = 0.0f;
    private float timer2 = 0.0f;
    private bool wait = false;

//    private GameObject targetObject; // �ΏۃI�u�W�F�N�g

    void Start()
    {        
        AtkPhaseFlg = PhaseObj.activeSelf;
    }

    void Update()
    {
        AtkPhaseFlg = PhaseObj.activeSelf;

        // �ΏۃI�u�W�F�N�g�����݂��Ȃ��ꍇ�́A���������s���Ȃ�
        if (AtkPhaseFlg == false)
        {
            return;
        }
        else
        {

            if (!wait)
            {
                timer += Time.deltaTime;
                timer2 += Time.deltaTime;

                if (timer >= Interval)
                {
                    Instantiate(prefab, transform.position, transform.rotation); // �v���n�u�I�u�W�F�N�g�𐶐�����
                    timer = 0.0f; // �^�C�}�[�����Z�b�g����
                }
                if (timer2 >= Interval2)
                {
                    wait = true;
                    timer2 = 0.0f;
                }
            }
            if (wait)
            {
                timer2 += Time.deltaTime;

                if (timer2 >= Interval2)
                {
                    wait = false;
                    timer2 = 0.0f;
                }
            }
        }
    }
}
