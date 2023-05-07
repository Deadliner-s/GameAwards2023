using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    // �I�u�W�F�N�g�̐ݒ�
    [Header("��u���ݒ�")]
    [Tooltip("�����ԁ�\n�E��ʂ��h��鎞��")]
    [SerializeField] float time = 0.5f;
    [Tooltip("���U���̋�����\n�E�������W��")]
    [SerializeField] float power = 0.1f;
    [Tooltip("���U������\n�E�傫���ƌ������h��܂�\n�E�������Ƃ������h��܂�)")]
    [SerializeField] int frequency = 15;
    [Tooltip("�������_���ʁ�\n�E�����Ɨh�������������܂�\n�E���Ȃ��Ɨh�����������Ȃ��Ȃ�܂�")]
    [SerializeField] int random = 100;
    //[Tooltip("�X�i�b�v")]
    private bool snap = false;
    [Tooltip("���t�F�[�h�A�E�g��\n�E�h�ꂪ�t�F�[�h�A�E�g(���k)�����邩�ǂ���")]
    [SerializeField] bool fadeOut = false;

    // ���������̔���p
    private bool bInput = false;
    // �����ʒu
    private Vector3 initPos;
    // �h�炷�����̑���p
    private Tweener tweener;

    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu���
        initPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // �����ꂽ���Ɏ��s
        if (Input.GetKeyDown(KeyCode.O) && !bInput)
        {
            // ������x�����Ȃ��悤�ɂ���
            bInput = true;

            // ���O�̏������c���Ă���ꍇ�A�������W�ɖ߂�
            if (tweener != null)
            {
                tweener.Kill();
                gameObject.transform.position = initPos;
            }

            // ��ʂ�h�炷����
            tweener =
                gameObject.transform.DOShakePosition(
                time,      // ����
                power,     // �U���̋���
                frequency, // �U����
                random,    // �����_����
                snap,      // �X�i�b�v�̗L��
                fadeOut    // �t�F�[�h�A�E�g�̗L��
                ).OnComplete(() => bInput = false);
        }
    }
}
