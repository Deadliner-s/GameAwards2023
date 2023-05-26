using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiCoTitle : MonoBehaviour
{
    // �t�F�[�h�I�u�W�F�N�g
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
        // �҂�����
        yield return StartCoroutine(fade.GetComponent<Fade>().Color_FadeIn_wait(5.0f));
    }
}
