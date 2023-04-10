using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHead : MonoBehaviour
{
    public static LaserHead instance;

    public float speed = 5f;  // �ړ����x
//    public GameObject otherObject;  // ��������v���n�u�I�u�W�F�N�g    
    public int Split;
    public float LaserTime = 4.0f;
    public float LaserSpeed = 3.0f;

    private int LaserMove;
    private float lifetime = 10.0f;  // �I�u�W�F�N�g�̎����i�b�j
    private float splitX;
    private float splitY;
    public Vector3 targetScreenPosition;  // �ڕW�X�N���[�����W
    public Vector3 targetWorldPosition;  // �ڕW���[���h���W
    private Camera mainCamera;  // ���C���J����
    private float timer;  // �^�C�}�[    
    GameObject newObj;    

    void Start()
    {
        LaserMove = Random.Range(0, 2);
    }

    void Update()
    {        
        timer += Time.deltaTime;  // �^�C�}�[�����Z����        

        if (timer >= 2.0f && timer <= 2.0f + LaserTime)
        {                                    
            targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // �ڕW�X�N���[�����W�����[���h���W�ɕϊ�����
            transform.LookAt(targetWorldPosition);
        }

        if (timer >= 3.0f && timer <= 2.0f + LaserTime)
        {            
            switch(Split)
            {            
                case 1: 
                    switch(LaserMove)
                    {
                        case 0: targetScreenPosition.x += LaserSpeed; break;
                        case 1: targetScreenPosition.x += LaserSpeed; break;
                        case 2: targetScreenPosition.y -= LaserSpeed; break;
                    }
                    break;

                case 2:
                    switch (LaserMove)
                    {
                        case 0: targetScreenPosition.x += LaserSpeed; break;
                        case 1: targetScreenPosition.x -= LaserSpeed; break;
                        case 2: targetScreenPosition.y -= LaserSpeed; break;
                    }
                    break;

                case 3:
                    switch (LaserMove)
                    {
                        case 0: targetScreenPosition.x -= LaserSpeed; break;
                        case 1: targetScreenPosition.x -= LaserSpeed; break;
                        case 2: targetScreenPosition.y -= LaserSpeed; break;
                    }
                    break;

                case 4:
                    switch (LaserMove)
                    {
                        case 0: targetScreenPosition.x += LaserSpeed; break;
                        case 1: targetScreenPosition.x += LaserSpeed; break;
                        case 2: targetScreenPosition.y += LaserSpeed; break;
                    }
                    break;

                case 5:
                    switch (LaserMove)
                    {
                        case 0: targetScreenPosition.x += LaserSpeed; break;
                        case 1: targetScreenPosition.x -= LaserSpeed; break;
                        case 2: targetScreenPosition.y += LaserSpeed; break;
                    }
                    break;

                case 6:
                    switch (LaserMove)
                    {
                        case 0: targetScreenPosition.x -= LaserSpeed; break;
                        case 1: targetScreenPosition.x -= LaserSpeed; break;
                        case 2: targetScreenPosition.y += LaserSpeed; break;
                    }
                    break;
            }                           
        }

        if (timer >= 2.0f + LaserTime)
        {
            transform.position += transform.forward * speed * Time.deltaTime;  // �ڕW���W�̕����Ɉړ�����
        }

        if (timer >= lifetime)
        {
            Destroy(gameObject);  // �I�u�W�F�N�g���폜����
        }
    }

    public void SetSplit(int num)
    {
        mainCamera = Camera.main;  // ���C���J�������擾����

        Split = num;

        switch (Split)
        {
            case 1: splitX = 1; splitY = 3; break;
            case 2: splitX = 3; splitY = 3; break;
            case 3: splitX = 5; splitY = 3; break;
            case 4: splitX = 1; splitY = 1; break;
            case 5: splitX = 3; splitY = 1; break;
            case 6: splitX = 5; splitY = 1; break;
        }

        targetScreenPosition.x = 320 * splitX;
        targetScreenPosition.y = 270 * splitY;
        targetScreenPosition.z = 1.0f;
        targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // �ڕW�X�N���[�����W�����[���h���W�ɕϊ�����        

        transform.LookAt(targetWorldPosition);  // �ڕW���W�̕���������

//        newObj = Instantiate(otherObject, targetWorldPosition, transform.rotation);  // �x��UI�̐���

        timer = 0.0f;  // �^�C�}�[��ݒ肷��
    }
}

