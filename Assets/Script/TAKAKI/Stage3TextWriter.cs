using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3TextWriter : MonoBehaviour
{
    public Stage3UIText uitext;

    public GameObject Window;
    public GameObject Name;
    public GameObject Text;

    private float Max_Height = 2.756843f;

    // Start is called before the first frame update
    void Start()
    {
        //Window.SetActive(false);
        //Name.SetActive(false);
        //Text.SetActive(false);

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
        uitext.DrawNameText("≪ 司令官 ≫", "今度こそだ。衛星軌道砲、撃て！");
        yield return new WaitForSeconds(7.0f);

        SoundManager.instance.PlayVOICE("2-12");
        uitext.DrawNameText("≪ AI ≫", "目標へ着弾。敵巨大飛行物体のバリアフィールド破壊を確認しました。");
        yield return new WaitForSeconds(7.0f);

        SoundManager.instance.PlayVOICE("2-13");
        uitext.DrawNameText("≪ 司令官 ≫", "これでも撃墜できんか。だが次で");
        yield return new WaitForSeconds(4.0f);

        SoundManager.instance.PlayVOICE("2-14");
        uitext.DrawNameText("≪ AI ≫", "敵巨大飛行物体、エネルギーの急激な上昇を確認。");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-15");
        uitext.DrawNameText("≪ 司令官 ≫", "何？");
        yield return new WaitForSeconds(2.0f);

        SoundManager.instance.PlayVOICE("2-16");
        uitext.DrawNameText("≪ AI ≫", "目標予測……攻撃目標は、衛星軌道砲です。");
        yield return new WaitForSeconds(12.0f);

        SoundManager.instance.PlayVOICE("2-17");
        uitext.DrawNameText("≪ 司令官 ≫", "衛星軌道砲がやられた。だが、作戦終了は認められない。");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-18");
        uitext.DrawNameText("≪ 司令官 ≫", "ヤツのバリアフィールドが破壊できた今なら、\nブルーアサルトのミサイルによる破壊が可能なはずだ。");
        yield return new WaitForSeconds(7.0f);

        SoundManager.instance.PlayVOICE("2-19");
        uitext.DrawNameText("≪ 司令官 ≫", "さらに接近し、ヤツを直接攻撃してくれ。");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-20");
        uitext.DrawNameText("≪ 司令官 ≫", "君に人類の命運を託す。　……頼んだぞ。");
        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlayVOICE("2-21");
        uitext.DrawNameText("≪ AI ≫", "敵巨大飛行物体に最接近します。\nさらなる攻撃に注意してください。");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-22");
        uitext.DrawNameText("≪ AI ≫", "予測される作戦成功確率、2％。");
        yield return new WaitForSeconds(6.0f);

        SoundManager.instance.PlayVOICE("2-tuika");
        uitext.DrawNameText("≪ AI ≫", "………ですが、");
        yield return new WaitForSeconds(3.0f);

        SoundManager.instance.PlayVOICE("2-23");
        uitext.DrawNameText("≪ AI ≫", "アナタとワタシなら、できます。\n生きて帰りましょう。");
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
