using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    // �ǂݍ��ݗp
    private AsyncOperation[] async;
    private AsyncOperation asyncStage1;
    private AsyncOperation asyncStage2;
    private AsyncOperation asyncStage3;
    private AsyncOperation asyncPrologue;
    private AsyncOperation asyncStage2Event;
    private AsyncOperation asyncStage3Event;
    private AsyncOperation asyncEpilogue;
    private AsyncOperation asyncResultCompleted;
    private AsyncOperation asyncResultFailed;





    // Start is called before the first frame update
    void Start()
    {
        // �X�e�[�W1
        if (asyncStage1 == null)
        {
            asyncStage1 = SceneManager.LoadSceneAsync("Stage1");
            asyncStage1.allowSceneActivation = false;
        }
        // �X�e�[�W2
        if (asyncStage2 == null)
        {
            asyncStage2 = SceneManager.LoadSceneAsync("Stage2");
            asyncStage2.allowSceneActivation = false;
        }
        // �X�e�[�W3
        if (asyncStage3 == null)
        {
            asyncStage3 = SceneManager.LoadSceneAsync("merge_2");
            asyncStage3.allowSceneActivation = false;
        }
        // �v�����[�O

        if (asyncPrologue == null)
        {
            asyncPrologue = SceneManager.LoadSceneAsync("Prologue");
            asyncPrologue.allowSceneActivation = false;
        }
        // �X�e�[�W1��2�̊Ԃ̃C�x���g
        if (asyncStage2Event == null)
        {
            asyncStage2Event = SceneManager.LoadSceneAsync("Stage2Event");
            asyncStage2Event.allowSceneActivation = false;
        }
        // �X�e�[�W2��3�̊Ԃ̃C�x���g
        if (asyncStage3Event == null)
        {
            asyncStage3Event = SceneManager.LoadSceneAsync("Stage3Event");
            asyncStage3Event.allowSceneActivation = false;
        }
        // �X�e�[�W1
        if (asyncStage1 == null)
        {
            asyncStage1 = SceneManager.LoadSceneAsync("Stage1");
            asyncStage1.allowSceneActivation = false;
        }
        // �X�e�[�W1
        if (asyncStage1 == null)
        {
            asyncStage1 = SceneManager.LoadSceneAsync("Stage1");
            asyncStage1.allowSceneActivation = false;
        }
        // �X�e�[�W1
        if (asyncStage1 == null)
        {
            asyncStage1 = SceneManager.LoadSceneAsync("Stage1");
            asyncStage1.allowSceneActivation = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //async.allowSceneActivation = true;
    }
}
