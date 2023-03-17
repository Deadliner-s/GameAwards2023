using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBossDirectionRandom : MonoBehaviour
{
    public float Speed;         //�~�T�C���̑��x
    public float MaxSpeed = 2.0f;
    public float Accel;         //�����x
    public float MissRange;     //�v���C���[�ɊO���̋���

    float DirectionX;           // -1,0
    float DirectionY;           // -1,0,1
    float range = 10.0f;
    float off;
    bool Locked;                //�~�T�C�������b�N�I�����Ă��邩
    bool Miss;

    System.Random rand = new System.Random();

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

        off = 0.2f;
        Locked = false;
        Miss = false;

        switch (rand.Next(5))                   //�~�T�C�������������_���ȕ�����
        {
            case 0:
                DirectionX = 0;
                DirectionY = 1;
                break;
            case 1:
                DirectionX = -1;
                DirectionY = 1;
                break;
            case 2:
                DirectionX = -1;
                DirectionY = 0;
                break;
            case 3:
                DirectionX = -1;
                DirectionY = -1;
                break;
            case 4:
                DirectionX = 0;
                DirectionY = -1;
                break;
            default:
                break;
        }

        if (DirectionX == 0 && DirectionY == 0)  //�o�O���h�~�@(0,0�Ƃ���ƃ~�T�C���͓����Ȃ�)
        {
            DirectionX = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;
        if (!Locked)
        {
            if (transform.position.x >= FromPos.x - range && transform.position.y >= FromPos.y - range && transform.position.y <= FromPos.y + range)
            {
                Move = new Vector3(DirectionX, DirectionY, 0.0f);
                Move *= range;
            }
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
            if (transform.position.x <= ToPos.x - MissRange || transform.position.y <= ToPos.y - MissRange || transform.position.y >= ToPos.y + MissRange)
            {
                if (!Miss)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, ToPos.z);
                    Move = ToPos - transform.position;
                    Move = Move.normalized;
                    LateMove = (Move - LateMove) * off + (LateMove);
                }
            }
            else
                Miss = true;
        }

        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;
    }
}