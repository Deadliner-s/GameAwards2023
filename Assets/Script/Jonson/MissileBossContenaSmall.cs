using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossContenaSmall : MonoBehaviour
{
    public float Speed = 0.02f;        //�~�T�C���̑��x
    public float MaxSpeed = 0.1f;      //���x����
    public float Accel = 0.001f;       //�����x
    public float Spread = 0.02f;       //�g�U
    //float off;
    bool Miss;

    System.Random rand = new System.Random();
    float randX;
    float randY;

    GameObject Player;
    Vector3 ToPos;              //���ː�
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�
    // Start is called before the first frame update
    void Start()
    {
        if (Player = GameObject.Find("Player"))
        {
            ToPos = Player.transform.position; //Player
            randX = rand.Next(16) - 8;
            randY = rand.Next(16) - 8;
            Miss = false;
            //off = 0.9f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            Speed += Accel;                                         //�����x
            if (Speed >= MaxSpeed)                                  //���x����
                Speed = MaxSpeed;
            //if (distance >= MissRange && !Miss)
            if (!Miss)
            {
                Move = ToPos - transform.position;
                Move = Move.normalized;
                Move.x += randX * Spread;
                Move.y += randY * Spread;
                //LateMove = (Move - LateMove) * off + (LateMove);
                LateMove = Move;
                Miss = true;
            }
            else
            {
                Destroy(gameObject, 3.0f);
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
