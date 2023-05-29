using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor_Resu : MonoBehaviour
{
    //private Myproject InputActions;
    
    private int Selected;
    private int ItemMax;

    [SerializeField]
    private GameObject fade;                // �t�F�[�h�I�u�W�F�N�g
   
    [Header("�Q�[���}�l�[�W���I�u�W�F�N�g")]
    GameObject ManagerObj;


    private GameObject continueTextJP;
    private GameObject titleTextJP;


    //void Awake()
    //{
    //    InputActions = new Myproject();
    //    InputActions.Enable();
    //    InputActions.UI.Up.performed += context => OnUp();
    //    InputActions.UI.Down.performed += context => OnDown();
    //    InputActions.UI.Select.performed += context => OnSelect();
    //}

    // Start is called before the first frame update
    void Start()
    {
        // �}�l�[�W���I�u�W�F�N�g�擾
        ManagerObj = GameObject.Find("GameManager");
        
        ItemMax = transform.childCount - 1;
        
        // �����J�[�\���ʒu�ݒ�
        Selected = 0;
        // �����I���𔒂�
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;


        continueTextJP = GameObject.Find("continueJP");
        titleTextJP = GameObject.Find("titleJP");

        continueTextJP.SetActive(true);
        titleTextJP.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.OnUp())
        {
            // �O�I��������
            transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.black;
            // 
            Selected--;
            Selected = (int)Mathf.Repeat(Selected, ItemMax);
            // ���I���𔒂�
            transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;

            SoundManager.instance.PlaySE("Select");

        }
        if(InputManager.instance.OnDown())
        {
            // �O�I��������
            transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.black;
            //
            Selected++;
            Selected = (int)Mathf.Repeat(Selected, ItemMax);
            // ���I���𔒂�
            transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;

            SoundManager.instance.PlaySE("Select");

        }

        switch (Selected)
        {
            case (0):
                // CONTINUE
                continueTextJP.SetActive(true);
                titleTextJP.SetActive(false);
                break;
            case (1):
                // Return to title
                continueTextJP.SetActive(false);
                titleTextJP.SetActive(true);
                break;
        }

        if(InputManager.instance.OnSelect())
        {
            // �I�����ꂽ���j���[�ɂ���ď�����ς���
            switch (Selected)
            {
                case (0):
                    // CONTINUE
                    //int num = ManagerObj.GetComponent<GManager>().GetNowStage();
                    SceneLoadStartUnload.SCENE_NAME sceneName;
                    sceneName = SceneNowBefore.instance.sceneBeforeCatch;
                    //InputActions.Disable();
                    switch (sceneName)
                    {
                        case (SceneLoadStartUnload.SCENE_NAME.E_STAGE1):
                            // �X�e�[�W1
                            //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Prologue");
                            fade.GetComponent<Fade>().StartCoroutine(
                                fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                                    SceneLoadStartUnload.SCENE_NAME.E_RESULT_FAILED,
                                    SceneLoadStartUnload.SCENE_NAME.E_STAGE1));
                            GManager.instance.SetContinueFlg(true);
                            break;
                        case (SceneLoadStartUnload.SCENE_NAME.E_STAGE2):
                            // �X�e�[�W2
                            //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2Event");
                            fade.GetComponent<Fade>().StartCoroutine(
                                fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                                    SceneLoadStartUnload.SCENE_NAME.E_RESULT_FAILED,
                                    SceneLoadStartUnload.SCENE_NAME.E_STAGE2));
                            GManager.instance.SetContinueFlg(true);
                            break;
                        case (SceneLoadStartUnload.SCENE_NAME.E_STAGE3):
                            // �X�e�[�W3
                            //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage3Event");
                            fade.GetComponent<Fade>().StartCoroutine(
                                fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                                    SceneLoadStartUnload.SCENE_NAME.E_RESULT_FAILED,
                                    SceneLoadStartUnload.SCENE_NAME.E_STAGE3));
                            GManager.instance.SetContinueFlg(true);
                            break;
                    }
                    SoundManager.instance.PlaySE("Decision");
                    break;
                case (1):
                    // Return to title
                    //InputActions.Disable();
                    //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Title");
                    fade.GetComponent<Fade>().StartCoroutine(
                                fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                                    SceneLoadStartUnload.SCENE_NAME.E_RESULT_FAILED,
                                    SceneLoadStartUnload.SCENE_NAME.E_TITLE));
                    SoundManager.instance.PlaySE("Decision");
                    break;
            }
        }
    }

    //private void OnUp()
    //{

    //}

    //private void OnDown()
    //{

    //}

    //private void OnSelect()
    //{

    //}
}
