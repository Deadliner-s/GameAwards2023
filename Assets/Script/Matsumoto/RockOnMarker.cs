// ロックオンしたいオブジェクトに追加
using UnityEngine;

public class RockOnMarker : MonoBehaviour
{
    private GameObject target;                  // ロックオした時のマーク
    private GameObject canvas;                      // キャンバス
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

    // World Space を Screen Space Cameraにするための関数
    private RectTransform targetRect;
    private Vector2 targetPos;
    private Vector2 targetScreenPos;
    private Camera uiCamera;
    private Camera worldCamera;
    private RectTransform canvasRect;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas RockOn");     // キャンバスを指定
        //canvasGraphic = canvas.GetComponent<Graphic>().canvas;

        target = null;
        uiCamera = Camera.main;
        worldCamera = Camera.main;
        hideFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        // マークが生成されてなかったら
        if (rockonFlg == false)
        {
            // 弱点でない場合
            if (true)           // WeakPointTop == false && WeakPointBottom == false
            {
                // ロックオンマークを生成
                target = (GameObject)Resources.Load("TargetMaker");
                target = Instantiate(target, transform.position, target.transform.rotation);

                targetRect = target.GetComponent<RectTransform>();

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
                targetPos = target.transform.position;
                uiCamera = Camera.main;
                worldCamera = Camera.main;
                canvasRect = canvas.GetComponent<RectTransform>();

                // ワールド座標をスクリーン座標に変換
                targetScreenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, transform.position);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, targetScreenPos, uiCamera, out targetPos);
                targetRect.localPosition = targetPos;
            }
        }
        
       // Debug.Log(uiCamera);
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
