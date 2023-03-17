using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileStraight : MonoBehaviour
{
    public float Speed;         //�~�T�C���̑��x
    public float MaxSpeed = 2.0f;
    public float Accel;         //�����x
    public float MissRange = 2.0f;     //�v���C���[�ɊO���̋���
    float off;
    bool Miss;
    public string FromName;

    //Vector3 FromPos;            //���ˌ�
    Vector3 ToPos;              //���ː�
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        //EnemyBoss�Ƃ������O�̂��̂����݂���
        //FromPos = GameObject.Find(FromName).transform.position;
        //Player�Ƃ������O�̂��̂����݂���
        ToPos = GameObject.Find("Player").transform.position;
        //�~�T�C���̏����ʒu��ݒ�
        //transform.position = FromPos;
        Miss = false;
        off = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;
        Speed += Accel;
        if (Speed >= MaxSpeed)
            Speed = MaxSpeed;
        float distance = Vector3.Distance(transform.position, ToPos);
        if (distance >= MissRange && !Miss)
        {
            Move = ToPos - transform.position;
            Move = Move.normalized;
            LateMove = (Move - LateMove) * off + (LateMove);
        }
        else
        {
            Miss = true;
        }

        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;
    }
}