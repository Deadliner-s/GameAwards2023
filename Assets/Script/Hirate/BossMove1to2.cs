using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossMove1to2 : MonoBehaviour
{
    // SceneMoveManager�^�O�����p
    GameObject obj;
    // �J�n���̂݋N������p
    private bool bStart = false;

    void Start()
    {
        // �e�L�X�g�I�����擾�p
        obj = GameObject.FindGameObjectWithTag("NowEventSceneSet");
    }

    // Update is called once per frame
    void Update()
    {
        if (obj== null)
        {
            // �e�L�X�g�I�����擾�p
            obj = GameObject.FindGameObjectWithTag("NowEventSceneSet");
        }

        if (!bStart && obj.GetComponent<SceneEventMove>().bTextEnd)
        {
            bStart = true;
            // �{�X���ړ�������
            gameObject.transform.DOMove(new Vector3(0, -5.0f, 5.0f), 9.4f);
        }
    }
}
