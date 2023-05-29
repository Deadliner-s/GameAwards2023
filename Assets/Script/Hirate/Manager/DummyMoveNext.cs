using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMoveNext : MonoBehaviour
{
    private void Start()
    {
        // ���g���C��
        if (SceneSelectCatch.instance.selectCatch == 1)
        {
            // �V�[���J��
            SceneAccessSearch.SceneAccessCatchLoadStart(SceneNowBefore.instance.sceneBeforeCatch);
            SceneAccessSearch.SceneAccessCatchUnload(SceneLoadStartUnload.SCENE_NAME.E_DUMMY);
            return;
        }
        // ���X�^�[�g��(�X�e�[�W1�ɖ߂�)
        if (SceneSelectCatch.instance.selectCatch == 2)
        {
            // �X�e�[�W1�̂ݓ����V�[���ɖ߂�
            if (SceneNowBefore.instance.sceneBeforeCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE1)
            {
                // �V�[���J��
                SceneAccessSearch.SceneAccessCatchLoadStart(SceneNowBefore.instance.sceneBeforeCatch);
                SceneAccessSearch.SceneAccessCatchUnload(SceneLoadStartUnload.SCENE_NAME.E_DUMMY);
                return;
            }
            else
            {
                // �V�[���J��
                SceneAccessSearch.SceneAccessCatchLoadStart(SceneLoadStartUnload.SCENE_NAME.E_STAGE1);
                SceneAccessSearch.SceneAccessCatchUnload(SceneLoadStartUnload.SCENE_NAME.E_DUMMY);
                return;
            }
        }
    }
}
