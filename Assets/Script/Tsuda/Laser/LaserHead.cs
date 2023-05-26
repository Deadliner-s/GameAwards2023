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
    private GameObject playerManager;

    private float lifetime;  // オブジェクトの寿命（秒）    
    private Vector3 PlayerPosition;
    public Vector3 targetScreenPosition;  // 目標スクリーン座標
    public Vector3 targetWorldPosition;  // 目標ワールド座標
    private Camera mainCamera;  // メインカメラ
    private float timer;  // タイマー        
    public float wait;

    //private PlayerHp playerHp;

    void Start()
    {
        wait = charge + charge2;

        lifetime = wait + LaserTime + 5.0f;

        mainCamera = Camera.main;  // メインカメラを取得する

        Player = GameObject.Find("Player");
        playerManager = GameObject.Find("PlayerManager");

        //playerHp = Player.GetComponent<PlayerHp>();
        PlayerPosition = Player.transform.position;
        targetWorldPosition = Player.transform.position;                

        SoundManager.instance.PlaySE("Laser_charge");
        SoundManager.instance.PlaySE("Laser_UI");
    }

    void Update()
    {        
        timer += Time.deltaTime;  // タイマーを減算する                                                          
        if (playerManager != null)
        {
            if (timer >= lifetime || playerManager.GetComponent<PlayerHp>().BreakFlag)
            {
                Destroy(gameObject);  // オブジェクトを削除する
            }
        }
        if (timer <= charge)
        {
            PlayerPosition = Player.transform.position;
            targetWorldPosition = Player.transform.position;
        }        

        if (timer >= wait)
        {
            if (mainCamera != null)
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
            }
            transform.LookAt(targetWorldPosition);
        }        
        


        if (timer >= wait && !IdkFlg)
        {
            GameObject obj = Instantiate(Idk, targetWorldPosition, transform.rotation);
            // MissileObjをタグ検索
            GameObject missileObj = GameObject.FindGameObjectWithTag("MissileObj");
            // ミサイルオブジェクトの子にする
            obj.transform.parent = missileObj.transform;
            IdkFlg = true;
        }



        //transform.LookAt(targetWorldPosition);

        /*
        if (timer >= 2.0f && timer <= 2.0f + LaserTime)
        {            
            targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // 目標スクリーン座標をワールド座標に変換する            
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
    }

    public void SetLaserTime(float time)
    {
        LaserTime = time;

        /*
        mainCamera = Camera.main;  // メインカメラを取得する

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
        targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // 目標スクリーン座標をワールド座標に変換する        

        transform.LookAt(targetWorldPosition);  // 目標座標の方向を向く

//        newObj = Instantiate(otherObject, targetWorldPosition, transform.rotation);  // 警告UIの生成

        */        
    }    
}

