using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1TextWriter : MonoBehaviour
{
    public Stage1UIText uitext;

    public GameObject Window;
    public GameObject Name;
    public GameObject Text;

    private float Max_Height = 2.756843f;

    // Start is called before the first frame update
    void Start()
    {
        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);

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
        StartCoroutine("WindowScaleUp");

        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("0-1");
        uitext.DrawNameText("≪ 司令官 ≫", "改めて作戦内容を伝える");
        yield return new WaitForSeconds(3.0f);

        SoundManager.instance.PlayVOICE("0-2");
        uitext.DrawNameText("≪ 司令官 ≫", "強力なビーム兵器「衛星軌道砲」により、敵巨大\n飛行物体を撃墜するのが本ミッションの目的である。");
        yield return new WaitForSeconds(8.0f);

        SoundManager.instance.PlayVOICE("0-3");
        uitext.DrawNameText("≪ 司令官 ≫", "「衛星軌道砲」を当てるためには、正確な照準と\nエネルギー充填までヤツに気づかれない必要がある。");
        yield return new WaitForSeconds(8.0f);

        SoundManager.instance.PlayVOICE("0-4");
        uitext.DrawNameText("≪ 司令官 ≫", "ヤツに接近しての照準補佐と、注意を引き\n続けてもらうことが君に課せられた任務だ。");
        yield return new WaitForSeconds(7.0f);

        SoundManager.instance.PlayVOICE("0-5");
        uitext.DrawNameText("≪ 司令官 ≫", "唯一、それを可能にするのが君の搭乗\nしている「ブルーアサルト」である。");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("0-6");
        uitext.DrawNameText("≪ 司令官 ≫", "苛烈な攻撃が予想されるが、音速戦闘とバリアフィールドを\n搭載するその機体であれば切り抜けることが可能なはずだ。");
        yield return new WaitForSeconds(9.5f);

        SoundManager.instance.PlayVOICE("0-7");
        uitext.DrawNameText("≪ 司令官 ≫", "ヤツの注意をひくため、一秒でも長く戦闘を継続せよ。");
        yield return new WaitForSeconds(5.5f);

        SoundManager.instance.PlayVOICE("0-8");
        uitext.DrawNameText("≪ 司令官 ≫", "以上だ。健闘を祈る。");
        yield return new WaitForSeconds(3.0f);

        SoundManager.instance.PlayVOICE("0-9");
        uitext.DrawNameText("≪ AI ≫", "まもなく敵巨大飛行物体の攻撃圏内\nに突入します。注意してください。");
        yield return new WaitForSeconds(6.0f);

        StartCoroutine("WindowScaleDown");
        //yield return null;
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
