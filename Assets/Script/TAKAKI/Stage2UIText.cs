using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage2UIText : MonoBehaviour
{
    // nameText:喋っている人の名前
    // talkText:喋っている内容やナレーション
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI talkText;

    public bool playing = false;
    public float textSpeed = 0.1f;

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
            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed);
            if (len > text.Length) break;
            talkText.text = text.Substring(0, len);
        }
        talkText.text = text;
        yield return 0;
        playing = false;
    }
}