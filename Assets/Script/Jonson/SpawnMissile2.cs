using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissile2 : MonoBehaviour
{
    [Tooltip("ミサイルプレハブ")]
    public GameObject Missile;
    [Tooltip("消えるまでの時間")]
    public float DestroyTime;
    [Tooltip("デバッグ用ミサイル生成キー")]
    public string Key;

    // 進行時間用
    private float timer;

    // 発射間隔用
    private float IntervalTime;

    // リセットフラグ
    private bool Reset_flg = false;

    [Tooltip("生成場所指定")]
    public GameObject SpawnPos;

    // 現在フェイズ
    private PhaseManager.Phase currentPhase;

    // これコピペ
    [Header("ミサイル1回目")]
    [Tooltip("アタックフェイズ何秒目")]
    public float StartTime_1 = 100.0f;
    [Tooltip("何秒間")]
    public float Timer_1;
    [Tooltip("発射間隔")]
    public int Interval_1;   //出す時間
    [Tooltip("ミサイル配列")]
    public List<GameObject> h_Missile_1;
    // 確認用フラグ
    private bool Use_flg_1 = false;

    [Header("ミサイル2回目")]
    [Tooltip("アタックフェイズ何秒目")]
    public float StartTime_2 = 100.0f;
    [Tooltip("何秒間")]
    public float Timer_2;
    [Tooltip("発射間隔")]
    public float Interval_2;  //出す間隔
    [Tooltip("ミサイル配列")]
    public List<GameObject> h_Missile_2;
    // 確認用フラグ
    private bool Use_flg_2 = false;

    [Header("ミサイル3回目")]
    [Tooltip("アタックフェイズ何秒目")]
    public float StartTime_3 = 100.0f;
    [Tooltip("何秒間")]
    public float Timer_3;
    [Tooltip("発射間隔")]
    public float Interval_3;  //出す間隔
    [Tooltip("ミサイル配列")]
    public List<GameObject> h_Missile_3;
    // 確認用フラグ
    private bool Use_flg_3 = false;

    [Header("ミサイル4回目")]
    [Tooltip("アタックフェイズ何秒目")]
    public float StartTime_4 = 100.0f;
    [Tooltip("何秒間")]
    public float Timer_4;
    [Tooltip("発射間隔")]
    public float Interval_4;  //出す間隔
    [Tooltip("ミサイル配列")]
    public List<GameObject> h_Missile_4;
    // 確認用フラグ
    private bool Use_flg_4 = false;

    [Header("ミサイル5回目")]
    [Tooltip("アタックフェイズ何秒目")]
    public float StartTime_5 = 100.0f;
    [Tooltip("何秒間")]
    public float Timer_5;
    [Tooltip("発射間隔")]
    public float Interval_5;  //出す間隔
    [Tooltip("ミサイル配列")]
    public List<GameObject> h_Missile_5;
    // 確認用フラグ
    private bool Use_flg_5 = false;

    [Header("ミサイル6回目")]
    [Tooltip("アタックフェイズ何秒目")]
    public float StartTime_6 = 100.0f;
    [Tooltip("何秒間")]
    public float Timer_6;
    [Tooltip("発射間隔")]
    public float Interval_6;  //出す間隔
    [Tooltip("ミサイル配列")]
    public List<GameObject> h_Missile_6;
    // 確認用フラグ
    private bool Use_flg_6 = false;

    [Header("ミサイル7回目")]
    [Tooltip("アタックフェイズ何秒目")]
    public float StartTime_7 = 100.0f;
    [Tooltip("何秒間")]
    public float Timer_7;
    [Tooltip("発射間隔")]
    public float Interval_7;  //出す間隔
    [Tooltip("ミサイル配列")]
    public List<GameObject> h_Missile_7;
    // 確認用フラグ
    private bool Use_flg_7 = false;

    [Header("ミサイル8回目")]
    [Tooltip("アタックフェイズ何秒目")]
    public float StartTime_8 = 100.0f;
    [Tooltip("何秒間")]
    public float Timer_8;
    [Tooltip("発射間隔")]
    public float Interval_8;  //出す間隔
    [Tooltip("ミサイル配列")]
    public List<GameObject> h_Missile_8;
    // 確認用フラグ
    private bool Use_flg_8 = false;

    [Header("ミサイル9回目")]
    [Tooltip("アタックフェイズ何秒目")]
    public float StartTime_9 = 100.0f;
    [Tooltip("何秒間")]
    public float Timer_9;
    [Tooltip("発射間隔")]
    public float Interval_9;  //出す間隔
    [Tooltip("ミサイル配列")]
    public List<GameObject> h_Missile_9;
    // 確認用フラグ
    private bool Use_flg_9 = false;

    [Header("ミサイル10回目")]
    [Tooltip("アタックフェイズ何秒目")]
    public float StartTime_10 = 100.0f;
    [Tooltip("何秒間")]
    public float Timer_10;
    [Tooltip("発射間隔")]
    public float Interval_10;  //出す間隔
    [Tooltip("ミサイル配列")]
    public List<GameObject> h_Missile_10;
    // 確認用フラグ
    private bool Use_flg_10 = false;

    // Start is called before the first frame update
    void Start()
    {
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();

        // 時間の初期化
        timer = 0.0f;
        IntervalTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player"))//プレイヤーは生きている（存在する）
        {
            // フェイズ確認
            currentPhase = PhaseManager.instance.GetPhase();

            // デバッグ用
            if (Input.GetKeyDown(Key))
            {
                GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                Destroy(obj, DestroyTime);
            }

            if (currentPhase == PhaseManager.Phase.Speed_Phase)
            {// アタックフェイズ
                // 各フラグなどリセット
                if (Reset_flg == true)
                {
                    timer = 0.0f;

                    Use_flg_1 = false;
                    Use_flg_2 = false;
                    Use_flg_3 = false;
                    Use_flg_4 = false;
                    Use_flg_5 = false;
                    Use_flg_6 = false;
                    Use_flg_7 = false;
                    Use_flg_8 = false;
                    Use_flg_9 = false;
                    Use_flg_10 = false;

                    h_Missile_1 = new List<GameObject>();
                    h_Missile_2 = new List<GameObject>();
                    h_Missile_3 = new List<GameObject>();
                    h_Missile_4 = new List<GameObject>();
                    h_Missile_5 = new List<GameObject>();
                    h_Missile_6 = new List<GameObject>();
                    h_Missile_7 = new List<GameObject>();
                    h_Missile_8 = new List<GameObject>();
                    h_Missile_9 = new List<GameObject>();
                    h_Missile_10 = new List<GameObject>();


                    Reset_flg = false;
                }

                // 時間更新
                timer += Time.deltaTime;
                IntervalTime += Time.deltaTime;

                // インターバル


                // 1回目
                if (Use_flg_1 == false && timer >= StartTime_1)
                {
                    if (IntervalTime >= Interval_1)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_1.Add(obj);
                        //GameObject obj1 = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        //h_Missile_1.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // タイマーをリセットする
                    }
                    if (StartTime_1 + Timer_1 >= timer)
                    {
                        Use_flg_1 = true;
                    }
                }
                // 2回目
                if (Use_flg_2 == false && timer >= StartTime_2)
                {
                    if (IntervalTime >= Interval_2)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_2.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // タイマーをリセットする
                    }
                    if (StartTime_2 + Timer_2 >= timer)
                    {
                        Use_flg_2 = true;
                    }
                }
                // 3回目
                if (Use_flg_3 == false && timer >= StartTime_3)
                {
                    if (IntervalTime >= Interval_3)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_3.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // タイマーをリセットする
                    }
                    if (StartTime_3 + Timer_3 >= timer)
                    {
                        Use_flg_3 = true;
                    }
                }
                // 4回目
                if (Use_flg_4 == false && timer >= StartTime_4)
                {
                    if (IntervalTime >= Interval_4)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_4.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // タイマーをリセットする
                    }
                    if (StartTime_4 + Timer_4 >= timer)
                    {
                        Use_flg_4 = true;
                    }
                }
                // 5回目
                if (Use_flg_5 == false && timer >= StartTime_5)
                {
                    if (IntervalTime >= Interval_5)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_5.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // タイマーをリセットする
                    }
                    if (StartTime_5 + Timer_5 >= timer)
                    {
                        Use_flg_5 = true;
                    }
                }
                // 6回目
                if (Use_flg_6 == false && timer >= StartTime_6)
                {
                    if (IntervalTime >= Interval_6)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_6.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // タイマーをリセットする
                    }
                    if (StartTime_6 + Timer_6 >= timer)
                    {
                        Use_flg_6 = true;
                    }
                }
                // 7回目
                if (Use_flg_7 == false && timer >= StartTime_7)
                {
                    if (IntervalTime >= Interval_7)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        h_Missile_7.Add(obj);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // タイマーをリセットする
                    }
                    if (StartTime_7 + Timer_7 >= timer)
                    {
                        Use_flg_7 = true;
                    }
                }
                // 8回目
                if (Use_flg_8 == false && timer >= StartTime_8)
                {
                    if (IntervalTime >= Interval_8)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        Destroy(obj, DestroyTime);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // タイマーをリセットする
                    }
                    if (StartTime_8 + Timer_8 >= timer)
                    {
                        Use_flg_8 = true;
                    }
                }
                // 9回目
                if (Use_flg_9 == false && timer >= StartTime_9)
                {
                    if (IntervalTime >= Interval_9)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        Destroy(obj, DestroyTime);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // タイマーをリセットする
                    }
                    if (StartTime_9 + Timer_9 >= timer)
                    {
                        Use_flg_9 = true;
                    }
                }
                // 10回目
                if (Use_flg_10 == false && timer >= StartTime_10)
                {
                    if (IntervalTime >= Interval_10)
                    {
                        GameObject obj = Instantiate(Missile, new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y, SpawnPos.transform.position.z), Quaternion.identity);
                        Destroy(obj, DestroyTime);
                        Destroy(obj, DestroyTime);

                        IntervalTime = 0.0f; // タイマーをリセットする
                    }
                    if (StartTime_10 + Timer_10 >= timer)
                    {
                        Use_flg_10 = true;
                    }
                }

            }
            else
            {// ハイスピードフェイズ
                Reset_flg = true;
            }
        }
    }
}