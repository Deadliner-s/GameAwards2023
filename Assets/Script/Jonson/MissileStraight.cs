using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MissileStraight : MonoBehaviour
{
    public float Speed = 0.0f;        //�~�T�C���̑��x
    public float MaxSpeed = 0.003f;      //���x����
    //public float Accel = 0.001f;       //�����x
    public float MissRange = 2.0f;     //�v���C���[�ɊO���̋���
    public float UIFillSpeed = 0.005f;  //UI�̑���
    private GameObject canvas;         // �L�����o�X
    float off;
    bool Miss;
    GameObject newObj;
    GameObject OutsideObj;
    private Camera mainCamera;            // ���C���J����
    public GameObject otherObject;        // ��������v���n�u�I�u�W�F�N�g
    public GameObject outsideObject;
    private Vector3 targetScreenPosition; // �ڕW�X�N���[�����W
    private Vector3 targetWorldPosition;  // �ڕW���[���h���W

    int time;

    GameObject Player; 
    Vector3 ToPos;              //���ː�
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        if (Player = GameObject.Find("Player"))//�v���C���[�͐����Ă���i���݂���j
        {
            ToPos = Player.transform.position; //Player
            mainCamera = Camera.main;                             // ���C���J�������擾����
            canvas = GameObject.Find("Canvas");                  // �L�����o�X���w��

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
            newObj.transform.SetParent(canvas.transform, false);                                        // Canvas�̎q�I�u�W�F�N�g�Ƃ��Đ���
            OutsideObj = Instantiate(outsideObject, targetScreenPosition, transform.rotation) as GameObject;  // �x��UI�̐���                                                           
            OutsideObj.transform.SetParent(canvas.transform, false);                                        // Canvas�̎q�I�u�W�F�N�g�Ƃ��Đ���
            newObj.GetComponent<Image>().fillAmount = 0;
            time = 0;
            Miss = false;
            off = 0.2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)//�v���C���[�͐����Ă���i���݂���j
        {
            time++;
            //world���W��camera���W�ɕϊ�
            targetWorldPosition = transform.position;
            targetWorldPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);
            Vector3 NewPosFix = targetWorldPosition;

            if (time >= 120)
            {
                Miss = true;            //��ʓ��ɓ�����
                Speed = MaxSpeed;       //���x��MAX
                Destroy(newObj);        //UI������
                Destroy(OutsideObj);
            }
            else if (!Miss)              //��ʓ��ɂ܂������ĂȂ��A�ǔ�
            {
                ToPos =Player.transform.position;   //�v���C���[�̈ʒu
                Move = ToPos - transform.position;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);
            }

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

            if (newObj)
            {
                newObj.transform.position = NewPosFix;  //UI�̈ʒu���X�V
                OutsideObj.transform.position = NewPosFix;  //UI�̈ʒu���X�V
                newObj.GetComponent<Image>().fillAmount +=�@UIFillSpeed;
                if (newObj.GetComponent<Image>().fillAmount >= 1.0f)
                {
                    newObj.GetComponent<Image>().fillAmount = 1.0f;
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