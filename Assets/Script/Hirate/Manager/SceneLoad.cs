using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    // �V�[���̖��O
    public enum SCENE_NAME
    {
        E_STAGE1 = 0,       // �X�e�[�W1
        E_STAGE2,           // �X�e�[�W2
        E_STAGE3,           // �X�e�[�W3
        E_PROLOGUE,         // �v�����[�O
        E_STAGE2_EVENT,     // �X�e�[�W1��2�̊Ԃ̃C�x���g
        E_STAGE3_EVENT,     // �X�e�[�W2��3�̊Ԃ̃C�x���g
        E_EPILOGUE,         // �G�s���[�O
        E_RESULT_COMPLETED, // ���U���g(���̏���)
        E_RESULT_FAILED,    // ���U���g(�l�̕���)

        E_SCENE_MAX         // �V�[���̍ő�
    }

    // �ǂݍ��ݗp
    private AsyncOperation[] async = new AsyncOperation[(int)SCENE_NAME.E_SCENE_MAX];

    // Start is called before the first frame update
    void Start()
    {
        // �X�e�[�W1
        if (async[(int)SCENE_NAME.E_STAGE1] == null)
        {
            async[(int)SCENE_NAME.E_STAGE1] = SceneManager.LoadSceneAsync("Stage1", LoadSceneMode.Additive);
        }
        // �X�e�[�W2
        if (async[(int)SCENE_NAME.E_STAGE2] == null)
        {
            async[(int)SCENE_NAME.E_STAGE2] = SceneManager.LoadSceneAsync("Stage2", LoadSceneMode.Additive);
        }
        // �X�e�[�W3
        if (async[(int)SCENE_NAME.E_STAGE3] == null)
        {
            async[(int)SCENE_NAME.E_STAGE3] = SceneManager.LoadSceneAsync("merge_2", LoadSceneMode.Additive);
        }
        // �v�����[�O
        if (async[(int)SCENE_NAME.E_PROLOGUE] == null)
        {
            async[(int)SCENE_NAME.E_PROLOGUE] = SceneManager.LoadSceneAsync("Prologue", LoadSceneMode.Additive);
        }
        // �X�e�[�W1��2�̊Ԃ̃C�x���g
        if (async[(int)SCENE_NAME.E_STAGE2_EVENT] == null)
        {
            async[(int)SCENE_NAME.E_STAGE2_EVENT] = SceneManager.LoadSceneAsync("Stage2Event", LoadSceneMode.Additive);
        }
        // �X�e�[�W2��3�̊Ԃ̃C�x���g
        if (async[(int)SCENE_NAME.E_STAGE3_EVENT] == null)
        {
            async[(int)SCENE_NAME.E_STAGE3_EVENT] = SceneManager.LoadSceneAsync("Stage3Event", LoadSceneMode.Additive);
        }
        // �G�s���[�O
        if (async[(int)SCENE_NAME.E_EPILOGUE] == null)
        {
            async[(int)SCENE_NAME.E_EPILOGUE] = SceneManager.LoadSceneAsync("Epilogue", LoadSceneMode.Additive);
        }
        // ���U���g(���̏���)
        if (async[(int)SCENE_NAME.E_RESULT_COMPLETED] == null)
        {
            async[(int)SCENE_NAME.E_RESULT_COMPLETED] = SceneManager.LoadSceneAsync("Stage1", LoadSceneMode.Additive);
        }
        // ���U���g(�l�̕���)
        if (async[(int)SCENE_NAME.E_RESULT_FAILED] == null)
        {
            async[(int)SCENE_NAME.E_RESULT_FAILED] = SceneManager.LoadSceneAsync("Stage1", LoadSceneMode.Additive);
        }

        // �S�Ẵ��[�h�t���O��false�ɂ���
        for (int i = 0; i < (int)SCENE_NAME.E_SCENE_MAX; i++)
        {
            async[i].allowSceneActivation = false;
        }
    }

    // �V�[���N���֐�
    public void SceneStart(SCENE_NAME scene_name)
    {
        // �Ԃ�l�Ń_���[�W��Ԃ����߂̕���
        switch (scene_name)
        {
            // �X�e�[�W1
            case SCENE_NAME.E_STAGE1:
                async[(int)SCENE_NAME.E_STAGE1].allowSceneActivation = true;
                break;
            // �X�e�[�W2
            case SCENE_NAME.E_STAGE2:
                async[(int)SCENE_NAME.E_STAGE2].allowSceneActivation = true;
                break;
            // �X�e�[�W3
            case SCENE_NAME.E_STAGE3:
                async[(int)SCENE_NAME.E_STAGE3].allowSceneActivation = true;
                break;
            // �v�����[�O
            case SCENE_NAME.E_PROLOGUE:
                async[(int)SCENE_NAME.E_PROLOGUE].allowSceneActivation = true;
                break;
            // �X�e�[�W1��2�̊Ԃ̃C�x���g
            case SCENE_NAME.E_STAGE2_EVENT:
                async[(int)SCENE_NAME.E_STAGE2_EVENT].allowSceneActivation = true;
                break;
            // �X�e�[�W2��3�̊Ԃ̃C�x���g
            case SCENE_NAME.E_STAGE3_EVENT:
                async[(int)SCENE_NAME.E_STAGE3_EVENT].allowSceneActivation = true;
                break;
            // �G�s���[�O
            case SCENE_NAME.E_EPILOGUE:
                async[(int)SCENE_NAME.E_EPILOGUE].allowSceneActivation = true;
                break;
            // ���U���g(���̏���)
            case SCENE_NAME.E_RESULT_COMPLETED:
                async[(int)SCENE_NAME.E_RESULT_COMPLETED].allowSceneActivation = true;
                break;
            // ���U���g(�l�̕���)
            case SCENE_NAME.E_RESULT_FAILED:
                async[(int)SCENE_NAME.E_RESULT_FAILED].allowSceneActivation = true;
                break;
        }
    }

    // �V�[���̃A�����[�h
    public void SceneUnLoad(SCENE_NAME scene_name)
    {
        // �Ԃ�l�Ń_���[�W��Ԃ����߂̕���
        switch (scene_name)
        {
            // �X�e�[�W1
            case SCENE_NAME.E_STAGE1:
                async[(int)SCENE_NAME.E_STAGE1] = SceneManager.UnloadSceneAsync("Stage1");
                break;
            // �X�e�[�W2
            case SCENE_NAME.E_STAGE2:
                async[(int)SCENE_NAME.E_STAGE2] = SceneManager.UnloadSceneAsync("Stage2");
                break;
            // �X�e�[�W3
            case SCENE_NAME.E_STAGE3:
                async[(int)SCENE_NAME.E_STAGE3] = SceneManager.UnloadSceneAsync("Stage3");
                break;
            // �v�����[�O
            case SCENE_NAME.E_PROLOGUE:
                async[(int)SCENE_NAME.E_PROLOGUE] = SceneManager.UnloadSceneAsync("Prologue");
                break;
            // �X�e�[�W1��2�̊Ԃ̃C�x���g
            case SCENE_NAME.E_STAGE2_EVENT:
                async[(int)SCENE_NAME.E_STAGE2_EVENT] = SceneManager.UnloadSceneAsync("Stage2Event");
                break;
            // �X�e�[�W2��3�̊Ԃ̃C�x���g
            case SCENE_NAME.E_STAGE3_EVENT:
                async[(int)SCENE_NAME.E_STAGE3_EVENT] = SceneManager.UnloadSceneAsync("Stage3Event");
                break;
            // �G�s���[�O
            case SCENE_NAME.E_EPILOGUE:
                async[(int)SCENE_NAME.E_EPILOGUE] = SceneManager.UnloadSceneAsync("Epilogue");
                break;
            // ���U���g(���̏���)
            case SCENE_NAME.E_RESULT_COMPLETED:
                async[(int)SCENE_NAME.E_RESULT_COMPLETED] = SceneManager.UnloadSceneAsync("Stage1");
                break;
            // ���U���g(�l�̕���)
            case SCENE_NAME.E_RESULT_FAILED:
                async[(int)SCENE_NAME.E_RESULT_FAILED] = SceneManager.UnloadSceneAsync("Stage1");
                break;
        }
    }
}
