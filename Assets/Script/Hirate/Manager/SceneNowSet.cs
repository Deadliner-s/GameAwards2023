using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNowSet : MonoBehaviour
{
    // ���݂̃V�[���̐ݒ�
    [Header("�V�[���Z�b�g")]
    [Tooltip("���݂̃V�[�����Z�b�g")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME nowSceneSet;

    // Start is called before the first frame update
    void Start()
    {
        SceneNowBefore.instance.sceneNowCatch = nowSceneSet;
    }
}
