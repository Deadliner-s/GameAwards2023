using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1to2 : MonoBehaviour
{
    private float Counttime;//���Ԃ𑪂�
    public float TimeLimit = 160.0f;//��������
    private Text TimeText;

    public float startPlayerInitPos = 153.0f;
    public float lastNormalTime = 147.0f;

    private bool startPlayerInitFlg = false;

    //private AsyncOperation async;
    private bool bSceneStart = false;

    [Header("�Q�[���}�l�[�W���I�u�W�F�N�g")]
    GameObject ManagerObj;

    private GameObject playerManager;
    private GameObject phaseManager;

    // Start is called before the first frame update
    void Start()
    {
        //async = SceneManager.LoadSceneAsync("Stage2Event");
        //async.allowSceneActivation = false;

        // �}�l�[�W���I�u�W�F�N�g�擾
        ManagerObj = GameObject.Find("GameManager");

        playerManager = GameObject.Find("PlayerManager");

        phaseManager = GameObject.Find("PhaseManagerObj");

        startPlayerInitFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        Counttime += Time.deltaTime;//���Ԃ𑫂�

        if (Counttime > TimeLimit)
        {
            ManagerObj.GetComponent<GManager>().SetClearFlg(1);

            if (bSceneStart) { return; }

            // SceneMoveManager���^�O����
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // �V�[���̊J�n
            //async.allowSceneActivation = true;
            obj.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT);
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
