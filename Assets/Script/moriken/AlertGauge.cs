using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertGauge : MonoBehaviour
{
    
    Image AlertObject;
    [Tooltip("������Q�[�W�̃I�u�W�F�N�g")]
    public GameObject AlertGaugeObject;
    [Tooltip("1�t���[���ł̃Q�[�W�̐i�݋(MAX 1)")]
    public float flamecount;

    // Start is called before the first frame update
    void Start()
    {
        AlertObject = AlertGaugeObject.GetComponent<Image>();
        AlertObject.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[�W�����X�ɕ\�������Ă�������
        AlertObject.fillAmount += flamecount;
    }
}
