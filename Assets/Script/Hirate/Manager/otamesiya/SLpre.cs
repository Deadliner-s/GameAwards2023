using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SLpre : MonoBehaviour
{
    //private AsyncOperation asyn = new AsyncOperation();
    private AsyncOperation[] asynAll = new AsyncOperation[(int)SceneLoadStartUnload.SCENE_NAME.E_SCENE_MAX];

    private void Start()
    {
        asynAll[0] = SceneManager.LoadSceneAsync("SLpre3", LoadSceneMode.Additive);
        asynAll[0].allowSceneActivation = false;
        asynAll[1] = SceneManager.LoadSceneAsync("SLpre4", LoadSceneMode.Additive);
        asynAll[1].allowSceneActivation = false;
        asynAll[2] = SceneManager.LoadSceneAsync("SLpre5", LoadSceneMode.Additive);
        asynAll[2].allowSceneActivation = false;
    }

    public void SSta()
    {
        asynAll[0].allowSceneActivation = true;
        asynAll[1].allowSceneActivation = true;
        asynAll[2].allowSceneActivation = true;
        SUnco();
    }

    public void SUnco()
    {
        asynAll[0] = SceneManager.UnloadSceneAsync("SLpre2");
    }

    public void SSta1()
    {
        asynAll[1].allowSceneActivation = true;
        SUnco1();
    }

    public void SUnco1()
    {
        asynAll[1] = SceneManager.UnloadSceneAsync("SLpre2");
    }
}
