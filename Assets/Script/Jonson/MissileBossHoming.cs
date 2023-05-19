using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossHoming : MonoBehaviour
{
    public float Speed;         //�~�T�C���̑��x
    public float MaxSpeed = 0.1f;
    public float Accel;         //�����x
    public float MissRange;     //�v���C���[�ɊO���̋���
    public float Height;        //�~�T�C���̍���
    float HeightHalf;
    float off;
    bool Locked;                //�~�T�C�������b�N�I�����Ă��邩
    bool Miss;
    float randomX;
    System.Random rand = new System.Random();
    Vector3 FromPos;            //���ˌ�
    Vector3 ToPos;              //���ː�
    GameObject Player;
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        if (Player = GameObject.Find("Player"))
        {
            //EnemyBoss�Ƃ������O�̂��̂����݂���
            FromPos = transform.position;
            //Player�Ƃ������O�̂��̂����݂���
            ToPos = Player.transform.position;
            //�~�T�C���̏����ʒu��ݒ�
            transform.position = FromPos;

            randomX = (rand.Next(2) + 1) * 0.05f;
            HeightHalf = Height / 2.0f;
            if (transform.position.x < 0.0f)
            {
                randomX *= -1.0f;
            }
            off = 0.04f;
            Locked = false;
            Miss = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Player)
        {
            ToPos = Player.transform.position;
            if (transform.position.y <= FromPos.y + HeightHalf && !Locked)
            {
                Move = new Vector3(randomX, 1.0f, -1.0f);
                LateMove = (Move - LateMove) * off + (LateMove);
            }
            else if (transform.position.y <= FromPos.y + Height && !Locked)
            {
                Move = new Vector3(randomX * 10.0f, 1.0f, -1.0f);
                LateMove = (Move - LateMove) * off + (LateMove);                
            }
            else
            {
                off = 0.12f;
                Locked = true;
                Speed += Accel;
                if (Speed >= MaxSpeed)
                {
                    Speed = MaxSpeed;
                }
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
            }
            Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
            transform.rotation = rot;
            transform.position += LateMove * Speed;

            if(transform.position.z <= -12.0f)  //��ʊO��(�J�����̌��)�o���Ƃ�
            {
                Destroy(this, 0.0f);
            }
        }
        else
        {
            Destroy(this, 0.0f);
        }
    }
}