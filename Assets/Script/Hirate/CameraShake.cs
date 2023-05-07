using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    // �I�u�W�F�N�g�̐ݒ�
    [Header("��u���ݒ�")]
    [Tooltip("����")]
    [SerializeField] float time = 5.0f;
    [Tooltip("�U���̋���")]
    [SerializeField] float power = 0.3f;
    [Tooltip("�U����")]
    [SerializeField] int frequency = 5;
    [Tooltip("�����_��")]
    [SerializeField] int random = 10;
    [Tooltip("�X�i�b�v")]
    [SerializeField] bool snap = false;
    [Tooltip("�t�F�[�h�A�E�g")]
    [SerializeField] bool fadeOut = false;

    // ���������̔���p
    private bool bInput = false;
    // �����ʒu
    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu���
        initPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            bInput = true;
        }
        // �����ꂽ���Ɏ��s
        if (bInput)
        {
            gameObject.transform.DOShakePosition(
                time,
                power,
                frequency,
                random,
                snap,
                fadeOut
                ).OnComplete(() => { gameObject.transform.position = initPos; bInput = false; });
        }
    }
}
