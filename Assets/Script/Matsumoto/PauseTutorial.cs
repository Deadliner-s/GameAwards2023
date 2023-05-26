using UnityEngine;

public class PauseTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject TutorialWindow;      // 操作説明画像
    [SerializeField]
    private GameObject PushWindow;
    public float startDisplayTime = 1.0f;   // 操作説明画像を表示するまでの時間
    public float targetScaleY;              // 目標のスケール
    public float animationDuration = 1.0f;  // アニメーションの時間

    private　bool pauseFlg = false;         // フラグ
    private float time = 0.0f;              // 経過時間

    [SerializeField]
    private float changetimer = 0.0f;
    [SerializeField]
    private float changetime = 0.7f;
    private bool pushActive = true;

    private float initialScaleY;            // 初期のスケール
    private float elapsedTime = 0.0f;       // 経過時間

    private bool isOpened = false;
    private bool startClose = false;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        pauseFlg = false;
        isOpened = false;
        startClose = false;
        time = 0.0f;
        changetimer = 0.0f;
        elapsedTime = 0.0f;
        pushActive = true;

        // 初期のスケールを保存
        initialScaleY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // 時間更新
        time += Time.deltaTime;
        // 時間が経過したら画像を表示 & 一時停止
        if (time > startDisplayTime && pauseFlg == false)
        {
            TutorialWindow.SetActive(true);
            PushWindow.SetActive(true);
            pauseFlg = true;
            Time.timeScale = 0.0f;
        }

        // 一時停止中
        if (pauseFlg == true)
        {
            if (elapsedTime < animationDuration && isOpened == false)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsedTime / animationDuration);
                // チュートリアルウィンドウ
                Vector3 newScale = TutorialWindow.transform.localScale;
                newScale.y = Mathf.Lerp(initialScaleY, targetScaleY, t);
                TutorialWindow.transform.localScale = newScale;
                // プッシュウィンドウ
                Vector3 newScale1 = PushWindow.transform.localScale;
                newScale1.y = Mathf.Lerp(initialScaleY, targetScaleY, t);
                PushWindow.transform.localScale = newScale1;
            }
            // ウィンドウが開き終わってから入力を受け付ける
            if (TutorialWindow.transform.localScale.y == targetScaleY && isOpened == false)
            {
                InputManager.instance.UI_Enable();
                isOpened = true;
            }

            // プッシュボタンパカパカする
            if (isOpened == true && startClose == false)
            {
                changetimer += Time.unscaledDeltaTime;
                if (changetimer >= changetime)
                {
                    // ウィンドウ表示を逆にする
                    if (pushActive)
                    {
                        PushWindow.SetActive(false);
                        pushActive = false;
                        changetimer = 0.0f;
                    }
                    else
                    {
                        PushWindow.SetActive(true);
                        pushActive = true;
                        changetimer = 0.0f;
                    }
                }
            }

            // ボタンが押されたらウィンドウを閉じる
            if (InputManager.instance.OnSelect()  && isOpened == true && startClose == false)
            {
                startClose = true;
                elapsedTime = 0.0f;
                InputManager.instance.UI_Disable();
            }

            // ウィンドウが閉じるアニメーション
            if (startClose == true)
            {
                if (elapsedTime < animationDuration)
                {
                    elapsedTime += Time.unscaledDeltaTime;
                    float t = Mathf.Clamp01(elapsedTime / animationDuration);
                    // チュートリアルウィンドウ
                    Vector3 newScale = TutorialWindow.transform.localScale;
                    newScale.y = Mathf.Lerp(targetScaleY, 0.0f, t);
                    TutorialWindow.transform.localScale = newScale;
                    // プッシュウィンドウ
                    Vector3 newScale1 = PushWindow.transform.localScale;
                    newScale1.y = Mathf.Lerp(targetScaleY, 0.0f, t);
                    PushWindow.transform.localScale = newScale1;
                }
            }
            // ウィンドウが閉じたら一時停止解除
            if (TutorialWindow.transform.localScale.y == 0.0f && startClose == true)
            {
                Time.timeScale = 1.0f;
                TutorialWindow.SetActive(false);
                PushWindow.SetActive(false);
                enabled = false;
            }
        }
    }

}
