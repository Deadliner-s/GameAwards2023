using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiCoTitle : MonoBehaviour
{
    // フェードオブジェクト
    [SerializeField]
    private GameObject fade;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneNowBefore.instance.sceneBeforeCatch == SceneLoadStartUnload.SCENE_NAME.E_RESULT_COMPLETED)
        {
            StartCoroutine(MiCoFadeIn());
        }
    }

    IEnumerator MiCoFadeIn()
    {
        // 待ち時間
        yield return StartCoroutine(fade.GetComponent<Fade>().Color_FadeIn_wait(5.0f));
    }
}
