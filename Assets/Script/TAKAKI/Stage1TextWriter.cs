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
        string A = "　　　　　　　　　　";
        uitext.DrawNameText("司令官", "改めて作戦内容を伝える"+ A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("司令官", "強力なビーム兵器「衛星軌道砲」により、敵巨大飛行物体を撃墜するのが本ミッションの目的である。" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("司令官", "「衛星軌道砲」を当てるためには、正確な照準とエネルギー充填までヤツに気づかれない必要がある。" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("司令官", "ヤツに接近しての照準補佐と、注意を引き続けてもらうことが君に課せられた任務だ。" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("司令官", "唯一、それを可能にするのが君の搭乗している「ブルーアサルト」である。" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("司令官", "苛烈な攻撃が予想されるが、音速戦闘とバリアフィールドを搭載するその機体であれば切り抜けることが可能なはずだ。" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("司令官", "ヤツの注意をひくため、一秒でも長く戦闘を継続せよ。" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("司令官", "以上だ。健闘を祈る。" + A);
        yield return StartCoroutine("Skip");

        uitext.DrawNameText("AI", "まもなく敵巨大飛行物体の攻撃圏内に突入します。注意してください" + A);
        yield return StartCoroutine("Skip");
        //yield return null;
    }
}
