using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WARNING : MonoBehaviour
{    
    // �_�ł�����Ώ�
    [SerializeField] private Image _target;

    // �_�Ŏ���[s]
    private float _cycle = 0.7f;
    private double _time;    

    void Start()
    {
        
    }

    private void Update()
    {               
        // �����������o�߂�����
        _time += Time.deltaTime;

         // ����cycle�ŌJ��Ԃ��l�̎擾
         // 0�`cycle�͈̔͂̒l��������
        var repeatValue = Mathf.Repeat((float)_time, _cycle);

         // ��������time�ɂ����閾�ŏ�Ԃ𔽉f
        _target.enabled = repeatValue >= _cycle * 0.5f;        
    }
}
