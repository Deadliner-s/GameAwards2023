using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2TextWriter : MonoBehaviour
{
    public Stage2UIText uitext;

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
        string A = "　　　　　　　　　　";

        uitext.DrawNameText("", " ");
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "衛星軌道砲、エネルギー充填完了まで10秒" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "9" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "8" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "7" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "6" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "5" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "4" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "3" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "2" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "1" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("司令官", "衛星軌道砲、撃て！" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "目標への着弾を確認。目標を覆うバリアフィールド、いまだ健在です。" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("司令官", "次で仕留める。衛星軌道砲、再充填を開始しろ。" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("司令官", "だが、衛星の位置がヤツに補足された可能性がある。" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("司令官", "さらに接近し、ヤツの注意を引き付けてくれ！" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "敵巨大飛行物体に接近します。さらなる攻撃に注意してください。" + A);
        yield return StartCoroutine("Skip");

        //yield return null;
    }
}
