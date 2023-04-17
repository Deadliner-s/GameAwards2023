using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossContena : MonoBehaviour
{
    public float Speed;                  //�~�T�C���̑��x
    public float ToSpeed = 0.01f;         //�~�T�C���v���C���[�ɂ������x
    public float UpSpeed = 0.02f;         //�~�T�C����ɂ������x
    public float ContenaRange = 7.0f;    //�R���e�i���鋗��
    public float Height;                 //�~�T�C���̍���
    public int ContenaNumber = 15;       //����̐�

    float off;
    bool Locked;                         //�~�T�C�������b�N�I�����Ă��邩

    GameObject newObj;
    public GameObject otherObject;       //��������v���n�u�I�u�W�F�N�g

    GameObject Player;
    Vector3 FromPos;            //���ˌ�
    Vector3 ToPos;              //���ː�
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        if (Player = GameObject.Find("Player"))//�v���C���[�͐����Ă���i���݂���j
        {
            //�v���C���[�̈ʒu���擾
            ToPos = Player.transform.position;
            //�{�X�̈ʒu���擾
            FromPos = transform.position;

            off = 0.05f;
            Locked = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)//�v���C���[�͐����Ă���i���݂���j
        {
            ToPos = Player.transform.position;
            if (transform.position.y <= FromPos.y + Height && !Locked)
            {
                Speed = UpSpeed;
                Move = new Vector3(0, 1.0f, 0);
                LateMove = Move;
            }
            else
            {
                Locked = true;
                Speed = ToSpeed;
                float distance = Vector3.Distance(transform.position, ToPos);
                if (distance >= ContenaRange)
                {
                    Move = ToPos - transform.position;
                    Move = Move.normalized;
                    LateMove = (Move - LateMove) * off + (LateMove);
                }
                else
                {
                    for (int i = 0; i < ContenaNumber; i++)
                    {
                        float j = (i % 3) - 1;
                        float k = (i / 3) - 1;
                        newObj = Instantiate(otherObject, new Vector3(transform.position.x + j * 0.05f, transform.position.y + k * 0.1f, transform.position.z),Quaternion.identity);
                        if(i == ContenaNumber / 2)
                        {
                            //newObj.GetComponent<MissileBossContenaSmall>().Spread = 0.0f;
                            newObj.GetComponent<NewContena>().First = true;
                        }
                    }
                    Destroy(gameObject, 0);
                }
            }
            Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
            transform.rotation = rot;
            transform.position += LateMove * Speed;
        }
        else
        {
            Destroy(this, 0.0f);
        }
    }

}
