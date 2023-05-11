using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneMove : MonoBehaviour
{
    // �V�[���J�ڗp
    //private enum Scene
    //{
    //    Scene1 = 1,
    //    Scene2,
    //    Scene3,

    //    SceneMax = 99,
    //}

    // �f�o�b�O�p�̃X�e�[�W�J��
    void Update()
    {
        // �ʏ�X�e�[�W
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // �X�e�[�W1
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene("Stage1");
            }
            // �X�e�[�W2
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene("Stage2");
            }
            // �X�e�[�W3
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SceneManager.LoadScene("merge_2");
            }
            // �v�����[�O
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SceneManager.LoadScene("Prologue");
            }
            // �X�e�[�W1��2�̊Ԃ̃C�x���g
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SceneManager.LoadScene("Stage2Event");
            }
            // �X�e�[�W2��3�̊Ԃ̃C�x���g
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SceneManager.LoadScene("Stage3Event");
            }
        }
    }
}
