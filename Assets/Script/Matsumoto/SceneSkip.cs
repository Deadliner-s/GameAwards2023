using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSkip : MonoBehaviour
{
    private GameObject fade;
    //private Myproject InputActions;

    //private SceneLoadStartUnload.SCENE_NAME currentScene;
    //private SceneLoadStartUnload.SCENE_NAME nextScene;

    //void Awake()
    //{
    //    if (this.gameObject != null)
    //    {
    //        InputActions = new Myproject();
    //        InputActions.Enable();
    //        InputActions.UI.Start.performed += context => OnStart();
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        fade = GameObject.Find("Fade");
        //currentScene = SceneNow.instance.sceneNowCatch;
        //nextScene = currentScene;
    }

    // Update is called once per frame
    void Update()
    {
        //currentScene = SceneNow.instance.sceneNowCatch;
        if (fade == null)
        {
            fade = GameObject.Find("Fade");
        }

        // シーンが切り替わった時に呼ばれる
        //if (SceneNow.instance != null)
        //{
        //    if (currentScene != nextScene)
        //    {
        //        nextScene = currentScene;
        //        if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE1 || 
        //            currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE2 || 
        //            currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
        //        {
        //            InputActions.UI.Start.performed -= context => OnStart();
        //            InputActions.Disable();
        //        }
        //    }
        //}

        if (InputManager.instance.OnStart())
        {
            if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE)
            {
                InputManager.instance.UI_Disable();
                GManager.instance.SetContinueFlg(true);
                StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut());
            }
            if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT)
            {
                InputManager.instance.UI_Disable();
                GManager.instance.SetContinueFlg(true);
                // ボスのローテーションを代入する
                GameObject boss = GameObject.FindGameObjectWithTag("BossParent");
                BossRotationCatch.instance.fRotation = boss.transform.rotation;
                StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut());
            }
            if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT)
            {
                InputManager.instance.UI_Disable();
                GManager.instance.SetContinueFlg(true);
                // ボスのローテーションを代入する
                GameObject boss = GameObject.FindGameObjectWithTag("BossParent");
                BossRotationCatch.instance.fRotation = boss.transform.rotation;
                StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut());
            }
            if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_EPILOGUE)
            {
                InputManager.instance.UI_Disable();
                GManager.instance.SetContinueFlg(true);
                StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut());
            }
        }
    }
    //private void OnStart()
    //{
    //    // シーン名で分岐
    //    if (this.gameObject != null)
    //    {

            //if (SceneManager.GetActiveScene().name == "Prologue")
            //{
            //    InputActions.Disable();
            //    fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage1");
            //}
            //if (SceneManager.GetActiveScene().name == "Stage2Event")
            //{
            //    InputActions.Disable();
            //    fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2");
            //}
            //if (SceneManager.GetActiveScene().name == "Stage3Event")
            //{
            //    InputActions.Disable();
            //    fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "merge_2");
            //}
            //if (SceneManager.GetActiveScene().name == "Epilogue")
            //{
            //    InputActions.Disable();
            //    fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Title");
            //}



    //    }
    //}
}
