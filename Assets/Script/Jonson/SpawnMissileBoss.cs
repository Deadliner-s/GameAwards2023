using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissileBoss : MonoBehaviour
{
    static int FlagMax = 3;
    public GameObject HomingMissile;
    public float HomingSpeed;
    public float HomingAccel;
    public float HomingMax;
    public float HomingHeight;
    public GameObject ContenaMissile;
    public float ContenaSpeed;
    public float ContenaAccel;
    public float ContenaMax;
    public float ContenaRange;
    public int ContenaNumber;
    public float DestroyTimeHoming;
    public float DestroyTimeContena;
    public string KeyHoming;
    public string KeyContena;
    public GameObject SpawnPos2;
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

    [Header("AttackPhaseから何秒たったら出すか（誘導）")]
    public float[] StartTimeHoming = new float[FlagMax];           //出すタイミング

    [Header("何秒出すか（誘導）")]
    public float[] DurationHoming = new float[FlagMax];           //出すトータル長さ

    [Header("ミサイル一個出す間隔（誘導）")]
    public float[] IntervalTriggerHoming = new float[FlagMax];   //出す間隔

    [Header("AttackPhaseから何秒たったら出すか（コンテナ）")]
    public float[] StartTimeContena = new float[FlagMax];           //出すタイミング

    [Header("何秒出すか（コンテナ）")]
    public float[] DurationContena = new float[FlagMax];           //出すトータル長さ

    [Header("ミサイル一個出す間隔（コンテナ）")]
    public float[] IntervalTriggerContena = new float[FlagMax];   //出す間隔

    // 確認用フラグ
    private bool[] UseFlagHoming = new bool[FlagMax];
    private bool[] UseFlagContena = new bool[FlagMax];

    bool left;
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
        for (int i = 0; i < FlagMax; i++)
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
                CreateHoming();   
            }
            if (Input.GetKeyDown(KeyContena))
            {
                CreateContena();
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
                    for (int i = 0; i < FlagMax; i++)
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
                            CreateHoming();
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
                            CreateContena();
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

    void CreateContena()
    {
        Vector3 sPos;
        if (left)
        {
            left = false;
            sPos = SpawnPos2.transform.position;
        }
        else
        {
            left = true;
            sPos = transform.position;
        }
        GameObject obj = Instantiate(ContenaMissile,sPos, Quaternion.identity);
        obj.GetComponent<MissileBossContena>().Speed = ContenaSpeed;
        obj.GetComponent<MissileBossContena>().Accel = ContenaAccel;
        obj.GetComponent<MissileBossContena>().MaxSpeed = ContenaMax;
        obj.GetComponent<MissileBossContena>().ContenaRange = ContenaRange;
        obj.GetComponent<MissileBossContena>().ContenaNumber = ContenaNumber;
        Destroy(obj, DestroyTimeContena);
    }

    void CreateHoming()
    {
        Vector3 sPos;
        if (left)
        {
            left = false;
            sPos = SpawnPos2.transform.position;
        }
        else
        {
            left = true;
            sPos = transform.position;
        }
        GameObject obj = Instantiate(HomingMissile, sPos, Quaternion.identity);
        obj.GetComponent<MissileBossHoming>().Speed = HomingSpeed;
        obj.GetComponent<MissileBossHoming>().Accel = HomingAccel;
        obj.GetComponent<MissileBossHoming>().MaxSpeed = HomingMax;
        obj.GetComponent<MissileBossHoming>().Height = HomingHeight;
        Destroy(obj, DestroyTimeHoming);
    }

}