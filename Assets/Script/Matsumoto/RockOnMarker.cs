// ロックオンしたいオブジェクトに追加
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class RockOnMarker : MonoBehaviour
{
    private GameObject reticle;                 // 照準
    private GameObject target;                  // ロックオした時のマーク
    private GameObject canvas;                  // キャンバス

    private Vector3 reticle2D;                  // 照準のスクリーン座標
    private Vector3 enemy2D;                    // 敵のスクリーン座標

    private bool rockonFlg = false;             // ロックオンしたかどうか
    private bool destroyFlg = false;            // 画面外で消えたかどうか
    // private bool animeFlg = false;              // アニメーション再生用フラグ

    public float TargetRadius = 1.0f;           // 敵の半径
    public bool FrameOutDestroy = false;        // 画面外で消すか
    public int MaxRockOn = 5;                   // ロックオンできる最大数
    public float AttractDistance = 50.0f;       // 引き寄せられ始める距離
    public float AttractPower = 10.0f;          // 引き寄せられる力

    private UnityEvent OnDestroyed = new UnityEvent();

    // RockOnAnimeで関数を使うため
    //public static RockOnMarker instance;

    // Start is called before the first frame update
    void Start()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}

        reticle = GameObject.Find("Reticle");   // 照準のオブジェクト指定
        canvas = GameObject.Find("Canvas");     // キャンバスを指定
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        // ワールド座標をスクリーン座標に変換
        reticle2D = reticle.transform.position; // UIなので元々スクリーン座標
        enemy2D = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);

        // 三平方の定理
        float a;                     // 辺X
        float b;                     // 辺Y
        float c;                     // a - b間の距離
        // XとYの距離
        a = enemy2D.x - reticle2D.x;
        b = enemy2D.y - reticle2D.y;
        a = a * a;                   // aの累乗
        b = b * b;                   // bの累乗
        c = a + b;                   // a + b の距離
        c = (float)Math.Sqrt(c);     // 平方根
        float ReticleRadius = 10.0f; // 照準の半径
        float r = ReticleRadius + TargetRadius;

        // マークが生成されてなかったら
        if (rockonFlg == false)
        {
            // ロックオンマークを生成
            target = (GameObject)Resources.Load("RockOn");
            target = Instantiate(target, transform.position, target.transform.rotation);
            // Canvasの子オブジェクトとして生成
            target.transform.SetParent(canvas.transform, false);
            rockonFlg = true;
        }

        // 敵に近づいたら照準を引き寄せる
        if (c <= AttractDistance)
        {
            // タグを持っていたら    ロックオンした後のオブジェクトに寄らないように
            if (this.tag == "Enemy")
            {
                //reticle.transform.position = Vector2.MoveTowards(reticle.transform.position, transform.position, AttractPower);
            }
            reticle.transform.position = Vector2.MoveTowards(reticle.transform.position, transform.position, AttractPower);


            // 当たり判定
            if (c <= r)
            {
                // Targetタグを持つオブジェクトをカウント
                GameObject[] tagObjects;
                tagObjects = GameObject.FindGameObjectsWithTag("Target");
                // ロックオンできる数以下だったら
                if (tagObjects.Length < MaxRockOn)
                {
                    // 回転
                    Transform transform = target.transform;
                    Vector3 angle = transform.localEulerAngles;
                    angle.z = 45.0f;
                    transform.localEulerAngles = angle;

                    // 色を赤に変更
                    Color color = target.GetComponent<Image>().color = Color.red; ;

                    //animeFlg = true;

                    // 当たったらタグをTargetに変える
                    this.tag = ("Target");
                }
            }
        }

        // 敵に追従させる
        if (rockonFlg == true && destroyFlg != true)
        {
            // ワールド座標をスクリーン座標に変換
            target.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        }
    }

    // ロックオンされたオブジェクトが消滅した場合マークも消す
    private void OnDestroy()
    {
        Destroy(target);
    }

    // 画面外に行ったら消える
    private void OnBecameInvisible()
    {
        // チェックボックスがtrue
        if (FrameOutDestroy == true)
        {
            Destroy(target);
            destroyFlg = true;
        }
    }

    // ロックオンしたらアニメーション開始
    //public bool RockOnAnime()
    //{
    //    return animeFlg;
    //}
}
