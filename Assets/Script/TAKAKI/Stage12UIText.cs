using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage12UIText : MonoBehaviour
{
    // nameText:喋っている人の名前
    // talkText:喋っている内容やナレーション
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI talkText;

    public bool playing = false;
    public float textSpeed = 0.1f;

    private PhaseManager.Phase currntPhase;
    private PhaseManager.Phase nextPhase;

    public GameObject Window;
    public GameObject Name;
    public GameObject Text;
    
    float AddTime = 0.0f;

    bool FlgA = true;
    bool FlgB = true;
    bool FlgFinish = false;

    private float Max_Height = 1.837893f;
    float MessageWindowActiveTime = 8.0f;
    float WarningActiveTime = 13.0f;
    int Attacknum = 0;


    void Start()
    {
        AddTime = 0.0f;
        FlgFinish = false;
        Attacknum = 0;

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);

        nextPhase = currntPhase;
        //StartCoroutine(A());
    }

    void Update()
    {
        AddTime += Time.deltaTime;

        // 現在のフェイズ確認
        currntPhase = PhaseManager.instance.GetPhase();
        // フェイズ変更時
        if (nextPhase != currntPhase)
        {
            // フラグリセット
            FlgA = true;
            nextPhase = currntPhase;
        }
        // ノーマルフェイズ
        if (currntPhase == PhaseManager.Phase.Normal_Phase && FlgFinish == false)
        {

        }
        // アタックフェイズ
        else if (currntPhase == PhaseManager.Phase.Attack_Phase && FlgFinish == false && Attacknum < 3)
        {
            if (FlgA)
            {
                // ウィンドウの開始
                StartCoroutine("AttackMessageUI");
                FlgA = false;
                Attacknum++;
            }
        }
        else if (currntPhase == PhaseManager.Phase.Speed_Phase && FlgFinish == false)
        {

        }

        if (AddTime >= 150.0f)
        {
            if (FlgB)
            {
                // カウントダウンの開始
                StartCoroutine("CountDownUI");
                FlgFinish = true;
                FlgB = false;
            }
        }
    }

    // クリックで次のページを表示させるための関数
    public bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

    // ナレーション用のテキストを生成する関数
    public void DrawText(string text)
    {
        nameText.text = "";
        StartCoroutine("CoDrawText", text);
    }
    // 通常会話用のテキストを生成する関数
    public void DrawNameText(string name, string text)
    {
        nameText.text = name;
        StartCoroutine("CoDrawText", text);
    }

    //IEnumerator A()
    //{
    //    while (true)
    //    {
    //        yield return null;
            
    //    }
    //}

    // ゲーム中のAIセリフ用
    IEnumerator AttackMessageUI()
    {
        // ウィンドウ表示
        StartCoroutine("WindowScaleUp");
        // ボイス再生
        SoundManager.instance.PlayVOICE("EX-1");
        // テキスト設定
        DrawNameText("≪ AI ≫", "敵内部から、多数の熱源反応を確認。\n飽和攻撃が予測されます。注意してください。");
        // ウィンドウ表示時間
        yield return new WaitForSeconds(MessageWindowActiveTime);
        // ウィンドウ非表示
        StartCoroutine("WindowScaleDown");

        // アタックフェイズの間待機
        yield return new WaitForSeconds(WarningActiveTime - MessageWindowActiveTime);

        StartCoroutine("WindowScaleUp");
        // ボイス再生
        SoundManager.instance.PlayVOICE("4-2");
        // テキスト設定
        DrawNameText("≪ AI ≫", "攻撃兵装へのエネルギーをカット。 メインエンジンに\nエネルギーを転換。回避行動に専念して下さい。");
        // ウィンドウ表示時間
        yield return new WaitForSeconds(MessageWindowActiveTime);
        // ウィンドウ非表示
        StartCoroutine("WindowScaleDown");
    }

    IEnumerator CountDownUI()
    {
        // ウィンドウ表示
        StartCoroutine("WindowScaleUp");

        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-1");
        DrawNameText("≪ AI ≫", "衛星軌道砲、エネルギー充填完了まで10秒");
        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlayVOICE("1-2");
        DrawNameText("≪ AI ≫", " 9");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-3");
        DrawNameText("≪ AI ≫", " 8");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-4");
        DrawNameText("≪ AI ≫", " 7");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-5");
        DrawNameText("≪ AI ≫", " 6");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-6");
        DrawNameText("≪ AI ≫", " 5");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-7");
        DrawNameText("≪ AI ≫", " 4");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-8");
        DrawNameText("≪ AI ≫", " 3");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-9");
        DrawNameText("≪ AI ≫", " 2");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-10");
        DrawNameText("≪ AI ≫", " 1");
        yield return new WaitForSeconds(1.0f);

        if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE1)
        {
            SoundManager.instance.PlayVOICE("1-11");
            DrawNameText("≪ 司令官 ≫", "衛星軌道砲、撃て！");
        }
        else if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE2)
        {
            SoundManager.instance.PlayVOICE("2-11");
            DrawNameText("≪ 司令官 ≫", "今度こそだ。衛星軌道砲、撃て！");
        }

    }

    // テキストがヌルヌル出てくるためのコルーチン
    IEnumerator CoDrawText(string text)
    {
        playing = true;
        float time = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // クリックされると一気に表示
            //if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed);
            if (len > text.Length) break;
            talkText.text = text.Substring(0, len);
        }
        talkText.text = text;
        yield return 0;
        playing = false;
    }
    // ウィンドウの拡大
    IEnumerator WindowScaleUp()
    {
        Window.SetActive(true);
        Window.transform.localScale = new Vector3(
            Window.transform.localScale.x,
            0.0f,
            Window.transform.localScale.z
            );
        for (float i = 1; i < 2; i += 0.1f)
        {
            Vector3 scale = Window.transform.localScale;
            scale.y += Max_Height * 0.1f;
            Window.transform.localScale = scale;
            yield return new WaitForSeconds(0.01f);
        }
        Name.SetActive(true);
        Text.SetActive(true);
    }
    // ウィンドウの縮小
    IEnumerator WindowScaleDown()
    {
        DrawNameText("", "");
        Name.SetActive(false);
        Text.SetActive(false);
        for (float i = 1; i < 2; i += 0.1f)
        {
            Vector3 scale = Window.transform.localScale;
            scale.y -= Max_Height * 0.1f;
            Window.transform.localScale = scale;
            yield return new WaitForSeconds(0.01f);
        }
        Window.SetActive(false);
    }
}