using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1TextWriter : MonoBehaviour
{
    public Stage1UIText uitext;

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
        uitext.DrawNameText("AI", "人が話すのならこんな感じ");
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("伊東", "人が話すのならこんな感じ");
        yield return StartCoroutine("Skip");
        //yield return null;
    }
}
