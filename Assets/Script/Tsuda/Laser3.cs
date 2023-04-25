using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser3 : MonoBehaviour
{
    public GameObject LaserA;

    public float sizeX = 0.0f;
    public float SizeY = 0.0f;
    public float decrease = 0.1f;
    [SerializeField] private float delay = 2.0f; // �Đ��܂ł̑ҋ@����

    private float timer = 0.0f; // �o�ߎ���
    private bool played = false; // �Đ��������ǂ����̃t���O
                                 //private bool stopped = false;

    private Vector3 currentScale;

    [SerializeField] private ParticleSystem particleSystem; // �Đ�����p�[�e�B�N���I�u�W�F�N�g

    void Start()
    {
        currentScale = transform.localScale;
        particleSystem.Stop();
    }

    private void Update()
    {
        // �Đ��ς݂ł���Ή������Ȃ�
        //        if (stopped) return;

        // �o�ߎ��Ԃ����Z
        timer += Time.deltaTime;

        if (!played)
        {
            currentScale.x = 0.0001f;
            currentScale.y = 0.0001f;
            // �ύX���scale��ݒ�
            transform.localScale = currentScale;
        }

        // �w�肵�����Ԃ��o�߂�����Đ�
        if (timer >= delay && !played)
        {
            particleSystem.Play();
            played = true;
        }

        if (played && currentScale.x <= sizeX && currentScale.y <= SizeY && timer <= delay + LaserA.GetComponent<LaserHead>().LaserTime)
        {
            currentScale.x += decrease;
            currentScale.y += decrease;
            // �ύX���scale��ݒ�
            transform.localScale = currentScale;
        }

        if (timer >= delay + LaserA.GetComponent<LaserHead>().LaserTime)
        {
            // scale������������
            currentScale.x -= decrease;
            currentScale.y -= decrease;
            // �ύX���scale��ݒ�
            transform.localScale = currentScale;
        }

        if (currentScale.x <= 0.0f && currentScale.y <= 0.0f)  // && !stopped)
        {
            Destroy(this.gameObject);
            //            particleSystem.Stop();
            //            stopped = true;
        }
    }
}


