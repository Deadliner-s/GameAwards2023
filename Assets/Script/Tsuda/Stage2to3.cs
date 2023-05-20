using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage2to3 : MonoBehaviour
{
    private float Counttime;//���Ԃ𑪂�
    public float TimeLimit;//��������
    private Text TimeText;

    //private AsyncOperation async;
    private bool bSceneStart = false;

    [Header("�Q�[���}�l�[�W���I�u�W�F�N�g")]
    GameObject ManagerObj;

    // Start is called before the first frame update
    void Start()
    {
        //async = SceneManager.LoadSceneAsync("Stage3Event");
        //async.allowSceneActivation = false;

        // �}�l�[�W���I�u�W�F�N�g�擾
        ManagerObj = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        Counttime += Time.deltaTime;//���Ԃ𑫂�

        if (Counttime > TimeLimit || Input.GetKeyDown(KeyCode.P))
        {
            ManagerObj.GetComponent<GManager>().SetClearFlg(2);

            if (bSceneStart) { return; }

            // SceneLoadManager���^�O����
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // �V�[���̊J�n
            //async.allowSceneActivation = true;
            obj.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT);
            obj.GetComponent<SceneMoveManager>().SceneStartUnload();

            bSceneStart = true;
        }
    }
}
