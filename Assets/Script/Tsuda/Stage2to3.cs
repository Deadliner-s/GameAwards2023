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

    private AsyncOperation async;

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

            //async.allowSceneActivation = true;
            // SceneLoadManager���^�O����
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // �V�[���̊J�n
            obj.GetComponent<SceneMoveManager>().SceneLoadUnload();
        }
    }
}
