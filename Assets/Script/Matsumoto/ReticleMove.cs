using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReticleMove : MonoBehaviour
{
    [Header("プレイヤーから見た照準の初期位置")]
    public float initPosX = 0.0f;
    public float initPosY = 50.0f;

    [Header("移動速度")]
    public float speed = 5.0f;      // 移動スピード
    [Header("ロックオンできる最大数")]
    public int MaxRockOn = 5;       // ロックオンできる最大数
    [Header("照準の半径")]
    public float ReticleRadius = 50.0f; // 照準の半径
    [Header("引き寄せられ始める距離")]
    public float AttractDistance = 50.0f;
    [Header("引き寄せられる力")]
    public float AttractPower = 10.0f;



    private Myproject InputActions; // InputSystem
    private Vector3 pos;            // 現在の位置
    private Vector3 nextPosition;   // 移動後の位置
    private float viewX;            // ビューポート座標のxの値
    private float viewY;            // ビューポート座標のyの値
    private GameObject player;      // プレイヤー


    private GameObject[] RockOnCnt = new GameObject[5];     // ロックオンできる数オブジェクト
    private GameObject RockOnCntPrefab;                     // ●のPrefab
    private float offsetY = 40.0f;

    private GameObject enemy;
    private Vector3 enemy2D;

    void Awake()
    {
        // コントローラー
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
    }

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置
        player = GameObject.Find("Player");
        Vector3 playerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, player.transform.position);
        playerPos.x = playerPos.x + initPosX;
        playerPos.y = playerPos.y + initPosY;
        transform.position = playerPos;

        // 初期化
        pos = transform.position;
        nextPosition = pos;


        // ロックオンできる数分、●を生成する
        for (int i = 0; i < 5; i++)
        {
            RockOnCntPrefab = (GameObject)Resources.Load("RockOnCnt");
            RockOnCnt[i] = Instantiate(RockOnCntPrefab, this.transform.position, Quaternion.identity, this.transform);
        }
        // 照準の下に表示させる
        Vector3 thisPos = this.transform.position;
        RockOnCnt[4].transform.position = new Vector3(thisPos.x - 25.0f, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[3].transform.position = new Vector3(thisPos.x - 12.5f, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[2].transform.position = new Vector3(thisPos.x, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[1].transform.position = new Vector3(thisPos.x + 12.5f, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[0].transform.position = new Vector3(thisPos.x + 25.0f, thisPos.y - offsetY, thisPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        Move();


        // ロックオンしたターゲットタグが付いたオブジェクトをカウント
        GameObject[] tagObj = GameObject.FindGameObjectsWithTag("Target");

        // 一番近い敵のオブジェクトを取得
        enemy = serchTag(this.gameObject, "Enemy");

        if (enemy != null)
        {
            if(enemy.GetComponent<RockOnMarker>() != null)
            {
                // ロックオンのマークが出現している場合
                if (enemy.GetComponent<RockOnMarker>().GetRockOnFlg() == true)
                {
                    // ロックオンのマークが隠れていない場合
                    if (enemy.GetComponent<RockOnMarker>().GetHideFlg() == false)
                    {
                        // ワールド座標をスクリーン座標に変換
                        enemy2D = RectTransformUtility.WorldToScreenPoint(Camera.main, enemy.transform.position);

                        // 三平方の定理
                        float a;                     // 辺X
                        float b;                     // 辺Y
                        float c;                     // a - b間の距離
                        float TargetRadius = enemy.GetComponent<RockOnMarker>().TargetRadius;

                        a = enemy2D.x - transform.position.x;
                        b = enemy2D.y - transform.position.y;
                        a = a * a;                   // aの累乗
                        b = b * b;                   // bの累乗
                        c = a + b;                   // a + b の距離
                        c = (float)Math.Sqrt(c);     // 平方根
                       
                        float r = ReticleRadius + TargetRadius;

                        // 敵に近づいたら照準を引き寄せる
                        if (c <= AttractPower)
                        {
                            // タグを持っていたら    ロックオンした後のオブジェクトに寄らないように
                            if (enemy.tag == "Enemy")
                            {
                                transform.position = Vector2.MoveTowards(transform.position, enemy2D, AttractDistance);
                            }
                        }

                        // 当たり判定
                        if (c <= r)
                        {
                            // ロックオンできる数以下だったら
                            if (tagObj.Length < MaxRockOn)
                            {
                                //// 回転
                                //Transform transform = target.transform;
                                //Vector3 angle = transform.localEulerAngles;
                                //angle.z = 45.0f;
                                //transform.localEulerAngles = angle;
                                //// 色を赤に変更
                                //Color color = target.GetComponent<Image>().color = Color.red; ;

                                enemy.GetComponent<RockOnMarker>().RockOnAnime();
                                enemy.gameObject.tag = "Target";
                                SoundManager.instance.PlaySE("RockOn");
                            }
                        }
                    }
                }
            }
        }

        // 下の・の処理
        // ロックオンした数によって色を変える
        if (tagObj.Length > 0)
        {
            for (int i = 0; i < tagObj.Length; i++)
            {
                Color color = RockOnCnt[i].GetComponent<Image>().color = Color.gray;
            }
        }
        // ロックオン解除されたら色を戻す
        for (int i = tagObj.Length; i < 5; i++)
        {
            Color color = RockOnCnt[i].GetComponent<Image>().color = Color.white;
        }
    }

    private void Move()
    {
        // 入力
        Vector3 move = InputActions.Player.Reticle.ReadValue<Vector2>();
        nextPosition = pos + move * speed;

        // 移動後のビューポート座標のxの値を取得
        viewX = Camera.main.ScreenToViewportPoint(nextPosition).x;
        viewY = Camera.main.ScreenToViewportPoint(nextPosition).y;

        // 移動後のビューポート座標が０から１の範囲ならば
        if (0.0f <= viewX && viewX <= 1.0f && 0.0f <= viewY && viewY <= 1.0f)
        {
            // 移動
            transform.position = nextPosition;

            pos = nextPosition;
        }
    }



    private GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            Vector3 enemypos = RectTransformUtility.WorldToScreenPoint(Camera.main, obs.transform.position);

            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(enemypos, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }
}
