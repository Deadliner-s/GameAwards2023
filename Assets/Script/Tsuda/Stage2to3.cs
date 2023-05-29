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

    private GameObject playerManager;
    public float startPlayerInitPos = 153.0f;

    public float lastNormalTime = 147.0f;
    private bool startPlayerInitFlg = false;

    //private AsyncOperation async;
    private bool bSceneStart = false;

    [Header("�Q�[���}�l�[�W���I�u�W�F�N�g")]
    GameObject ManagerObj;

    private GameObject phaseManager;

    // Start is called before the first frame update
    void Start()
    {
        //async = SceneManager.LoadSceneAsync("Stage3Event");
        //async.allowSceneActivation = false;

        // �}�l�[�W���I�u�W�F�N�g�擾
        ManagerObj = GameObject.Find("GameManager");
        phaseManager = GameObject.Find("PhaseManagerObj");
        playerManager = GameObject.Find("PlayerManager");
        startPlayerInitFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        Counttime += Time.deltaTime;//���Ԃ𑫂�

        if (Counttime > TimeLimit)
        {
            ManagerObj.GetComponent<GManager>().SetClearFlg(2);

            if (bSceneStart) { return; }

            // SceneMoveManager���^�O����
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // �V�[���̊J�n
            //async.allowSceneActivation = true;
            obj.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT);
            obj.GetComponent<SceneMoveManager>().SceneStartUnload();

            bSceneStart = true;
        }

        // �v���C���[�������Ɉړ�
        if (Counttime >= startPlayerInitPos && startPlayerInitFlg == false)
        {
            startPlayerInitFlg = true;
            playerManager.GetComponent<MoveToInitialPosition>().enabled = true;
        }
        if (Counttime >= lastNormalTime)
        {
            phaseManager.GetComponent<PhaseManager>().SetLastNormalFlg(true);
        }
    }
}
