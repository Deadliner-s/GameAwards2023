using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBoss : MonoBehaviour
{
    public float Speed;         //�~�T�C���̑��x
    public float MaxSpeed = 2.0f;
    public float Accel;         //�����x
    public float MissRange;     //�v���C���[�ɊO���̋���
    public float Height;        //�~�T�C���̍���
    float off;
    bool Locked;                //�~�T�C�������b�N�I�����Ă��邩

    System.Random rand = new System.Random();
    float randomX;

    Vector3 FromPos;            //���ˌ�
    Vector3 ToPos;              //���ː�
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        //EnemyBoss�Ƃ������O�̂��̂����݂���
        FromPos = GameObject.Find("EnemyBoss").transform.position;
        //Player�Ƃ������O�̂��̂����݂���
        ToPos = GameObject.Find("Player").transform.position;
        //�~�T�C���̏����ʒu��ݒ�
        transform.position = FromPos;

        randomX = (rand.Next(10) - 5) * 0.1f;
        off = 0.2f;
        Locked = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;
        if (transform.position.y <= FromPos.y + Height && !Locked)
        {
            Move = new Vector3(randomX, 1.0f, -0.1f);
            LateMove = Move;
        }
        else
        {
            Speed += Accel;
            if (Speed >= MaxSpeed)
                Speed = MaxSpeed;
            Locked = true;
            if (transform.position.z >= ToPos.z + MissRange || transform.position.x >= ToPos.x + MissRange)
            {
                Move = ToPos - transform.position;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);
            }
        }
        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;
    }
}