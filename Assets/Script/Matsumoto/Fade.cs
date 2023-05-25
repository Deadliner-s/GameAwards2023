using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ////フェードイン
        //if(SceneManager.GetActiveScene().name == "Prologue")
        //{
        //    StartCoroutine("Color_FadeIn");
        //}
        //if(SceneManager.GetActiveScene().name == "Stage2Event")
        //{
        //    StartCoroutine("Color_FadeIn");
        //}
        //if(SceneManager.GetActiveScene().name == "Stage3Event")
        //{
        //    StartCoroutine("Color_FadeIn");
        //}
        //if (SceneManager.GetActiveScene().name == "Title")
        //{
        //    StartCoroutine("Color_FadeIn");
        //}
        if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_TITLE)
        {
            StartCoroutine("Color_FadeIn");
        }
        if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE)
        {
            StartCoroutine("Color_FadeIn");
        }
        // コンティニューの場合
        if(GManager.instance.SetContinueFlg() == true)
        {
            if(SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT)
            {
                StartCoroutine("Color_FadeIn");
            }
            if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT)
            {
                StartCoroutine("Color_FadeIn");
            }
        }

    }

    public IEnumerator Color_FadeIn()
    {
        // 画面をフェードインさせるコールチン
        // 前提：画面を覆うPanelにアタッチしている

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        // フェード元の色を設定（黒）★変更可
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (255.0f / 255.0f));

        // フェードインにかかる時間（秒）★変更可
        const float fade_time = 1.0f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 50;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ下げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
    }
    
    public IEnumerator Color_FadeOut()
    {
        // 画面をフェードインさせるコールチン
        // 前提：画面を覆うPanelにアタッチしている

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        // フェード後の色を設定（黒）★変更可
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // フェードアウトにかかる時間（秒）★変更可
        const float fade_time = 1.0f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 50;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha < 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        Color color = fade.color;
        color.a = 1.0f;
        fade.color = color;

        if (fade.color.a == 1.0f)
        {

            // シーン遷移
            //SceneManager.LoadScene(nextScene);
            // SceneLoadManagerをタグ検索
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // シーンの開始
            obj.GetComponent<SceneMoveManager>().SceneStartUnload();
            
        }
    }

    public IEnumerator Color_FadeOut_Title(SceneLoadStartUnload.SCENE_NAME scene_name)
    {
        // 画面をフェードインさせるコールチン
        // 前提：画面を覆うPanelにアタッチしている

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        // フェード後の色を設定（黒）★変更可
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // フェードアウトにかかる時間（秒）★変更可
        const float fade_time = 1.0f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 50;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha < 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        Color color = fade.color;
        color.a = 1.0f;
        fade.color = color;

        if (fade.color.a == 1.0f)
        {
            // シーン遷移
            SceneAccessSearch.SceneAccessCatchLoad(scene_name);
            SceneAccessSearch.SceneAccessCatchStart();
            SceneAccessSearch.SceneAccessCatchUnload(SceneLoadStartUnload.SCENE_NAME.E_TITLE);
        }
    }

    public IEnumerator Color_FadeOut_GameOver(
        SceneLoadStartUnload.SCENE_NAME scene_now)
    {
        // 画面をフェードインさせるコールチン
        // 前提：画面を覆うPanelにアタッチしている

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        // フェード後の色を設定（黒）★変更可
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // フェードアウトにかかる時間（秒）★変更可
        const float fade_time = 1.0f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 50;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha < 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        Color color = fade.color;
        color.a = 1.0f;
        fade.color = color;

        if (fade.color.a == 1.0f)
        {
            // シーン遷移
            SceneAccessSearch.SceneAccessCatchLoad(SceneLoadStartUnload.SCENE_NAME.E_RESULT_FAILED);
            SceneAccessSearch.SceneAccessCatchStart();
            SceneAccessSearch.SceneAccessCatchUnload(scene_now);
        }
    }

    public IEnumerator Color_FadeOut_NowNext(
        SceneLoadStartUnload.SCENE_NAME scene_now,
        SceneLoadStartUnload.SCENE_NAME scene_next)
    {
        // 画面をフェードインさせるコールチン
        // 前提：画面を覆うPanelにアタッチしている

        // 色を変えるゲームオブジェクトからImageコンポーネントを取得
        Image fade = GetComponent<Image>();

        // フェード後の色を設定（黒）★変更可
        fade.color = new Color((0.0f / 255.0f), (0.0f / 255.0f), (0.0f / 0.0f), (0.0f / 255.0f));

        // フェードアウトにかかる時間（秒）★変更可
        const float fade_time = 1.0f;

        // ループ回数（0はエラー）★変更可
        const int loop_count = 50;

        // ウェイト時間算出
        float wait_time = fade_time / loop_count;

        // 色の間隔を算出
        float alpha_interval = 255.0f / loop_count;

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha < 255.0f; alpha += alpha_interval)
        {
            // 待ち時間
            yield return new WaitForSeconds(wait_time);

            // Alpha値を少しずつ上げる
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        Color color = fade.color;
        color.a = 1.0f;
        fade.color = color;

        if (fade.color.a == 1.0f)
        {
            // シーン遷移
            SceneAccessSearch.SceneAccessCatchLoad(scene_next);
            SceneAccessSearch.SceneAccessCatchStart();
            SceneAccessSearch.SceneAccessCatchUnload(scene_now);
        }
    }
}
