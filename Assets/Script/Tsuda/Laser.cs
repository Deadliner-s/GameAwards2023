using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float delay = 1.0f; // �Đ��܂ł̑ҋ@����
    private float timer = 0.0f; // �o�ߎ���
    private bool played = false; // �Đ��������ǂ����̃t���O
//    private CapsuleCollider Col;

    [SerializeField] private ParticleSystem particleSystem; // �Đ�����p�[�e�B�N���I�u�W�F�N�g

    void Start()
    {
//        Col = GetComponent<CapsuleCollider>();
//        Col.enabled = false;
        particleSystem.Stop();
    }

    private void Update()
    {
        // �Đ��ς݂ł���Ή������Ȃ�
        if (played) return;

        // �o�ߎ��Ԃ����Z
        timer += Time.deltaTime;

        // �w�肵�����Ԃ��o�߂�����Đ�
        if (timer >= delay)
        {
//            Col.enabled = true;
            particleSystem.Play();
            played = true;
        }
    }
}


