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
                        targetScreenPosition.x += LaserSpeed * Time.timeScale;
                    }
                    if (PlayerPosition.x >= 0.0f)
                    {
                        targetScreenPosition.x -= LaserSpeed * Time.timeScale;
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
    }

    public void SetLaserTime(float time)
    {
        LaserTime = time;        
    }    
}

