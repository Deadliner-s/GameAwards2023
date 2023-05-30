using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MissileStraight : MonoBehaviour
{
    public float Speed = 0.0f;        //�~�T�C���̑��x
    public float MaxSpeed = 0.003f;      //���x����
    public float MissRange = 2.0f;     //�v���C���[�ɊO���̋���
    float UIFillSpeed = 0.01f;  //UI�̑���
    private GameObject canvas;         // �L�����o�X
    float off;
    bool Miss;
    public bool isDestroyed = false;
    GameObject newObj;
    GameObject OutsideObj;
    GameObject LightObj;
    private Camera mainCamera;            // ���C���J����
    public GameObject otherObject;        // ��������v���n�u�I�u�W�F�N�g
    public GameObject outsideObject;
    public GameObject LightObject;
    private Vector3 targetScreenPosition; // �ڕW�X�N���[�����W
    private Vector3 targetWorldPosition;  // �ڕW���[���h���W
    Vector3 NewPosFix;
    GameObject BossFlg;
    float time = 0;
    bool instant = false;

    GameObject Player; 
    Vector3 ToPos;              //���ː�
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        BossFlg = GameObject.Find("BossManager");
        if (!BossFlg.GetComponent<MainBossHp>().BreakFlag)
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
                if (!instant)
                {
                    //world���W��camera���W�ɕϊ�
                    targetWorldPosition = transform.position;
                    targetWorldPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);
                    NewPosFix = targetWorldPosition;
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
                    if (GameObject.Find("Canvas"))
                    {
                        LightObj = Instantiate(LightObject, targetScreenPosition, transform.rotation) as GameObject;
                        LightObj.transform.SetParent(canvas.transform, false);
                        LightObj.GetComponent<Image>().fillAmount = 0;
                        newObj = Instantiate(otherObject, targetScreenPosition, transform.rotation) as GameObject;  // �x��UI�̐���                                                           
                        newObj.transform.SetParent(canvas.transform, false);                                        // Canvas�̎q�I�u�W�F�N�g�Ƃ��Đ���
                        newObj.GetComponent<Image>().fillAmount = 0;
                        OutsideObj = Instantiate(outsideObject, targetScreenPosition, transform.rotation) as GameObject;  // �x��UI�̐���                                                           
                        OutsideObj.transform.SetParent(canvas.transform, false);                                        // Canvas�̎q�I�u�W�F�N�g�Ƃ��Đ���

                        LightObj.transform.position = NewPosFix;    //UI�̈ʒu���X�V
                        newObj.transform.position = NewPosFix;      //UI�̈ʒu���X�V
                        OutsideObj.transform.position = NewPosFix;  //UI�̈ʒu���X�V
                    }
                }
                time = 0.0f;
                Miss = false;
                off = 0.2f;
            }
            else
            {
                Destroy(this, 0.0f);
            }
        }            
    }

    // Update is called once per frame
    void Update()
    {
        if (Player&&!BossFlg.GetComponent<MainBossHp>().BreakFlag)//�v���C���[�͐����Ă���i���݂���j
        {
            time += Time.timeScale;
            if (time >= 120.0f)
                instant = true;
            if(time >= 135.0f)
            {
                if (newObj)
                    Destroy(newObj);        //UI������
                if (outsideObject)
                    Destroy(OutsideObj);
                if (LightObj)
                    Destroy(LightObj);
            }
            if (!Miss)              //��ʓ��ɂ܂������ĂȂ��A�ǔ�
            {
                ToPos =Player.transform.position;   //�v���C���[�̈ʒu
                Move = ToPos - transform.position;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);
            }
            if (instant)
            {
                Miss = true;            //��ʓ��ɓ�����
                Speed = MaxSpeed;       //���x��MAX
            }

            targetWorldPosition = transform.position;
            targetWorldPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);

            if (targetWorldPosition.y > 1500 || targetWorldPosition.y < -200 || targetWorldPosition.x > 2100) 
            {
                isDestroyed = true;
            }

            if (isDestroyed)
            {
                Destroy(newObj);        //UI������
                Destroy(OutsideObj);
                Destroy(LightObj);
                Destroy(gameObject);
            }

            if (newObj)
            {
                //newObj.transform.position = NewPosFix;  //UI�̈ʒu���X�V
                //OutsideObj.transform.position = NewPosFix;  //UI�̈ʒu���X�V
                newObj.GetComponent<Image>().fillAmount +=�@UIFillSpeed * Time.timeScale;
                if (newObj.GetComponent<Image>().fillAmount >= 0.9f)
                {
                    LightObj.GetComponent<Image>().fillAmount = 1.0f;
                    Destroy(newObj);
                }

            }
            Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
            transform.rotation = rot;
            transform.position += LateMove * Speed * Time.timeScale;
        }
        else
        {
            if(newObj)
                Destroy(newObj);        //UI������
            if(outsideObject)
                Destroy(OutsideObj);
            if (LightObj)
                Destroy(LightObj);
                Destroy(gameObject);
        }
    }
}