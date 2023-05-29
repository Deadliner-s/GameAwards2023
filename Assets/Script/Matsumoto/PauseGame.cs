using TMPro;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool pauseFlg = false;                                              // ポーズ中か

    private const int MAX_PAUSEMENU = 4;                                        // ポーズメニューの数
    private int Selected = 0;                                                   // 選択中のポーズメニュー

    [SerializeField]
    private GameObject pauseWindow;                                             // ポーズ画面のオブジェクト
    private GameObject pauseMenu;                                               // ポーズメニューのオブジェクト
    private GameObject[] text = new GameObject[MAX_PAUSEMENU];                  // テキストのオブジェクト
    private GameObject[] textJP = new GameObject[MAX_PAUSEMENU];                // 概要説明のオブジェクト

    private GameObject fade;                                                    // フェードのオブジェクト

    private bool isAnimating = false;                                           // アニメーション中か
    public float animationTime = 0.1f;                                          // アニメーションにかける時間
    public float targetScaleY = 8.0f;
    private float scaleY = 8.0f;                                                // 目標のスケール値
    private float initialScaleY;                                                // 初期のスケール値
    private float animationStartTime;                                           // アニメーション開始時間

    // Start is called before the first frame update
    void Start()
    {
        pauseFlg = false;
        Selected = 0;

        // フェードのオブジェクトを取得
        fade = GameObject.Find("Fade");

        // ポーズ画面のオブジェクトを取得
        pauseMenu = pauseWindow.transform.Find("PauseMenu").gameObject;
        // ポーズメニューのテキストを取得
        int cnt = pauseMenu.transform.childCount;
        for (int i = 0; i < cnt; i++)
        {
            text[i] = pauseMenu.transform.GetChild(i).gameObject;
        }

        // 概要説明のオブジェクトを取得
        int cntJP = pauseWindow.transform.childCount - 1;
        for (int i = 0; i < cntJP; i++)
        {
            textJP[i] = pauseWindow.transform.GetChild(i).gameObject;
        }

        text[0].GetComponent<TextMeshProUGUI>().color = Color.white;
        text[1].GetComponent<TextMeshProUGUI>().color = Color.black;
        text[2].GetComponent<TextMeshProUGUI>().color = Color.black;
        text[3].GetComponent<TextMeshProUGUI>().color = Color.black;
        textJP[0].SetActive(true);
        textJP[1].SetActive(false);
        textJP[2].SetActive(false);
        textJP[3].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fade == null)
        {
            fade = GameObject.Find("Fade");
        }

        if (Time.timeScale == 1.0f)
        {
            if (InputManager.instance.OnPause() && pauseFlg == false)
            {
                // ポーズ開始
                PauseStart();
                Selected = 0;
                SoundManager.instance.PlaySE("Decision");
            }
        }
        else if (InputManager.instance.OnBack() && pauseFlg == true)
        {
            // ポーズ終了
            PauseEndBack();
            // タイムスケールを1にする
            Time.timeScale = 1.0f;
            SoundManager.instance.PlaySE("Decision");
        }       
        else if (InputManager.instance.OnPause() && pauseFlg == true)
        {
            // ポーズ終了
            PauseEndBack();
            // タイムスケールを1にする
            Time.timeScale = 1.0f;
            SoundManager.instance.PlaySE("Decision");
        }

        if (pauseFlg == true)
        {
            // カーソル移動
            if (InputManager.instance.OnUp())
            {
                Selected--;
                SoundManager.instance.PlaySE("Select");
            }
            if (InputManager.instance.OnDown())
            {
                Selected++;
                SoundManager.instance.PlaySE("Select");
            }
            Selected = (Selected + MAX_PAUSEMENU) % MAX_PAUSEMENU;

            // 選択中のメニューの色を変える
            switch (Selected)
            {
                case 0:
                    // BACK
                    text[0].GetComponent<TextMeshProUGUI>().color = Color.white;
                    text[1].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[2].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[3].GetComponent<TextMeshProUGUI>().color = Color.black;
                    textJP[0].SetActive(true);
                    textJP[1].SetActive(false);
                    textJP[2].SetActive(false);
                    textJP[3].SetActive(false);
                    break;

                case 1:
                    // RETRY
                    text[0].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[1].GetComponent<TextMeshProUGUI>().color = Color.white;
                    text[2].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[3].GetComponent<TextMeshProUGUI>().color = Color.black;
                    textJP[0].SetActive(false);
                    textJP[1].SetActive(true);
                    textJP[2].SetActive(false);
                    textJP[3].SetActive(false);
                    break;
                case 2:
                    // RESTART
                    text[0].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[1].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[2].GetComponent<TextMeshProUGUI>().color = Color.white;
                    text[3].GetComponent<TextMeshProUGUI>().color = Color.black;
                    textJP[0].SetActive(false);
                    textJP[1].SetActive(false);
                    textJP[2].SetActive(true);
                    textJP[3].SetActive(false);
                    break;
                    
                case 3:
                    // RETURN TITLE
                    text[0].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[1].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[2].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[3].GetComponent<TextMeshProUGUI>().color = Color.white;
                    textJP[0].SetActive(false);
                    textJP[1].SetActive(false);
                    textJP[2].SetActive(false);
                    textJP[3].SetActive(true);
                    break;
            }

            if (InputManager.instance.OnSelect() && pauseFlg == true)
            {
                switch (Selected)
                {
                    case 0:
                        // BACK
                        PauseEndBack();
                        // タイムスケールを1にする
                        Time.timeScale = 1.0f;
                        SoundManager.instance.PlaySE("Decision");
                        break;
                    case 1:
                        // RETRY
                        //直前のシーンをセット
                        SceneNowBefore.instance.sceneBeforeCatch =
                          SceneNowBefore.instance.sceneNowCatch;
                        // シーン遷移
                        StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                              SceneNowBefore.instance.sceneNowCatch,
                              SceneLoadStartUnload.SCENE_NAME.E_DUMMY));
                        SoundManager.instance.PlaySE("Decision");
                        PauseEndBack();
                        // Fade.csでタイムスケールを1.0f、再生中のサウンドを止める
                        break;
                    case 2:
                        // RESTART
                        // 直前のシーンをセット
                        //SceneNowBefore.instance.sceneBeforeCatch =
                        //    SceneNowBefore.instance.sceneNowCatch;
                        //// シーン遷移
                        //StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                        //        SceneNowBefore.instance.sceneNowCatch,
                        //        SceneLoadStartUnload.SCENE_NAME.E_STAGE1));
                        //SoundManager.instance.PlaySE("Decision");
                        //PauseEndBack();
                        // Fade.csでタイムスケールを1.0f、再生中のサウンドを止める
                        break;

                    case 3:
                        // RETURN TITLE
                        // 直前のシーンをセット
                        SceneNowBefore.instance.sceneBeforeCatch =
                            SceneNowBefore.instance.sceneNowCatch;
                        // シーン遷移
                        StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                                SceneNowBefore.instance.sceneNowCatch,
                                SceneLoadStartUnload.SCENE_NAME.E_TITLE));
                        SoundManager.instance.PlaySE("Decision");
                        PauseEndBack();
                        // Fade.csでタイムスケールを1.0f、再生中のサウンドを止める
                        break;
                }
            }
        }
        // アニメーション中の処理
        if (isAnimating)
        {
            float elapsedTime = Time.unscaledTime - animationStartTime;
            float t = Mathf.Clamp01(elapsedTime / animationTime);

            // スケール変更
            float scale = Mathf.Lerp(initialScaleY, scaleY, t);
            pauseWindow.transform.localScale = new Vector3(pauseWindow.transform.localScale.x, scale, pauseMenu.transform.localScale.z);

            // アニメーション終了時の処理
            if (t >= 1.0f)
            {
                isAnimating = false;

                // ウィンドウが開き終わってから入力を受け付ける
                if (scaleY == 8.0f && pauseFlg == true)
                {
                    InputManager.instance.UI_Enable();
                }
                // ウィンドウが閉じ終わってから入力を受け付ける
                if (scaleY == 0.0f && pauseFlg == false)
                {
                    pauseWindow.SetActive(false);
                    InputManager.instance.Player_Enable();
                }
            }
        }
    }

    // ポーズの開始処理
    private void PauseStart()
    {
        pauseFlg = true;
        pauseWindow.SetActive(true);

        // サウンドの一時停止
        SoundManager.instance.PauseBGM();
        SoundManager.instance.PauseSE();
        SoundManager.instance.PauseVOICE();

        InputManager.instance.Player_Disable();

        // タイムスケールを0にする
        Time.timeScale = 0.0f;

        // アニメーション開始
        isAnimating = true;
        animationStartTime = Time.unscaledTime;
        initialScaleY = pauseWindow.transform.localScale.y;
        scaleY = targetScaleY;
    }

    // ポーズの終了処理
    private void PauseEndBack()
    {
        pauseFlg = false;

        // サウンドの再開
        SoundManager.instance.UnPauseBGM();
        SoundManager.instance.UnPauseSE();
        SoundManager.instance.UnPauseVOICE();

        InputManager.instance.UI_Disable();

        // アニメーション開始
        isAnimating = true;
        animationStartTime = Time.unscaledTime;
        initialScaleY = pauseWindow.transform.localScale.y;
        scaleY = 0.0f;
    }
}
