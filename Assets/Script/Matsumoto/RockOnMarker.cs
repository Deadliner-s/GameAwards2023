// ロックオンしたいオブジェクトに追加
using UnityEngine;

public class RockOnMarker : MonoBehaviour
{
    private GameObject playerManager;           // プレイヤーマネージャー
    private GameObject bossManager;             // ボスマネージャー
    private GameObject target;                  // ロックオした時のマーク
    private GameObject canvas;                  // キャンバス
    private bool rockonFlg = false;             // ロックオンのマークが付いているか       

    [Header("敵の半径")]
    public float TargetRadius = 1.0f;           // 敵の半径

    //[Header("照準関係")]
    //[Tooltip("引き寄せられ始める距離")]
    //public float AttractDistance = 50.0f;     // 引き寄せられ始める距離
    //[Tooltip("引き寄せられる力")]
    //public float AttractPower = 10.0f;        // 引き寄せられる力

    [Header("画面外でRockOnを消す")]
    public bool FrameOutDestroy = false;        // 画面外で消すか

    [Header("弱点が隠れたら")]
    public bool WeakPointTop = false;
    public bool WeakPointBottom = false;

    private Animator anime;                     // アニメーション制御

    private bool hideFlg = false;               // 隠れたオブジェクト

    // World Space を Screen Space Cameraにするための関数
    private RectTransform targetRect;
    private Vector2 targetPos;
    private Vector2 targetScreenPos;
    private Camera uiCamera;
    private Camera worldCamera;
    private RectTransform canvasRect;

    // フェイズ
    private PhaseManager.Phase currentPhase;
    private PhaseManager.Phase nextPhase;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas RockOn");          // キャンバス
        playerManager = GameObject.Find("PlayerManager");   // プレイヤーマネージャー
        bossManager = GameObject.Find("BossManager");       // ボスマネージャー

        target = null;
        uiCamera = Camera.main;
        worldCamera = Camera.main;
        hideFlg = false;

        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();
        nextPhase = currentPhase;
    }

    // Update is called once per frame
    void Update()
    {
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();

        RaycastHit hit;    //ヒットした情報
        // 自身からカメラに向かってRayを飛ばし、条件に当てはまるならマークを生成する
        bool isHit = Physics.Linecast(transform.position, Camera.main.transform.position, out hit);
        if (currentPhase == PhaseManager.Phase.Attack_Phase && 
            playerManager.GetComponent<PlayerHp>().BreakFlag !=true && 
            bossManager.GetComponent<MainBossHp>().BreakFlag != true)
        {
            if (isHit)
            {
                if (hit.collider.gameObject.tag == "MainCamera" ||
                    hit.collider.gameObject.tag == "Player" ||
                    hit.collider.gameObject.name == "BossMissleHoming(Clone)" ||
                    hit.collider.gameObject.name == "BossMissleContena(Clone)" ||
                    hit.collider.gameObject.name == "BossMissleContenaSmall(Clone)" ||
                    hit.collider.gameObject.name == "Cylinder")
                {

                    // 一度隠れたオブジェクトの場合
                    if (hideFlg == true)
                    {
                        rockonFlg = false;
                        hideFlg = false;
                    }

                    // ロックオンマークがない場合
                    if (rockonFlg == false)
                    {
                        // ロックオンマークを生成
                        target = (GameObject)Resources.Load("TargetMaker");
                        target = Instantiate(target, transform.position, target.transform.rotation);

                        targetRect = target.GetComponent<RectTransform>();

                        // Canvasの子オブジェクトとして生成
                        target.transform.SetParent(canvas.transform, false);
                        rockonFlg = true;
                    }
                }
                else
                {
                    // 条件に当てはまらない場合はマークを消してフラグを立てる
                    if (rockonFlg == true)
                    {
                        if (target != null)
                        {
                            Destroy(target);
                        }
                        hideFlg = true;
                        this.tag = "Enemy";
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
        }
        // Debug.DrawLine(transform.position, Camera.main.transform.position, Color.red);

        // フェイズが変わったらマークを消す
        if(currentPhase != nextPhase)
        {
            nextPhase = currentPhase;
            if(currentPhase != PhaseManager.Phase.Attack_Phase)
            {
                if (target != null)
                {
                    Destroy(target);
                }
            }
        }

        // プレイヤーかボスが撃墜された場合も消す
        if (playerManager.GetComponent<PlayerHp>().BreakFlag ||
            bossManager.GetComponent<MainBossHp>().BreakFlag)
        {
            if (target != null)
            {
                Destroy(target);
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
        // 弱点の場合 複数回当たったら消すため、一度消してから生成する
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
