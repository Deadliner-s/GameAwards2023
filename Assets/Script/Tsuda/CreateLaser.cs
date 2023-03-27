using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public static CreateLaser instance;

    [Tooltip("ミサイルプレハブ")]
    public GameObject prefab; // プレハブオブジェクト    

    // 進行時間用
    public float timer;

    // リセットフラグ
    private bool Reset_flg = false;

    // 現在フェイズ
    private PhaseManager.Phase currentPhase;

    // 現在狙っているSplit
    public int TargetSplit;

    [Header("レーザー1回目")]
    [Tooltip("何秒目開始")]
    public float StartTime_1 = 100.0f;
    [Tooltip("何番に撃つか")]
    public int Split_1;
    // 確認用フラグ
    private bool Use_flg_1 = false;

    [Header("レーザー2回目")]
    [Tooltip("何秒目開始")]
    public float StartTime_2 = 100.0f;
    [Tooltip("何番に")]
    public int Split_2;
    // 確認用フラグ
    private bool Use_flg_2 = false;

    [Header("レーザー3回目")]
    [Tooltip("何秒目開始")]
    public float StartTime_3 = 100.0f;
    [Tooltip("何番に")]
    public int Split_3;
    // 確認用フラグ
    private bool Use_flg_3 = false;

    [Header("レーザー4回目")]
    [Tooltip("何秒目開始")]
    public float StartTime_4 = 100.0f;
    [Tooltip("何番に")]
    public int Split_4;
    // 確認用フラグ
    private bool Use_flg_4 = false;

    [Header("レーザー5回目")]
    [Tooltip("何秒目開始")]
    public float StartTime_5 = 100.0f;
    [Tooltip("何番に")]
    public int Split_5;
    // 確認用フラグ
    private bool Use_flg_5 = false;

    void Start()
    {
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();

        // 時間の初期化
        timer = 0.0f;
    }

    void Update()
    {
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();

        // デバッグ用
        if (Input.GetKeyDown(KeyCode.C)) // Cキーが押されたら
        {
            Instantiate(prefab, transform.position, transform.rotation); // プレハブオブジェクトを生成する
        }


        if (currentPhase == PhaseManager.Phase.Speed_Phase)
        {// ハイスピードフェイズ
            // フラグなどをリセット
            if (Reset_flg == true)
            {
                timer = 0.0f;

                Use_flg_1 = false;
                Use_flg_2 = false;
                Use_flg_3 = false;
                Use_flg_4 = false;
                Use_flg_5 = false;

                Reset_flg = false;
            }

            // 時間更新
            timer += Time.deltaTime;

            // 1回目
            if (Use_flg_1 == false && timer >= StartTime_1)
            {
                TargetSplit = 1;
                // レーザー作成
                GameObject obj = Instantiate(prefab, transform.position, transform.rotation); // プレハブオブジェクトを生成する
                obj.GetComponent<LaserHead>().SetSplit(TargetSplit);
                // フラグ更新
                Use_flg_1 = true;
            }
            // 2回目
            if (Use_flg_2 == false && timer >= StartTime_2)
            {
                TargetSplit = 2;
                // レーザー作成
                GameObject obj1 = Instantiate(prefab, transform.position, transform.rotation); // プレハブオブジェクトを生成する
                obj1.GetComponent<LaserHead>().SetSplit(TargetSplit);
                // フラグ更新
                Use_flg_2 = true;
            }
            // 3回目
            if (Use_flg_3 == false && timer >= StartTime_3)
            {
                TargetSplit = 3;
                // レーザー作成
                GameObject obj2 = Instantiate(prefab, transform.position, transform.rotation); // プレハブオブジェクトを生成する
                obj2.GetComponent<LaserHead>().SetSplit(TargetSplit);
                // フラグ更新
                Use_flg_3 = true;
            }
            // 4回目
            if (Use_flg_4 == false && timer >= StartTime_4)
            {
                TargetSplit = 4;
                // レーザー作成
                GameObject obj3 = Instantiate(prefab, transform.position, transform.rotation); // プレハブオブジェクトを生成する
                obj3.GetComponent<LaserHead>().SetSplit(TargetSplit);
                // フラグ更新
                Use_flg_4 = true;
            }
            // 5回目
            if (Use_flg_5 == false && timer >= StartTime_5)
            {
                TargetSplit = 5;
                // レーザー作成
                GameObject obj4 = Instantiate(prefab, transform.position, transform.rotation); // プレハブオブジェクトを生成する
                obj4.GetComponent<LaserHead>().SetSplit(TargetSplit);
                // フラグ更新
                Use_flg_5 = true;
            }
        }
        else
        {// アタックフェイズ
            Reset_flg = true;
        }
    }
}

