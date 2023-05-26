using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiCoFade : MonoBehaviour
{
    // ミコ
    [SerializeField]
    private TextMeshProUGUI MiCo;
    // フェードオブジェクト
    [SerializeField]
    private GameObject fade;
    // スタートしたかのフラグ
    private bool bStart;

    // Start is called before the first frame update
    void Start()
    {
        bStart = false;
    }

    private void Update()
    {
        if (!bStart)
        {
            StartCoroutine(MiCoFadeOut());
            bStart = true;
        }
    }


    // Update is called once per frame
    IEnumerator MiCoFadeOut()
    {
        // フェード後の色を設定（黒）★変更可
        MiCo.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // フェードアウトにかかる時間（秒）★変更可
        const float MiCo_time = 3.0f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 50;

        // ウェイト時間算出
        float wait_time = MiCo_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha < 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = MiCo.color;
            new_color.a = alpha / 255.0f;
            MiCo.color = new_color;
        }
        Color color = MiCo.color;
        color.a = 1.0f;
        MiCo.color = color;

        if (MiCo.color.a == 1.0f)
        {
            // 待ち時間
            yield return new WaitForSeconds(3.0f);

            // 直前のシーンに入れる
            SceneNowBefore.instance.sceneBeforeCatch = SceneNowBefore.instance.sceneNowCatch;
            // シーン遷移
            StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext_time(
                            SceneLoadStartUnload.SCENE_NAME.E_RESULT_COMPLETED,
                            SceneLoadStartUnload.SCENE_NAME.E_TITLE,
                            3.0f));
        }
    }
}
