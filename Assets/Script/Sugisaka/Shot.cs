using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shot : MonoBehaviour
{
    public static Shot instance;
    Myproject InputActions;

    [Header("ミサイル発射用")]
    [Tooltip("プレイヤーミサイルのプレハブ")]
    public GameObject MissilePrefab;
    [Tooltip("装填時間")]
    public int ReloadTime;
    [Tooltip("ミサイル生成場所")]
    public GameObject Shotpos1;
    public GameObject Shotpos2;
    public GameObject Shotpos3;
    public GameObject Shotpos4;
    public GameObject Shotpos5;

    // 経過時間
    private float currenttime;
    // 射撃フラグ
    private bool Shotflg;

    //フェイズ切り替え用
    [Header("フェイズ確認用オブジェクト")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;

    // ロックオンされたオブジェクトのリスト
    public List<GameObject> targets;
    // 退避用
    public List<GameObject> sub = new List<GameObject>();

    private int num;
    private Vector3 PlayerPos;

    public AudioClip ShotSE;
    private AudioSource audioSource;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Shot.performed += context => OnShot();
    }

    // Start is called before the first frame update
    private void Start()
    {
        currenttime = 0.0f;    // 経過時間初期化
        Shotflg = false;    // フラグをオフに

        // フェイズ取得
        AtkPhaseFlg = PhaseObj.activeSelf;

        audioSource = GetComponent<AudioSource>();

        num = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Shotflg == true)
        {
            currenttime += Time.deltaTime;

            if (currenttime > ReloadTime)
            {
                Shotflg = false;
            }
        }

        // フェイズ確認
        AtkPhaseFlg = PhaseObj.activeSelf;
        if (AtkPhaseFlg == false)
        {
            sub = new List<GameObject>();
        }

    }

    public void OnShot()
    {
        // フェイズの確認
        if (AtkPhaseFlg == true)
        {
            // アタックフェイズ    
            // フラグの確認
            if (Shotflg == false)
            {
                // ターゲットリストをサブに退避
                foreach (GameObject k in targets)
                {
                    // ターゲットがリストに含まれていなければ追加する
                    if (!sub.Contains(k))
                    {
                        sub.Add(k);
                    }
                }

                // 生成場所取得
                //Vector3 PlayerPos = Shotpos1.transform.position;
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

                // 各ロックオンされたターゲットにミサイルを打つ
                foreach (GameObject target in targets)
                {
                    switch(num)
                    {
                        case (0):
                            // 生成場所取得
                            PlayerPos = Shotpos1.transform.position;
                            break;
                        case (1):
                            // 生成場所取得
                            PlayerPos = Shotpos2.transform.position;
                            break;
                        case (2):
                            // 生成場所取得
                            PlayerPos = Shotpos3.transform.position;
                            break;
                        case (3):
                            // 生成場所取得
                            PlayerPos = Shotpos4.transform.position;
                            break;
                        case (4):
                            // 生成場所取得
                            PlayerPos = Shotpos5.transform.position;
                            break;
                    }
                    // 新しい誘導ミサイルプレハブをインスタンス化する
                    GameObject missile = Instantiate(MissilePrefab, PlayerPos, Quaternion.identity);

                    // 誘導ミサイルのターゲットを設定する
                    missile.GetComponent<TrackingBullet>().SetTarget(target);

                    audioSource.PlayOneShot(ShotSE);

                    num++;
                }

                // 経過時間初期化
                currenttime = 0;
                // 射撃フラグを立てる
                Shotflg = true;
                // 生成場所初期化
                num = 0;
            }
        }
    }
}
