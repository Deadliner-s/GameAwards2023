using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4TextWriter : MonoBehaviour
{
    public Stage4UIText uitext;

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
        yield return new WaitForSeconds(7.0f);

        StartCoroutine("WindowScaleUp");

        yield return new WaitForSeconds(1.0f);

        SoundManager.instance.PlayVOICE("3-1");
        uitext.DrawNameText("≪ AI ≫", "敵巨大飛行物体の破壊を確認。");
        yield return new WaitForSeconds(3.0f);

        SoundManager.instance.PlayVOICE("3-2");
        uitext.DrawNameText("≪ 司令官 ≫", "無事か！ ");
        yield return new WaitForSeconds(1.5f);

        SoundManager.instance.PlayVOICE("3-3");
        uitext.DrawNameText("≪ 司令官 ≫", "よくやった。お前は本物の英雄だ！");
        yield return new WaitForSeconds(5.5f);

        SoundManager.instance.PlayVOICE("3-4");
        uitext.DrawNameText("≪ AI ≫", "作戦終了の承認、確認しました。帰還しましょう。");
        yield return new WaitForSeconds(5.0f);

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
