using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3TextWriter : MonoBehaviour
{
    public Stage3UIText uitext;

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

        uitext.DrawNameText("AI", "衛星軌道砲、エネルギー充填完了まで10秒" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "9" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "8" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "7" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "6" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "5" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "4" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "3" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "2" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("AI", "1" + A);
        yield return new WaitForSeconds(1.0f);

        uitext.DrawNameText("司令官", "今度こそだ。衛星軌道砲、撃て！" + A);
        yield return new WaitForSeconds(2.0f);

        uitext.DrawNameText("AI", "目標へ着弾。敵巨大飛行物体のバリアフィールド破壊を確認しました。" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("司令官", "これでも撃墜できんか。だが次で" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "敵巨大飛行物体、エネルギーの急激な上昇を検出" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("司令官", "何？" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "予想攻撃目標、衛星軌道砲です。" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("司令官", "衛星軌道砲がやられた。だが、作戦終了は認められない。" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("司令官", "ヤツのバリアフィールドが破壊できた今なら、ブルーアサルトの\nミサイルによる破壊が可能なはずだ。" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("司令官", "さらに接近し、ヤツを直接攻撃してくれ" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("司令官", "君に人類の命運を託す。　……頼んだぞ。" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "敵巨大飛行物体に最接近します。さらなる攻撃が予想されます。" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "予測される作戦成功確率、2％。" + A);
        yield return new WaitForSeconds(3.0f);

        uitext.DrawNameText("AI", "アナタとワタシなら、できます。生きて帰りましょう。" + A);
        yield return new WaitForSeconds(3.0f);

        Window.SetActive(false);
        Name.SetActive(false);
        Text.SetActive(false);
        //yield return null;
    }
}
