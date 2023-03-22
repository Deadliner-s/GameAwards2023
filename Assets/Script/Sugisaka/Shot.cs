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
    public GameObject Shotpos;
    // 経過時間
    private int currenttime;
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

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Shot.performed += context => OnShot();
    }

    // Start is called before the first frame update
    private void Start()
    {
        currenttime = 0;    // 経過時間初期化
        Shotflg = false;    // フラグをオフに

        // フェイズ取得
        AtkPhaseFlg = PhaseObj.activeSelf;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Shotflg == true)
        {
            currenttime++;

            if (currenttime > ReloadTime)
            {
                Shotflg = false;
            }
        }

        // フェイズ確認
        AtkPhaseFlg = PhaseObj.activeSelf;
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
                Vector3 PlayerPos = Shotpos.transform.position;
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
                    // 新しい誘導ミサイルプレハブをインスタンス化する
                    GameObject missile = Instantiate(MissilePrefab, PlayerPos, Quaternion.identity);

                    // 誘導ミサイルのターゲットを設定する
                    missile.GetComponent<TrackingBullet>().SetTarget(target);
                }

                // 経過時間初期化
                currenttime = 0;
                // 射撃フラグを立てる
                Shotflg = true;
            }
        }
    }
}
