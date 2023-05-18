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

    // Start is called before the first frame update
    void Start()
    {
        // ���̃V�[���ǂݍ���
        SceneAccessSearch.SceneAccessCatchLoad(SceneNext);
    }

    // ���̃V�[���̃��[�h �� ���݂̃V�[���̃A�����[�h
    public void SceneLoadUnload()
    {
        // ���̃V�[���̃��[�h
        SceneAccessSearch.SceneAccessCatchStart();
        // ���݂̃V�[���̃A�����[�h
        SceneAccessSearch.SceneAccessCatchUnload(SceneNow);
    }

    // ���̃V�[���̃��[�h �� ���݂̃V�[���̃A�����[�h�̌Ăяo��
    public static void SceneLoadUnloadCall()
    {
        // SceneLoadManager���^�O����
        GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
        // �V�[���̊J�n
        obj.GetComponent<SceneMoveManager>().SceneLoadUnload();
    }
}
