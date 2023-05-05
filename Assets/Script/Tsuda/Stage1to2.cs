using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1to2 : MonoBehaviour
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
        async = SceneManager.LoadSceneAsync("Stage2");
        async.allowSceneActivation = false;

        // �}�l�[�W���I�u�W�F�N�g�擾
        ManagerObj = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        Counttime += Time.deltaTime;//���Ԃ𑫂�

        if (Counttime > TimeLimit || Input.GetKeyDown(KeyCode.P))
        {
            ManagerObj.GetComponent<GManager>().SetClearFlg(1);

            async.allowSceneActivation = true;            
        }
    }
}
