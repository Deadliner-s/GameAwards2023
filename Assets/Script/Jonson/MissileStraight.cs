using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileStraight : MonoBehaviour
{
    public float Speed = 0.01f;        //�~�T�C���̑��x
    public float MaxSpeed = 2.0f;      //���x����
    public float Accel = 0.001f;       //�����x
    public float MissRange = 2.0f;     //�v���C���[�ɊO���̋���
    private GameObject canvas;         // �L�����o�X
    float off;
    bool Miss;
    GameObject newObj;
    private Camera mainCamera;            // ���C���J����
    public GameObject otherObject;        // ��������v���n�u�I�u�W�F�N�g
    private Vector3 targetScreenPosition; // �ڕW�X�N���[�����W
    private Vector3 targetWorldPosition;  // �ڕW���[���h���W


    Vector3 ToPos;              //���ː�
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�

    // Start is called before the first frame update
    void Start()
    {      
        ToPos = GameObject.Find("Player").transform.position; //Player
        mainCamera = Camera.main;                             // ���C���J�������擾����
        canvas = GameObject.Find("Canvas");                 �@// �L�����o�X���w��


        //UI�����ʒu
        if (transform.position.x < ToPos.x)
        {
            targetScreenPosition.x = 1820 / -2;
        }
        else
        {
            targetScreenPosition.x = 0;
        }
        if (transform.position.y < ToPos.y)
        {
            targetScreenPosition.y = 980 / -2;
        }
        else if (transform.position.y > ToPos.y)
        {
            targetScreenPosition.y = 980 / 2;
        }
        else
        {
            targetScreenPosition.y = 0;
        }
        targetScreenPosition.z = 2.0f;

        //UI����
        newObj = Instantiate(otherObject, targetScreenPosition, transform.rotation) as GameObject;  // �x��UI�̐���
        Destroy(newObj, 3.0f);                                                                      // UI������

        newObj.transform.SetParent(canvas.transform, false);                                        // Canvas�̎q�I�u�W�F�N�g�Ƃ��Đ���


        Miss = false;
        off = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;   //�v���C���[�̈ʒu
        Speed += Accel;                                         //�����x
        if (Speed >= MaxSpeed)                                  //���x����
            Speed = MaxSpeed;
        float distance = Vector3.Distance(transform.position, ToPos);
        if (distance >= MissRange && !Miss)                     //�܂��O��ĂȂ�
        {
            Move = ToPos - transform.position;
            Move = Move.normalized;
            LateMove = (Move - LateMove) * off + (LateMove);
        }
        else
        {
            Miss = true;
        }

        //world���W��camera���W�ɕϊ�
        targetWorldPosition = transform.position;
        targetWorldPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);
        Vector3 NewPosFix = targetWorldPosition;

        //UI����ʊO�ɂ����Ȃ��悤��
        if (NewPosFix.y >= 1030)
        {
            NewPosFix.y = 1030;
        }
        if (NewPosFix.y <= 50)
        {
            NewPosFix.y = 50;
        }
        if (NewPosFix.x >= 1870)
        {
            NewPosFix.x = 1870;
        }
        if (NewPosFix.x <= 50)
        {
            NewPosFix.x = 50;
        }

        if(newObj)
        {
            newObj.transform.position = NewPosFix;  //UI�̈ʒu���X�V
        }   
        if(Miss)
        {
            Destroy(newObj);        //�~�T�C�����v���C���[�ɊO�ꂽ��UI������
        }
        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;
    }
}