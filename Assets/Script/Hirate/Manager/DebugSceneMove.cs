using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneMove : MonoBehaviour
{
    // ����p
    private SceneLoadStartUnload.SCENE_NAME sceneName;
    // �f�o�b�O�p(�_���[�W��0�ɂ��܂�)
    private bool debug = false;

    private void Start()
    {
        // �f�o�b�O�p�̐ݒ�
        debug = DebugCommandooo.instance.debugSceneSet;
    }

    // �f�o�b�O�p�̃X�e�[�W�J��
    private void Update()
    {
        if (!debug) { return; }

        // P�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.P))
        {
            // ���݂̃V�[���̎擾
            sceneName = SceneNowBefore.instance.sceneNowCatch;

            // �V�[���J��
            switch (sceneName)
            {
                // �X�e�[�W1
                case (SceneLoadStartUnload.SCENE_NAME.E_STAGE1):
                    // �V�[���̊J�n
                    gameObject.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT);
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
                // �X�e�[�W2
                case (SceneLoadStartUnload.SCENE_NAME.E_STAGE2):
                    // �V�[���̊J�n
                    gameObject.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT);
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
                // �X�e�[�W3
                case (SceneLoadStartUnload.SCENE_NAME.E_STAGE3):
                    // �V�[���̊J�n
                    gameObject.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_EPILOGUE);
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
                // �v�����[�O
                case (SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE):
                    // �V�[���̊J�n
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
                // �X�e�[�W1��2�̊�
                case (SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT):
                    // �V�[���̊J�n
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
                // �X�e�[�W2��3�̊�
                case (SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT):
                    // �V�[���̊J�n
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
            }
        }

        // L�L�[�������ꂽ��
        if (!Input.GetKeyDown(KeyCode.L)) { return; }
        // NowEventSceneSet���^�O����
        GameObject texEnd = GameObject.FindGameObjectWithTag("NowEventSceneSet");
        if (texEnd == null) { return; }
        // �e�L�X�g���΂�
        texEnd.GetComponent<SceneEventMove>().bTextEnd = true;
    }
}
