using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStartManager : MonoBehaviour
{
    // �n�߂�V�[���̐ݒ�
    [Header("�n�߂�V�[��")]
    [Tooltip("�n�߂�V�[��")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME SceneName;

    // Start is called before the first frame update
    void Start()
    {
        // SceneLoadManager���^�O����
        GameObject obj = GameObject.FindGameObjectWithTag("SceneLoadManager");
        // �V�[���̃��[�h
        obj.GetComponent<SceneLoadStartUnload>().SceneLoad(SceneName);
        // �V�[���̊J�n
        obj.GetComponent<SceneLoadStartUnload>().SceneStart();
    }
}
