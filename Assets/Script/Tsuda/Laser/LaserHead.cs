using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHead : MonoBehaviour
{
    public static LaserHead instance;
        
    public float LaserTime = 4.0f;
    public float LaserSpeed = 3.0f;
    public float charge = 4.0f;
    public float charge2 = 1.0f;

    public GameObject Idk;
    private bool IdkFlg = false;

    private GameObject Player;    
    private float lifetime;  // �I�u�W�F�N�g�̎����i�b�j    
    private Vector3 PlayerPosition;
    public Vector3 targetScreenPosition;  // �ڕW�X�N���[�����W
    public Vector3 targetWorldPosition;  // �ڕW���[���h���W
    private Camera mainCamera;  // ���C���J����
    private float timer;  // �^�C�}�[        
    public float wait;

    void Start()
    {
        wait = charge + charge2;

        lifetime = wait + LaserTime + 5.0f;

        mainCamera = Camera.main;  // ���C���J�������擾����

        Player = GameObject.Find("Player");
        PlayerPosition = Player.transform.position;
        targetWorldPosition = Player.transform.position;                

        SoundManager.instance.PlaySE("Laser_charge");
        SoundManager.instance.PlaySE("Laser_UI");
    }

    void Update()
    {        
        timer += Time.deltaTime;  // �^�C�}�[�����Z����                                                          

        if (timer <= charge)
        {
            PlayerPosition = Player.transform.position;
            targetWorldPosition = Player.transform.position;
        }        

        if (timer >= wait)
        {            
            targetScreenPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);

            if (timer >= wait + 1.0f)
            {
                if (PlayerPosition.x <= 0.0f)
                {
                    targetScreenPosition.x += LaserSpeed;
                }
                if (PlayerPosition.x >= 0.0f)
                {
                    targetScreenPosition.x -= LaserSpeed;
                }
            }

            targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);

            transform.LookAt(targetWorldPosition);
            

            /*
            if (PlayerPosition.x <= 0.0f)
            {
                targetScreenPosition.x += LaserSpeed;
            }
            if (PlayerPosition.x >= 0.0f)
            {
                targetScreenPosition.x -= LaserSpeed;
            }

            
            if (timer >= wait && timer <= wait + LaserTime)
            {
                if (PlayerPosition.x <= 0.0f)
                {
                    targetScreenPosition.x += LaserSpeed;
                }
                if (PlayerPosition.x >= 0.0f)
                {
                    targetScreenPosition.x -= LaserSpeed;
                }
            }
            */
        }
        
        


        if (timer >= wait && !IdkFlg)
        {
            Instantiate(Idk, targetWorldPosition, transform.rotation);
            IdkFlg = true;
        }



        //transform.LookAt(targetWorldPosition);

        /*
        if (timer >= 2.0f && timer <= 2.0f + LaserTime)
        {            
            targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // �ڕW�X�N���[�����W�����[���h���W�ɕϊ�����            
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
        */

        if (timer >= lifetime)
        {
            Destroy(gameObject);  // �I�u�W�F�N�g���폜����
        }
    }

    public void SetLaserTime(float time)
    {
        LaserTime = time;

        /*
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

        */        
    }    
}

