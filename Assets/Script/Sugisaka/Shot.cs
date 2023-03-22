using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shot : MonoBehaviour
{
    public static Shot instance;

    Myproject InputActions;

    // ミサイルのプレハブ
    public GameObject MissilePrefab;

    // 装填時間
    public int ReloadTime;
    // 経過時間
    private int currenttime;
    // 射撃フラグ
    private bool Shotflg;
    // ミサイル生成場所
    public GameObject Shotpos;


    // ロックオンされたオブジェクトのリスト
    public List<GameObject> targets = new List<GameObject>();

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Shot.performed += context => OnShot();
    }

    // Start is called before the first frame update
    void Start()
    {
        currenttime = 0;    // 経過時間初期化
        Shotflg = false;    // フラグをオフに

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Shotflg == true)
        {
            currenttime++;

            if (currenttime > ReloadTime)
            {
                Shotflg = false;
            }
        }
    }

    public void OnShot()
    {
        if (Shotflg == false)
        {
            Vector3 PlayerPos = Shotpos.transform.position;

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

            currenttime = 0;
        }
        
    }
}
