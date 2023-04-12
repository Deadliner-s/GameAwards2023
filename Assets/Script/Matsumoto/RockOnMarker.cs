// ロックオンしたいオブジェクトに追加
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class RockOnMarker : MonoBehaviour
{
    private GameObject target;                  // ロックオした時のマーク
    private GameObject canvas;                  // キャンバス
    private bool rockonFlg = false;             // ロックオンのマークが付いているか

    [Header("敵の半径")]
    public float TargetRadius = 1.0f;           // 敵の半径

    [Header("照準関係")]
    [Tooltip("引き寄せられ始める距離")]
    public float AttractDistance = 50.0f;       // 引き寄せられ始める距離
    [Tooltip("引き寄せられる力")]
    public float AttractPower = 10.0f;          // 引き寄せられる力

    [Header("画面外でRockOnを消す")]
    public bool FrameOutDestroy = false;        // 画面外で消すか

    [Header("弱点が隠れたら")]
    public bool WeakPointTop = false;
    public bool WeakPointBottom = false;

    private Animator anime;     // アニメーション制御

    private bool hideFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");     // キャンバスを指定
        target = null;

        hideFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        // マークが生成されてなかったら
        if (rockonFlg == false)
        {
            // 弱点でない場合
            if (WeakPointTop == false && WeakPointBottom == false)
            {
                // ロックオンマークを生成
                target = (GameObject)Resources.Load("TargetMaker");
                target = Instantiate(target, transform.position, target.transform.rotation);
                // Canvasの子オブジェクトとして生成
                target.transform.SetParent(canvas.transform, false);
                rockonFlg = true;
            }

            // 弱点の場合 Top
            else if (WeakPointTop == true && WeakPointBottom == false)
            {
                if (this.transform.position.z < 1.5f)
                {
                    // ロックオンマークを生成
                    target = (GameObject)Resources.Load("TargetMaker");
                    target = Instantiate(target, transform.position, target.transform.rotation);
                    // Canvasの子オブジェクトとして生成
                    target.transform.SetParent(canvas.transform, false);
                    rockonFlg = true;
                }
            }
            // 弱点の場合 Bottom
            else if (WeakPointTop == false && WeakPointBottom == true)
            {
                if (this.transform.position.z < 0.13f)
                {
                    // ロックオンマークを生成
                    target = (GameObject)Resources.Load("TargetMaker");
                    target = Instantiate(target, transform.position, target.transform.rotation);
                    // Canvasの子オブジェクトとして生成
                    target.transform.SetParent(canvas.transform, false);
                    rockonFlg = true;
                }

            }
        }

        if (target != null)
        {
            // 敵に追従させる
            if (rockonFlg == true)
            {
                // ワールド座標をスクリーン座標に変換
                target.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
            }
        }

        // 弱点が後ろに行ったらロックオンを消す Top
        if (WeakPointTop == true && WeakPointBottom == false)
        {
            if (this.transform.position.z > 2.5f && hideFlg == false && rockonFlg == true)
            {
                if (target != null)
                {
                    Destroy(target);
                }
                hideFlg = true;
                this.tag = "Enemy";
            }
            if (this.transform.position.z < 1.5f && hideFlg == true)
            {
                hideFlg = false;
                rockonFlg = false;
            }
        }

        // 弱点が後ろに行ったらロックオンを消す Bottom
        if (WeakPointTop == false && WeakPointBottom == true)
        {
            if (this.transform.position.z > 0.3f && hideFlg == false && rockonFlg == true)
            {
                if (target != null)
                {
                    Destroy(target);
                }
                hideFlg = true;
                this.tag = "Enemy";
            }
            if (this.transform.position.z < 0.25f && hideFlg == true)
            {
                hideFlg = false;
                rockonFlg = false;
            }
        }
    }

    // ロックオンされたオブジェクトが消滅した場合マークも消す
    private void OnDestroy()
    {
        if (target != null)
        {
            Destroy(target);
        }
    }

    // 画面外に行ったら消える
    private void OnBecameInvisible()
    {
        // チェックボックスがtrue
        if (FrameOutDestroy == true)
        {
            if (target != null)
            {
                Destroy(target);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 弱点の場合 2回当たったら消すため、一度消してから生成する
        if (collision.gameObject.tag == "PlayerBullet")
        {
            if (WeakPointTop == false && WeakPointBottom == false)
            {
                if (target != null)
                {
                    Destroy(target);
                }
            }
            if (WeakPointTop == true || WeakPointBottom == true)
            {
                if (target != null)
                {
                    Destroy(target);
                }
                rockonFlg = false;
                this.tag = "Enemy";
            }
        }
    }

    public void RockOnAnime()
    {
        if (target != null)
        {
            anime = target.GetComponent<Animator>();
            anime.SetBool("isMove", true);
        }
    }

    public bool GetRockOnFlg()
    {
        return rockonFlg;
    }
    public bool GetHideFlg()
    {
        return hideFlg;
    }
}
