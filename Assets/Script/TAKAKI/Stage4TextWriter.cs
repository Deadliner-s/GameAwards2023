using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4TextWriter : MonoBehaviour
{
    public Stage4UIText uitext;

    public GameObject Window;
    public GameObject Name;
    public GameObject Text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Cotest");
    }

    // クリック待ちのコルーチン
    IEnumerator Skip()
    {
        while (uitext.playing) yield return 0;
        //while (!uitext.IsClicked()) yield return 0;
    }

    // 文章を表示させるコルーチン
    IEnumerator Cotest()
    {
        Window.SetActive(true);
        Name.SetActive(true);
        Text.SetActive(true);

        string A = "　　　　　　　　　　";

        uitext.DrawNameText("", " ");
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "敵巨大飛行物体の破壊を確認。" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("司令官", "無事か！ " + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("司令官", "よくやった。お前は本物の 英雄だ" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "作戦終了の承認、確認しました。帰還しましょう。" + A);
        yield return new WaitForSeconds(3.0f);

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);
        //yield return null;
    }
}
