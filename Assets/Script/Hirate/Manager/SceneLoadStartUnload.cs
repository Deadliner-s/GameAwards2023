using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadStartUnload : MonoBehaviour
{
    // �V�[���̖��O(enum)
    // ���uenum�v�Ɓu������v�̗����ɓ����ꏊ�ɒǉ����邱�ƁI
    public enum SCENE_NAME
    {
        E_STAGE1 = 0,       // �X�e�[�W1
        E_STAGE2,           // �X�e�[�W2
        E_STAGE3,           // �X�e�[�W3
        E_TITLE,            // �^�C�g��
        E_PROLOGUE,         // �v�����[�O
        E_STAGE2_EVENT,     // �X�e�[�W1��2�̊Ԃ̃C�x���g
        E_STAGE3_EVENT,     // �X�e�[�W2��3�̊Ԃ̃C�x���g
        E_EPILOGUE,         // �G�s���[�O
        E_RESULT_COMPLETED, // ���U���g(���̏���)
        E_RESULT_FAILED,    // ���U���g(�l�̕���)
        E_DUMMY,            // �_�~�[

        E_SCENE_MAX         // �V�[���̍ő�
    }

    // �V�[����(������)
    // ���uenum�v�Ɓu������v�̗����ɓ����ꏊ�ɒǉ����邱�ƁI
    private string[] sSceneName = {
        "Stage1",      // �X�e�[�W1
        "Stage2",      // �X�e�[�W2
        "merge_2",     // �X�e�[�W3
        "Title",       // �^�C�g��
        "Prologue",    // �v�����[�O
        "Stage2Event", // �X�e�[�W1��2�̊Ԃ̃C�x���g
        "Stage3Event", // �X�e�[�W2��3�̊Ԃ̃C�x���g
        "Epilogue",    // �G�s���[�O
        "GameClear",   // ���U���g(���̏���)
        "GameOver",    // ���U���g(�l�̕���)
        "Dummy",       // �_�~�[

        "Scene_Max"    // �V�[���̍ő�
    };

    //---- �ǂݍ��ݗp ----
    // �P�i
    private AsyncOperation async = new AsyncOperation();
    // �S��
    private AsyncOperation[] asyncAll = new AsyncOperation[(int)SCENE_NAME.E_SCENE_MAX];

    // �ǂݍ��ݏI�����p�t���O
    public bool bLoad { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        // ��Unull����
        async = null;
        for (int i = 0; i < (int)SCENE_NAME.E_SCENE_MAX; i++)
        {
            asyncAll[i] = null;
        }
        // �V�[���ǂݍ���
        //SceneLoadAll();
    }

    //�`�`�`�` ��̃V�[���̂݃��[�h�A�J�n�A�A�����[�h �`�`�`�`
    // �V�[���̃��[�h
    public void SceneLoad(SCENE_NAME scene_name)
    {
        // �����ɓ��͂��ꂽ�V�[�������[�h
        async = SceneManager.LoadSceneAsync(sSceneName[(int)scene_name], LoadSceneMode.Additive);
        //async.completed += SceneLoadFinish;
        async.allowSceneActivation = false;
    }

    // �V�[���N���֐�
    public void SceneStart()
    {
        // �V�[����NULL�`�F�b�N
        if (async == null)
        {
            Debug.Log("SceneStart�FNULL�ł�");
            return;
        }

        // �V�[�����N��
        async.allowSceneActivation = true;
    }

    // �V�[���̋N��(���[�h�ƋN�������p)
    private void SceneStart(AsyncOperation obj)
    {
        // �V�[����NULL�`�F�b�N
        if (async == null)
        {
            Debug.Log("SceneStart�FNULL�ł�");
            return;
        }

        // �V�[�����N��
        async.allowSceneActivation = true;
        //do
        //{
        //    // �V�[�����N��
        //    async.allowSceneActivation = true;
        //    // ���[�h�����t���O�����Ă�
        //    bLoad = false;
        //}
        //while (!bLoad);
    }

    // �V�[���̃��[�h�ƋN��
    public void SceneLoadStart(SCENE_NAME scene_name)
    {
        // �����ɓ��͂��ꂽ�V�[�������[�h
        async = SceneManager.LoadSceneAsync(sSceneName[(int)scene_name], LoadSceneMode.Additive);
        // ���[�h������ɋN��
        async.completed += SceneStart;
    }

    // �V�[���̃A�����[�h
    public void SceneUnload(SCENE_NAME scene_name)
    {
        // �V�[����NULL�`�F�b�N
        if (async == null)
        {
            Debug.Log("SceneUnload�F�V�[����NULL�ł�");
            return;
        }
        // �����ɓ��͂��ꂽ�V�[�����A�����[�h
        async = SceneManager.UnloadSceneAsync(sSceneName[(int)scene_name]);
    }

    // �V�[�����[�h�}�l�[�W���[�̎擾 �� �V�[���̊J�n
    public void SceneLoadCatchLoad(SCENE_NAME scene_name)
    {
        // �V�[����NULL�`�F�b�N
        if (async == null)
        {
            Debug.Log("���̃V�[���͌��݂̃V�[���A��������NULL�ł�");
            return;
        }
        // �V�[�����[�h�}�l�[�W���[�̎擾
        GameObject SceneLoadManager;
        SceneLoadManager = GameObject.FindWithTag("SceneLoadManager");
        // �V�[���̊J�n
        SceneLoadManager.GetComponent<SceneLoadStartUnload>().SceneStart();
    }

    // �V�[�����[�h�}�l�[�W���[�̎擾 �� �V�[���̃A�����[�h
    public void SceneLoadCatchUnLoad(SCENE_NAME scene_name)
    {
        // �V�[����NULL�`�F�b�N
        if (async == null)
        {
            Debug.Log("���̃V�[���͌��݂̃V�[���A��������NULL�ł�");
            return;
        }

        // �V�[�����[�h�}�l�[�W���[�̎擾
        GameObject SceneLoadManager;
        SceneLoadManager = GameObject.FindWithTag("SceneLoadManager");
        // �V�[���̃A�����[�h
        SceneLoadManager.GetComponent<SceneLoadStartUnload>().SceneUnload(scene_name);
    }

    // ���[�h�������Ƀt���O�����Ă�
    private void SceneLoadFinish(AsyncOperation obj)
    {
        // ���[�h�����t���O�����Ă�
        bLoad = true;
    }

    // �f�o�b�O�p
    private void SceneLoad_completed(AsyncOperation obj)
    {
        Debug.Log("finish");
    }




    //�`�`�`�` �S�V�[���̃��[�h�A�J�n�A�A�����[�h �`�`�`�`
    // �V�[���̃��[�h
    // �����݂̃V�[���̏ꍇ��NULL�̂܂܂ɂ��܂�
    public void SceneLoadAll()
    {
        // �S�ẴV�[�������[�h�A���[�h�t���O��false����
        for (int i = 0; i < (int)SCENE_NAME.E_SCENE_MAX; i++)
        {
            // ���[�h
            asyncAll[i] = SceneManager.LoadSceneAsync(sSceneName[i], LoadSceneMode.Additive);
            // false�ɂ���
            asyncAll[i].allowSceneActivation = false;
        }
    }

    // �V�[���N���֐�
    public void SceneStartAll(SCENE_NAME scene_name)
    {
        // �V�[����NULL�`�F�b�N
        if (asyncAll[(int)scene_name] == null)
        {
            Debug.Log("SceneStart�F���̃V�[���͌��݂̃V�[���A��������NULL�ł�");
            return;
        }
        // �����ɓ��͂��ꂽ�V�[�����N��
        asyncAll[(int)scene_name].allowSceneActivation = true;
    }

    // �V�[���̃A�����[�h
    public void SceneUnloadAll(SCENE_NAME scene_name)
    {
        // �V�[����NULL�`�F�b�N
        if (asyncAll[(int)scene_name] == null)
        {
            Debug.Log("���̃V�[���͌��݂̃V�[���A��������NULL�ł�");
            return;
        }
        // �����ɓ��͂��ꂽ�V�[�����A�����[�h
        asyncAll[(int)scene_name] = SceneManager.UnloadSceneAsync(sSceneName[(int)scene_name]);
        //async[(int)SCENE_NAME.E_PROLOGUE].completed += SceneLoad_completed;
    }

    // �V�[�����[�h�}�l�[�W���[�̎擾 �� �V�[���̊J�n
    public void SceneLoadCatchLoadAll(SCENE_NAME scene_name)
    {
        // �V�[����NULL�`�F�b�N
        if (asyncAll[(int)scene_name] == null)
        {
            Debug.Log("���̃V�[���͌��݂̃V�[���A��������NULL�ł�");
            return;
        }
        // �V�[�����[�h�}�l�[�W���[�̎擾
        GameObject SceneLoadManager;
        SceneLoadManager = GameObject.FindWithTag("SceneLoadManager");
        // �V�[���̊J�n
        SceneLoadManager.GetComponent<SceneLoadStartUnload>().SceneStartAll(scene_name);
    }

    // �V�[�����[�h�}�l�[�W���[�̎擾 �� �V�[���̃A�����[�h
    public void SceneLoadCatchUnLoadAll(SCENE_NAME scene_name)
    {
        // �V�[����NULL�`�F�b�N
        if (asyncAll[(int)scene_name] == null)
        {
            Debug.Log("���̃V�[���͌��݂̃V�[���A��������NULL�ł�");
            return;
        }
        // �V�[�����[�h�}�l�[�W���[�̎擾
        GameObject SceneLoadManager;
        SceneLoadManager = GameObject.FindWithTag("SceneLoadManager");
        // �V�[���̃A�����[�h
        SceneLoadManager.GetComponent<SceneLoadStartUnload>().SceneUnloadAll(scene_name);
    }
}
