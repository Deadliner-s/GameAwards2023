using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIText : MonoBehaviour
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
    
    private float Max_Height = 1.837893f;
    float WarningActiveTime = 33.0f;

    void Start()
    {
        AddTime = 0.0f;

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);

        nextPhase = currntPhase;
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
        if (currntPhase == PhaseManager.Phase.Normal_Phase)
        {

        }
        // アタックフェイズ
        else if (currntPhase == PhaseManager.Phase.Attack_Phase)
        {
            if (FlgA)
            {
                // ウィンドウの開始
                StartCoroutine("AttackMessageUI");
                FlgA = false;
            }
        }
        else if (currntPhase == PhaseManager.Phase.Speed_Phase)
        {

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
    
    // ゲーム中のAIセリフ用
    IEnumerator AttackMessageUI()
    {
        // ウィンドウ表示
        StartCoroutine("WindowScaleUp");
        // ボイス再生,テキスト設定,表示
        SoundManager.instance.PlayVOICE("4-3");
        DrawNameText("≪ AI ≫", "敵、装甲温度上昇。冷却状態への移行を確認。");
        yield return new WaitForSeconds(5.0f);
        SoundManager.instance.PlayVOICE("4-4");
        DrawNameText("≪ AI ≫", "攻撃兵装にエネルギーを接続。\n露出した冷却装置を攻撃して下さい。");
        yield return new WaitForSeconds(6.0f);
        // ウィンドウ非表示
        StartCoroutine("WindowScaleDown");

        // アタックフェイズの間待機
        yield return new WaitForSeconds(WarningActiveTime - 5.0f - 6.0f);

        StartCoroutine("WindowScaleUp");
        // ボイス再生,テキスト設定,表示
        SoundManager.instance.PlayVOICE("4-1");
        DrawNameText("≪ AI ≫", "敵内部から、多数の熱源反応を確認。\n飽和攻撃が予測されます。");
        yield return new WaitForSeconds(6.0f);
        SoundManager.instance.PlayVOICE("4-2");
        DrawNameText("≪ AI ≫", "攻撃兵装へのエネルギーをカット。メインエンジンに\nエネルギーを転換。回避行動に専念して下さい。");
        yield return new WaitForSeconds(8.0f);
        // ウィンドウ非表示
        StartCoroutine("WindowScaleDown");
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