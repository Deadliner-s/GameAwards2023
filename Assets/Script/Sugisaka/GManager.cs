using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    
    [SerializeField]
    bool Stage1;
    [SerializeField]
    bool Stage2;
    
    [SerializeField]
    bool FirstTime = true;

    //private AsyncOperation stage1;
    //private AsyncOperation stage2;
    //private AsyncOperation stage3;
    //private bool SceneLoadFlg = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadData();

        //stage1 = SceneManager.LoadSceneAsync("Stage1");
        //stage2 = SceneManager.LoadSceneAsync("Stage2");
        //stage3 = SceneManager.LoadSceneAsync("merge_2");
        //stage1.allowSceneActivation = false;
        //stage2.allowSceneActivation = false;
        //stage3.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {

        // �񓯊�����
        //if (SceneManager.GetActiveScene().name == "Title")
        //{
        //    if (SceneLoadFlg == false)
        //    {
        //        stage1 = null;
        //        stage1 = SceneManager.LoadSceneAsync("Stage1");
        //        stage1.allowSceneActivation = false;

        //        stage2 = null;
        //        stage2 = SceneManager.LoadSceneAsync("Stage2");
        //        stage2.allowSceneActivation = false;

        //        stage3 = null;
        //        stage3 = SceneManager.LoadSceneAsync("merge_2");
        //        stage3.allowSceneActivation = false;

        //        SceneLoadFlg = true;
        //    }
        //}

        //if (SceneManager.GetActiveScene().name != "Title")
        //{
        //    SceneLoadFlg = false;
        //}
    }

    void LoadData()
    {
        int stageNum = PlayerPrefs.GetInt("Stage_Num", 4);
        switch (stageNum)
        {
            case (0):
                Stage1 = false;
                Stage2 = false;
                break;
            case (1):
                Stage1 = true;
                Stage2 = false;
                break;
            case (2):
                Stage1 = true;
                Stage2 = true;
                break;
            default:
                Stage1 = false;
                Stage2 = false;
                break;
        }
    }


    public void ReSetData()
    {
        // �Q�[�����t���O���Z�b�g
        Stage1 = false;
        Stage2 = false;

        // 
        PlayerPrefs.SetInt("Stage_Num", 0);
    }

    // ���j���[�̃R���e�j���[�Ŏg�p
    public int GetNowStage()
    {
        // ������
        int Snum = 0;
        if (FirstTime){
            Snum = 4;
            return Snum;
        }

        // �N���A�t���O���m�F���Ď��̃X�e�[�W�I��
        if (Stage1 != true){
            // �X�e�[�W1
            Snum = 0;
        }
        else if (Stage2 != true){
            // �X�e�[�W2
            Snum = 1;
        }
        else{
            // �X�e�[�W3
            Snum = 2;
        }

        return Snum;
    }

    // �X�e�[�W�N���A����
    public void SetClearFlg(int num)
    {
        switch (num)
        {
            case (1):
                Stage1 = true;
                Stage2 = false;
                PlayerPrefs.SetInt("Stage_Num", 1);
                break;
            case (2):
                Stage1 = true;
                Stage2 = true;
                PlayerPrefs.SetInt("Stage_Num", 2);
                break;
            default:
                break;
        }
    }

    void OnApplicationQuit()
    {
        // �A�v���P�[�V�����I������
        // ���݂̃N���A�󋵂�ۑ�
        if (Stage1 != true)
        {
            PlayerPrefs.SetInt("Stage_Num", 0);
        }
        else if(Stage2 != true)
        {
            PlayerPrefs.SetInt("Stage_Num", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Stage_Num", 2);
        }
    }

    public bool GetFirstTime()
    {
        if (FirstTime){
            // ����
            FirstTime = false;
            return true;
        }
        else{
            // 2��ڈȍ~
            return false;
        }
    }
}
