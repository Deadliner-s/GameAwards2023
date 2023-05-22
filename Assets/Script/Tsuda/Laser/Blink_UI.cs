using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink_UI : MonoBehaviour
{
    public GameObject Laser;
    // �_�ł�����Ώ�
    [SerializeField] private Image _target;

    // �_�Ŏ���[s]
    private float _cycle = 1.0f;
    private double _time;

    private float timer = 0.0f;
    private Camera mainCamera;  // ���C���J����

    void Start()
    {        
        mainCamera = Camera.main;  // ���C���J�������擾����
        GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(Laser.GetComponent<LaserHead>().targetWorldPosition);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(Laser.GetComponent<LaserHead>().targetWorldPosition);

        if (timer <= Laser.GetComponent<LaserHead>().wait)
        {
            if (timer >= Laser.GetComponent<LaserHead>().charge) { _cycle = 0.2f; }

            // �����������o�߂�����
            _time += Time.deltaTime;

            // ����cycle�ŌJ��Ԃ��l�̎擾
            // 0�`cycle�͈̔͂̒l��������
            var repeatValue = Mathf.Repeat((float)_time, _cycle);

            // ��������time�ɂ����閾�ŏ�Ԃ𔽉f
            _target.enabled = repeatValue >= _cycle * 0.5f;
        }
        else
        {
            _target.enabled = false;
        }
    }
}
