using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //Panel�i���j���[�E�B���h�E�j�̕ϐ�
    public GameObject Object;
    bool UseFlag;

    public float Hp;
    public float Damage;

    public float MaxInvflame;
    float Invflame;

    // �ϐ�cubes�̒�`�FGameObject�̔z��
    GameObject[] cubes;

    // Start is called before the first frame update
    void Start()
    {
        //������Ԃł̓��j���[���\��
        Object.SetActive(false);
        //�t���O���\������
        UseFlag = false;

        // "enemy"�^�O���ݒ肳�ꂽ�Q�[���I�u�W�F�N�g�̔z����擾���A�ϐ�cubes�Ɋi�[����
        cubes = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (UseFlag == true)
        {
            //������Ԃł̓��j���[���\��
            Object.SetActive(true);
            Invflame++;
        }

        if(MaxInvflame < Invflame)
        {
            Object.SetActive(false);
            UseFlag = false;
            Invflame = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (UseFlag == false)
        {
            // �����Փ˂�������I�u�W�F�N�g�̃^�O��"Enemy"�Ȃ�Β��̏��������s
            if (collision.gameObject.CompareTag("Enemy"))
            {
                UseFlag = true;
                Hp -= Damage;
                if (Hp <= 0)
                {
                    //                this.Destroy();
                }
            }
        }
    }
}
