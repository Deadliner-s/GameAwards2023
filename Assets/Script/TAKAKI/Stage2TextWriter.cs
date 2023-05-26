using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2TextWriter : MonoBehaviour
{
    public Stage2UIText uitext;

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

        SoundManager.instance.PlayVOICE("1-1");
        uitext.DrawNameText("≪ AI ≫", "衛星軌道砲、エネルギー充填完了まで10秒");
        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlayVOICE("1-2");
        uitext.DrawNameText("≪ AI ≫", " 9");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-3");
        uitext.DrawNameText("≪ AI ≫", " 8");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-4");
        uitext.DrawNameText("≪ AI ≫", " 7");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-5");
        uitext.DrawNameText("≪ AI ≫", " 6");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-6");
        uitext.DrawNameText("≪ AI ≫", " 5");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-7");
        uitext.DrawNameText("≪ AI ≫", " 4");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-8");
        uitext.DrawNameText("≪ AI ≫", " 3");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-9");
        uitext.DrawNameText("≪ AI ≫", " 2");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-10");
        uitext.DrawNameText("≪ AI ≫", " 1");
        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("1-11");
        uitext.DrawNameText("≪ 司令官 ≫", "衛星軌道砲、撃て！");
        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlayVOICE("1-12");
        uitext.DrawNameText("≪ AI ≫", "目標への着弾を確認。目標を覆う\nバリアフィールドいまだ健在です。");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("1-13");
        uitext.DrawNameText("≪ 司令官 ≫", "次で仕留める。衛星軌道砲、再充填を開始しろ。");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("1-14");
        uitext.DrawNameText("≪ 司令官 ≫", "衛星の位置がヤツに補足された可能性がある。");
        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlayVOICE("1-15");
        uitext.DrawNameText("≪ 司令官 ≫", "さらに接近し、ヤツの注意を引き付けてくれ！");
        yield return new WaitForSeconds(3.5f);
        
        SoundManager.instance.PlayVOICE("1-16");
        uitext.DrawNameText("≪ AI ≫", "敵巨大飛行物体に接近します。\nさらなる攻撃に注意してください。");
        yield return new WaitForSeconds(6.0f);

        StartCoroutine("WindowScaleDown");

        // SceneMoveManagerをタグ検索
        GameObject obj = GameObject.FindGameObjectWithTag("NowEventSceneSet");
        // シーンの開始
        obj.GetComponent<SceneEventMove>().bTextEnd = true;
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
