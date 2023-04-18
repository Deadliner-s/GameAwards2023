using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissileBoss : MonoBehaviour
{
    static int FlagMax = 3;
    public GameObject HomingMissile;
    public GameObject ContenaMissile;
    public float DestroyTime;
    public string KeyHoming;
    public string KeyContena;

    GameObject player;

    // 進行時間用
    private float timer;

    // 発射間隔用
    private float IntervalTimeHoming;
    private float IntervalTimeContena;

    // リセットフラグ
    private bool Reset_flg = false;

    // 現在フェイズ
    private PhaseManager.Phase currentPhase;

    // これコピペ
    public float[] StartTimeHoming = new float[FlagMax];           //出すタイミング
    public float[] DurationHoming = new float[FlagMax];           //出すトータル長さ
    public float[] IntervalTriggerHoming = new float[FlagMax];   //出す間隔
    public float[] StartTimeContena = new float[FlagMax];           //出すタイミング
    public float[] DurationContena = new float[FlagMax];           //出すトータル長さ
    public float[] IntervalTriggerContena = new float[FlagMax];   //出す間隔

    // 確認用フラグ
    private bool[] UseFlagHoming = new bool[FlagMax];
    private bool[] UseFlagContena = new bool[FlagMax];

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();

        // 時間の初期化
        timer = 0.0f;
        IntervalTimeHoming = 0.0f;
        IntervalTimeContena = 0.0f;
        for(int i = 0; i < FlagMax;i++)
        {
            UseFlagHoming[i] = false;
            UseFlagContena[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)//プレイヤーは生きている（存在する）
        {
            // デバッグ用
            if (Input.GetKeyDown(KeyHoming))
            {
                GameObject obj = Instantiate(HomingMissile, transform.position, Quaternion.identity);
                Destroy(obj, DestroyTime);
            }
            if (Input.GetKeyDown(KeyContena))
            {
                GameObject obj = Instantiate(ContenaMissile, transform.position, Quaternion.identity);
                Destroy(obj, DestroyTime);
            }
            // フェイズ確認
            currentPhase = PhaseManager.instance.GetPhase();

            if (currentPhase == PhaseManager.Phase.Attack_Phase)
            {   
                // アタックフェイズ
                // 各フラグ、変数などリセット
                if (Reset_flg)
                {
                    timer = 0.0f;
                    IntervalTimeHoming = 0.0f;
                    IntervalTimeContena = 0.0f;
                    for(int i = 0;i < FlagMax; i++)
                    { 
                        UseFlagHoming[i] = false;
                        UseFlagContena[i] = false;
                    }
                    Reset_flg = false;
                }
                // 時間更新
                timer += Time.deltaTime;
                IntervalTimeHoming += Time.deltaTime;
                IntervalTimeContena += Time.deltaTime;
                // i回目インターバル
                for (int i = 0; i < FlagMax; i++)
                {
                    if (!UseFlagHoming[i] && timer >= StartTimeHoming[i])
                    {
                        if (IntervalTimeHoming >= IntervalTriggerHoming[i])
                        {
                            GameObject obj = Instantiate(HomingMissile, transform.position, Quaternion.identity);
                            Destroy(obj, DestroyTime);
                            IntervalTimeHoming = 0.0f;
                        }
                        if (StartTimeHoming[i] + DurationHoming[i] <= timer)
                        {
                            UseFlagHoming[i] = true;
                        }
                    }
                    if (!UseFlagContena[i] && timer >= StartTimeContena[i])
                    {
                        if (IntervalTimeContena >= IntervalTriggerContena[i])
                        {
                            GameObject obj = Instantiate(ContenaMissile, transform.position, Quaternion.identity);
                            Destroy(obj, DestroyTime);
                            IntervalTimeContena = 0.0f;
                        }
                        if (StartTimeContena[i] + DurationContena[i] <= timer)
                        {
                            UseFlagContena[i] = true;
                        }
                    }
                }
            }
            else // ハイスピードフェイズ
            {
                Reset_flg = true;
            }
        }
    }
}