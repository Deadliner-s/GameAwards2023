using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1TextWriter : MonoBehaviour
{
    public Stage1UIText uitext;

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

        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("0-1");
        uitext.DrawNameText("≪ 司令官 ≫", "改めて作戦内容を伝える");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("≪ 司令官 ≫", "強力なビーム兵器「衛星軌道砲」により、敵巨大飛行物体を\n撃墜するのが本ミッションの目的である。");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("≪ 司令官 ≫", "「衛星軌道砲」を当てるためには、正確な照準とエネルギー充填までヤツに気づかれない必要がある。");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("≪ 司令官 ≫", "ヤツに接近しての照準補佐と、注意を引き続けてもらうことが\n君に課せられた任務だ。");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("≪ 司令官 ≫", "唯一、それを可能にするのが君の搭乗している\n「ブルーアサルト」である。");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("≪ 司令官 ≫", "苛烈な攻撃が予想されるが、音速戦闘とバリアフィールドを搭載する\nその機体であれば切り抜けることが可能なはずだ。");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("≪ 司令官 ≫", "ヤツの注意をひくため、一秒でも長く戦闘を継続せよ。");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("≪ 司令官 ≫", "以上だ。健闘を祈る。");
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("≪ AI ≫", "まもなく敵巨大飛行物体の攻撃圏内に突入します。注意してください");
        yield return new WaitForSeconds(3.0f);

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);
        //yield return null;
    }
}
