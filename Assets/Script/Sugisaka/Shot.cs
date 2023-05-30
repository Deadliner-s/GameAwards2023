using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shot : MonoBehaviour
{
    public static Shot instance;
    //Myproject InputActions;

    [Header("ミサイル発射用")]
    [Tooltip("プレイヤーミサイルのプレハブ")]
    public GameObject MissilePrefab;
    [Tooltip("装填時間")]
    public int ReloadTime;
    [Tooltip("ミサイル生成場所")]
    public GameObject Shotpos;
    [Tooltip("発射間隔")]
    public float interval;

    // 経過時間
    private float currenttime;
    // インターバル用時間
    private float intervalTime;
    // 射撃フラグ
    public bool Shotflg;

    // 現在フェイズ
    private PhaseManager.Phase currentPhase;
    private PhaseManager.Phase nextPhase;

    // ロックオンされたオブジェクトのリスト
    public List<GameObject> targets;
    // 退避用
    public List<GameObject> sub = new List<GameObject>();

    //void Awake()
    //{
    //    InputActions = new Myproject();
    //    InputActions.Enable();
    //    InputActions.Player.Shot.performed += context => OnShot();
    //    InputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
    //}

    // Start is called before the first frame update
    private void Start()
    {
        currenttime = 0.0f;    // 経過時間初期化
        intervalTime = 0.0f;
        Shotflg = false;    // フラグをオフに

        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();
        nextPhase = currentPhase;
    }

    // Update is called once per frame
    private void Update()
    {

        if (Shotflg == true)
        {
            // 時間の更新
            currenttime += Time.deltaTime;

            // 弾の発射
            if (targets.Count != 0)                
            {
                // 発射間隔の時間更新
                intervalTime += Time.deltaTime;

                if (interval <= intervalTime)
                {
                    // ロックオンされたターゲットにミサイルを打つ
                    // 生成場所取得
                    Vector3 PlayerPos = Shotpos.transform.position;

                    // 
                    if (targets[0] == null)
                    {
                        int num = targets.Count;

                        for (int i = 0; i < num; i++)
                        {
                            if (targets[0] == null)
                            { 
                                targets.RemoveAt(0);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //
                    if (targets.Count != 0)
                    {
                        // MissileObjをタグ検索
                        GameObject missileObj = GameObject.FindGameObjectWithTag("MissileObj");
                        // 新しい誘導ミサイルプレハブをインスタンス化する
                        GameObject missileUp = Instantiate(MissilePrefab, PlayerPos, Quaternion.identity);
                        // ミサイルオブジェクトの子にする
                        missileUp.transform.parent = missileObj.transform;
                        GameObject missileDown = Instantiate(MissilePrefab, PlayerPos, Quaternion.identity);
                        // ミサイルオブジェクトの子にする
                        missileDown.transform.parent = missileObj.transform;
                        // ミサイルのターゲットを設定する
                        missileUp.GetComponent<TrackingMissile_3>().SetTarget(targets[0], 1);
                        missileDown.GetComponent<TrackingMissile_3>().SetTarget(targets[0], -1);

                        //SE再生
                        int num = targets.Count;
                        switch (num)
                        {
                            case (5):
                                SoundManager.instance.PlaySE("Shot5");
                                VibrationManager.instance.StartCoroutine("PlayVibration", "Shot");
                                break;
                            case (4):
                                SoundManager.instance.PlaySE("Shot4");
                                VibrationManager.instance.StartCoroutine("PlayVibration", "Shot");
                                break;
                            case (3):
                                SoundManager.instance.PlaySE("Shot3");
                                VibrationManager.instance.StartCoroutine("PlayVibration", "Shot");
                                break;
                            case (2):
                                SoundManager.instance.PlaySE("Shot2");
                                VibrationManager.instance.StartCoroutine("PlayVibration", "Shot");
                                break;
                            case (1):
                                SoundManager.instance.PlaySE("Shot1");
                                VibrationManager.instance.StartCoroutine("PlayVibration", "Shot");
                                break;
                        }

                        // ターゲットリスト更新
                        targets.RemoveAt(0);

                        intervalTime = 0.0f;
                    }
                }   
            }

            // フラグ管理
            if (targets.Count == 0 && currenttime > ReloadTime)
            {
                Shotflg = false;
            }
        }


        // フェイズ確認

        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();
        if (currentPhase != nextPhase)
        {
            nextPhase = currentPhase;
            if (currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                sub = new List<GameObject>();
            }
        }

        if(InputManager.instance.OnShot())
        {
            // フェイズの確認
            if (currentPhase == PhaseManager.Phase.Attack_Phase)
            {
                // アタックフェイズ    
                // フラグの確認
                if (Shotflg == false)
                {
                    // リスト再生成
                    targets = new List<GameObject>();

                    // 配列にタグがTargetのオブジェクトを入れる
                    GameObject[] targetsObj = GameObject.FindGameObjectsWithTag("Target");
                    foreach (GameObject target in targetsObj)
                    {
                        // ターゲットがリストに含まれていなければ追加する
                        if (!targets.Contains(target))
                        {
                            targets.Add(target);
                        }
                    }

                    // 経過時間初期化
                    currenttime = 0.0f;
                    // 射撃フラグを立てる
                    Shotflg = true;
                    // 即発射用
                    intervalTime = 10.0f;
                }
            }
        }

    }

    //public void OnShot()
    //{

    //}
}
