using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissileScreen : MonoBehaviour
{
    static int FlagMax = 3;
    public GameObject ShowSpeedMissile;
    public GameObject SpeedMissile;
    public float SpeedStraight;
    public GameObject ShowClusterMissile;
    public GameObject ClusterMissile;
    public float SpeedCluster;
    public int ClusterNumber;
    public float ShowDelay1;
    public float ShowDelay2;
    public float SpawnDelay;
    public string KeyCluster;
    public GameObject SpawnTop;
    public string KeyTop;
    public GameObject SpawnTopLeft;
    public string KeyTopLeft;
    public GameObject SpawnLeft;
    public string KeyLeft;
    public GameObject SpawnBotLeft;
    public string KeyBotLeft;
    public GameObject SpawnBot;
    public string KeyBot;
    public float DestroyTime;

    GameObject player;

    private float timer;

    // 発射間隔用
    private float IntervalTimeTop;
    private float IntervalTimeTopLeft;
    private float IntervalTimeLeft;
    private float IntervalTimeBotLeft;
    private float IntervalTimeBot;
    private float IntervalTimeCluster;

    // リセットフラグ
    private bool Reset_flg = false;

    // 現在フェイズ
    private PhaseManager.Phase currentPhase;

    [Header("AttackPhaseから何秒たったら出すか（クラスター）")]
    public float[] StartTimeCluster = new float[FlagMax];           //出すタイミング

    [Header("何秒出すか（クラスター）")]
    public float[] DurationCluster = new float[FlagMax];           //出すトータル長さ

    [Header("ミサイル一個出す間隔（クラスター）")]
    public float[] IntervalTriggerCluster = new float[FlagMax];   //出す間隔

    [Header("AttackPhaseから何秒たったら出すか（上）")]
    public float[] StartTimeTop = new float[FlagMax];           //出すタイミング

    [Header("何秒出すか（上）")]
    public float[] DurationTop = new float[FlagMax];           //出すトータル長さ

    [Header("ミサイル一個出す間隔（上）")]
    public float[] IntervalTriggerTop = new float[FlagMax];   //出す間隔

    [Header("AttackPhaseから何秒たったら出すか（左上）")]
    public float[] StartTimeTopLeft = new float[FlagMax];           //出すタイミング

    [Header("何秒出すか（左上）")]
    public float[] DurationTopLeft = new float[FlagMax];           //出すトータル長さ

    [Header("ミサイル一個出す間隔（左上）")]
    public float[] IntervalTriggerTopLeft = new float[FlagMax];   //出す間隔

    [Header("AttackPhaseから何秒たったら出すか（左）")]
    public float[] StartTimeLeft = new float[FlagMax];           //出すタイミング

    [Header("何秒出すか（左）")]
    public float[] DurationLeft = new float[FlagMax];           //出すトータル長さ

    [Header("ミサイル一個出す間隔（左）")]
    public float[] IntervalTriggerLeft = new float[FlagMax];   //出す間隔

    [Header("AttackPhaseから何秒たったら出すか（左下）")]
    public float[] StartTimeBotLeft = new float[FlagMax];           //出すタイミング

    [Header("何秒出すか（左下）")]
    public float[] DurationBotLeft = new float[FlagMax];           //出すトータル長さ

    [Header("ミサイル一個出す間隔（左下）")]
    public float[] IntervalTriggerBotLeft = new float[FlagMax];   //出す間隔

    [Header("AttackPhaseから何秒たったら出すか（下）")]
    public float[] StartTimeBot = new float[FlagMax];           //出すタイミング

    [Header("何秒出すか（下）")]
    public float[] DurationBot = new float[FlagMax];           //出すトータル長さ

    [Header("ミサイル一個出す間隔（下）")]
    public float[] IntervalTriggerBot = new float[FlagMax];   //出す間隔

    // 確認用フラグ
    private bool[] UseFlagCluster = new bool[FlagMax];
    private bool[] UseFlagTop = new bool[FlagMax];
    private bool[] UseFlagTopLeft = new bool[FlagMax];
    private bool[] UseFlagLeft = new bool[FlagMax];
    private bool[] UseFlagBotLeft = new bool[FlagMax];
    private bool[] UseFlagBot = new bool[FlagMax];

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();

        // 時間の初期化
        timer = 0.0f;
        IntervalTimeCluster = 0.0f;
        IntervalTimeTop = 0.0f;
        IntervalTimeTopLeft = 0.0f;
        IntervalTimeLeft = 0.0f;
        IntervalTimeBotLeft = 0.0f;
        IntervalTimeBot = 0.0f;
        for (int i = 0; i < FlagMax; i++)
        {
            UseFlagCluster[i] = false;
            UseFlagTop[i] = false;
            UseFlagTopLeft[i] = false;
            UseFlagLeft[i] = false;
            UseFlagBotLeft[i] = false;
            UseFlagBot[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)//プレイヤーは生きている（存在する）
        {
            // デバッグ用
            if (Input.GetKeyDown(KeyCluster))
            {
                Invoke("SpawnShowCluster", ShowDelay1);
                Invoke("SpawnShowCluster", ShowDelay2);
                Invoke("SpawnCluster", SpawnDelay);
            }
            if (Input.GetKeyDown(KeyTop))
            {
                Invoke("SpawnShowSpeed", ShowDelay1);
                Invoke("SpawnShowSpeed", ShowDelay2);
                Invoke("SpawnSpeedTop", SpawnDelay);
            }
            if (Input.GetKeyDown(KeyTopLeft))
            {
                Invoke("SpawnShowSpeed", ShowDelay1);
                Invoke("SpawnShowSpeed", ShowDelay2);
                Invoke("SpawnSpeedTopLeft", SpawnDelay);
            }
            if (Input.GetKeyDown(KeyLeft))
            {
                Invoke("SpawnShowSpeed", ShowDelay1);
                Invoke("SpawnShowSpeed", ShowDelay2);
                Invoke("SpawnSpeedLeft", SpawnDelay);
            }
            if (Input.GetKeyDown(KeyBotLeft))
            {
                Invoke("SpawnShowSpeed", ShowDelay1);
                Invoke("SpawnShowSpeed", ShowDelay2);
                Invoke("SpawnSpeedBotLeft", SpawnDelay);
            }
            if (Input.GetKeyDown(KeyBot))
            {
                Invoke("SpawnShowSpeed", ShowDelay1);
                Invoke("SpawnShowSpeed", ShowDelay2);
                Invoke("SpawnSpeedBot", SpawnDelay);
            }

            // フェイズ確認
            currentPhase = PhaseManager.instance.GetPhase();

            if (currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                // アタックフェイズ
                // 各フラグ、変数などリセット
                if (Reset_flg)
                {
                    timer = 0.0f;
                    IntervalTimeCluster = 0.0f;
                    IntervalTimeTop = 0.0f;
                    IntervalTimeTopLeft = 0.0f;
                    IntervalTimeLeft = 0.0f;
                    IntervalTimeBotLeft = 0.0f;
                    IntervalTimeBot = 0.0f;
                    for (int i = 0; i < FlagMax; i++)
                    {
                        UseFlagCluster[i] = false;
                        UseFlagTop[i] = false;
                        UseFlagTopLeft[i] = false;
                        UseFlagLeft[i] = false;
                        UseFlagBotLeft[i] = false;
                        UseFlagBot[i] = false;
                    }
                    Reset_flg = false;
                }
                // 時間更新
                timer += Time.deltaTime;
                IntervalTimeCluster += Time.deltaTime;
                IntervalTimeTop += Time.deltaTime;
                IntervalTimeTopLeft += Time.deltaTime;
                IntervalTimeLeft += Time.deltaTime;
                IntervalTimeBotLeft += Time.deltaTime;
                IntervalTimeBot += Time.deltaTime;
                // i回目インターバル
                for (int i = 0; i < FlagMax; i++)
                {
                    if (!UseFlagCluster[i] && timer >= StartTimeCluster[i])
                    {
                        if (IntervalTimeCluster >= IntervalTriggerCluster[i])
                        {
                            Invoke("SpawnShowCluster", ShowDelay1);
                            Invoke("SpawnShowCluster", ShowDelay2);
                            Invoke("SpawnCluster", SpawnDelay);
                            IntervalTimeCluster = 0.0f;
                        }
                        if (StartTimeCluster[i] + DurationCluster[i] <= timer)
                        {
                            UseFlagCluster[i] = true;
                        }
                    }
                    if (!UseFlagTop[i] && timer >= StartTimeTop[i])
                    {
                        if (IntervalTimeTop >= IntervalTriggerTop[i])
                        {
                            Invoke("SpawnShowSpeed", ShowDelay1);
                            Invoke("SpawnShowSpeed", ShowDelay2);
                            Invoke("SpawnSpeedTop", SpawnDelay);
                            IntervalTimeTop = 0.0f;
                        }
                        if (StartTimeTop[i] + DurationTop[i] <= timer)
                        {
                            UseFlagTop[i] = true;
                        }
                    }
                    if (!UseFlagTopLeft[i] && timer >= StartTimeTopLeft[i])
                    {
                        if (IntervalTimeTopLeft >= IntervalTriggerTopLeft[i])
                        {
                            Invoke("SpawnShowSpeed", ShowDelay1);
                            Invoke("SpawnShowSpeed", ShowDelay2);
                            Invoke("SpawnSpeedTopLeft", SpawnDelay);
                            IntervalTimeTopLeft = 0.0f;
                        }
                        if (StartTimeTopLeft[i] + DurationTopLeft[i] <= timer)
                        {
                            UseFlagTopLeft[i] = true;
                        }
                    }
                    if (!UseFlagLeft[i] && timer >= StartTimeLeft[i])
                    {
                        if (IntervalTimeLeft >= IntervalTriggerLeft[i])
                        {
                            Invoke("SpawnShowSpeed", ShowDelay1);
                            Invoke("SpawnShowSpeed", ShowDelay2);
                            Invoke("SpawnSpeedLeft", SpawnDelay);
                            IntervalTimeLeft = 0.0f;
                        }
                        if (StartTimeLeft[i] + DurationLeft[i] <= timer)
                        {
                            UseFlagLeft[i] = true;
                        }
                    }
                    if (!UseFlagBotLeft[i] && timer >= StartTimeBotLeft[i])
                    {
                        if (IntervalTimeBotLeft >= IntervalTriggerBotLeft[i])
                        {
                            Invoke("SpawnShowSpeed", ShowDelay1);
                            Invoke("SpawnShowSpeed", ShowDelay2);
                            Invoke("SpawnSpeedBotLeft", SpawnDelay);
                            IntervalTimeBotLeft = 0.0f;
                        }
                        if (StartTimeBotLeft[i] + DurationBotLeft[i] <= timer)
                        {
                            UseFlagBotLeft[i] = true;
                        }
                    }
                    if (!UseFlagBot[i] && timer >= StartTimeBot[i])
                    {
                        if (IntervalTimeBot >= IntervalTriggerBot[i])
                        {
                            Invoke("SpawnShowSpeed", ShowDelay1);
                            Invoke("SpawnShowSpeed", ShowDelay2);
                            Invoke("SpawnSpeedBot", SpawnDelay);
                            IntervalTimeBot = 0.0f;
                        }
                        if (StartTimeBot[i] + DurationBot[i] <= timer)
                        {
                            UseFlagBot[i] = true;
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

    void SpawnCluster()
    {
        GameObject obj = Instantiate(ClusterMissile, SpawnLeft.transform.position, Quaternion.identity);
        SpawnLeft.GetComponent<UINumber>().Timer += 120;
        obj.GetComponent<MissileBossCluster>().Speed = SpeedCluster;
        obj.GetComponent<MissileBossCluster>().ClusterNumber = ClusterNumber;
        Destroy(obj, DestroyTime);
    }    
    void SpawnSpeedTop()
    {
        GameObject obj = Instantiate(SpeedMissile, SpawnTop.transform.position, Quaternion.identity);
        obj.GetComponent<MissileStraight>().MaxSpeed = SpeedStraight;
        Destroy(obj, DestroyTime);
    }
    void SpawnSpeedTopLeft()
    {
        GameObject obj = Instantiate(SpeedMissile, SpawnTopLeft.transform.position, Quaternion.identity);
        obj.GetComponent<MissileStraight>().MaxSpeed = SpeedStraight;
        Destroy(obj, DestroyTime);
    }
    void SpawnSpeedLeft()
    {
        GameObject obj = Instantiate(SpeedMissile, SpawnLeft.transform.position, Quaternion.identity);
        obj.GetComponent<MissileStraight>().MaxSpeed = SpeedStraight;
        Destroy(obj, DestroyTime);
    }
    void SpawnSpeedBotLeft()
    {
        GameObject obj = Instantiate(SpeedMissile, SpawnBotLeft.transform.position, Quaternion.identity);
        obj.GetComponent<MissileStraight>().MaxSpeed = SpeedStraight;
        Destroy(obj, DestroyTime);
    }
    void SpawnSpeedBot()
    {
        GameObject obj = Instantiate(SpeedMissile, SpawnBot.transform.position, Quaternion.identity);
        obj.GetComponent<MissileStraight>().MaxSpeed = SpeedStraight;
        Destroy(obj, DestroyTime);
    }

    void SpawnShowCluster()
    {
        GameObject obj = Instantiate(ShowClusterMissile, transform.position, Quaternion.identity);
        Destroy(obj, DestroyTime);
    }
    void SpawnShowSpeed()
    {
        GameObject obj = Instantiate(ShowSpeedMissile, transform.position, Quaternion.identity);
        Destroy(obj, DestroyTime);
    }

}
