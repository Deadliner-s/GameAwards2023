using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventMove : MonoBehaviour
{
    [SerializeField]
    private GameObject fade; // �t�F�[�h�I�u�W�F�N�g

    public bool bTextEnd { get; set; } = false; // �e�L�X�g�̏I���擾�p
    public bool bAniEnd { get; set; } = false; // �A�j���[�V�����̏I���擾�p

    // Update is called once per frame
    void Update()
    {
        //if (bTextEnd && bAniEnd)
        if (bAniEnd)
        {
            // SceneMoveManager���^�O����
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // �V�[���̊J�n
            obj.GetComponent<SceneMoveManager>().SceneStartUnload();
        }
    }
}
