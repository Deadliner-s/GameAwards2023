using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBoss2 : MonoBehaviour
{
    public float Speed;         //�~�T�C���̑��x
    public float MaxSpeed = 2.0f;      
    public float Accel;         //�����x
    public float MissRange;     //�v���C���[�ɊO���̋���
    float range = 10.0f;
    float off;
    bool Locked;                //�~�T�C�������b�N�I�����Ă��邩

    System.Random rand = new System.Random();
    float randomY;

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

        randomY = (rand.Next(10) - 5);
        off = 0.2f;
        Locked = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;
        if (!Locked)
        {
            if (transform.position.x >= FromPos.x - range)
                Move = new Vector3(-range,randomY, 0.0f);
            else
                Locked = true;
            Move = Move.normalized;
            LateMove = Move;
        }
        else 
        {
            Speed += Accel;
            if (Speed >= MaxSpeed)
                Speed = MaxSpeed;
            if (transform.position.x <= ToPos.x - MissRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, ToPos.z);
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