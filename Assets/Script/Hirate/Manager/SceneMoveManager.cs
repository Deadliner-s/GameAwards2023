using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveManager : MonoBehaviour
{
    // ���݂̃V�[���̐ݒ�
    [Header("���݂̃V�[���ݒ�")]
    [Tooltip("���݂̃V�[��")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME SceneNow;

    // ���̃V�[���̐ݒ�
    [Header("���̃V�[���ݒ�")]
    [Tooltip("���̃V�[��")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME SceneNext;

    // ���̃V�[���̎��O���[�h�̐ݒ�
    [Header("���̃V�[���̎��O���[�h�ݒ�")]
    [Tooltip("���[�h�̗L��")]
    [SerializeField] bool bLoad = true;

    // ���O���[�h��n�����p
    public bool bLoadGet { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        // ���O���[�h���邩�ǂ����̔���
        if (bLoad)
        {
            // ���̃V�[���ǂݍ���
            SceneLoad(SceneNext);
        }

        // ���O���[�h��n����悤�ɐݒ����
        bLoadGet = bLoad;
    }

    // ���̃V�[���̃��[�h
    public void SceneLoad(SceneLoadStartUnload.SCENE_NAME loadscene)
    {
        // ���̃V�[���ǂݍ���
        SceneAccessSearch.SceneAccessCatchLoad(loadscene);
    }

    // ���̃V�[���̃��[�h �� ���݂̃V�[���̃A�����[�h
    public void SceneStartUnload()
    {
        // ���̃V�[���̋N��
        SceneAccessSearch.SceneAccessCatchStart();
        // ���݂̃V�[���̃A�����[�h
        SceneAccessSearch.SceneAccessCatchUnload(SceneNow);
    }
}
