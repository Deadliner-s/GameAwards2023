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
        uitext.DrawNameText("≪ 司令官 ≫", "衛星軌道砲、撃て！");
        yield return new WaitForSeconds(5.0f);

        SoundManager.instance.PlayVOICE("1-12");
        uitext.DrawNameText("≪ AI ≫", "目標への着弾を確認。目標を覆うバリアフィールドいまだ健在です。");
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
