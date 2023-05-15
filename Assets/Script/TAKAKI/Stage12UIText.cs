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

    void Start()
    {
        nextPhase = currntPhase;
        StartCoroutine(A());
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

    IEnumerator A()
    {
        while (true)
        {
            yield return null;
            currntPhase = PhaseManager.instance.GetPhase();
            if (nextPhase != currntPhase)
            {
                FlgA = true;
                FlgB = true;
                //AddTime = 0.0f;

                nextPhase = currntPhase;
            }
            if (currntPhase == PhaseManager.Phase.Normal_Phase)
            {


                //SeFlg = false;
            }
            else if (currntPhase == PhaseManager.Phase.Speed_Phase)
            {
                AddTime = 0.0f;
                // ハイスピードフェイズ
                if (FlgA)
                {
                    Window.SetActive(true);
                    Name.SetActive(true);
                    Text.SetActive(true);
                    DrawNameText("AI", "攻撃兵装へのエネルギーをカット。 メインエンジンにエネルギーを転換。回避行動に専念して下さい。");
                    FlgA = false;
                }

                while (FlgB)
                {
                    AddTime += Time.deltaTime;
                    Debug.Log(AddTime);
                    yield return null;
                    if (AddTime >= 5.0f)
                    {
                        Name.SetActive(false);
                        Text.SetActive(false);
                        Window.SetActive(false);
                        FlgB = false;
                        break;
                    }
                }
            }

            else if (currntPhase == PhaseManager.Phase.Attack_Phase)
            {
                AddTime = 0.0f;
                // アタックフェイズ
                if (FlgA)
                {
                    Window.SetActive(true);
                    Name.SetActive(true);
                    Text.SetActive(true);
                    DrawNameText("AI", "敵内部から、多数の熱源反応を確認。飽和攻撃が予測されます。注意してください。");
                    FlgA = false;
                }
                while (FlgB)
                {
                    AddTime += Time.deltaTime;
                    Debug.Log(AddTime);
                    yield return null;
                    if (AddTime >= 5.0f)
                    {
                        Name.SetActive(false);
                        Text.SetActive(false);
                        Window.SetActive(false);
                        FlgB = false;
                        break;
                    }
                }
            }

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
}