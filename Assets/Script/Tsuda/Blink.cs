using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    // �_�ł�����Ώ�
    [SerializeField] private Renderer _target;
    // �_�Ŏ���[s]
    private float _cycle = 0.5f;

    private double _time;

    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f) { _cycle = 0.2f; }        


        // �����������o�߂�����
        _time += Time.deltaTime;

        // ����cycle�ŌJ��Ԃ��l�̎擾
        // 0�`cycle�͈̔͂̒l��������
        var repeatValue = Mathf.Repeat((float)_time, _cycle);

        // ��������time�ɂ����閾�ŏ�Ԃ𔽉f
        _target.enabled = repeatValue >= _cycle * 0.5f;
    }
}
