using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor : MonoBehaviour
{
    //private Myproject InputActions;
    
    private int Selected;
    private int ItemMax;

    [SerializeField]
    private GameObject fade;                // �t�F�[�h�I�u�W�F�N�g

    [SerializeField]
    private GameObject title;

    [SerializeField]
    [Header("�I�v�V�������j���[")]
    private GameObject OptionMenu;
    private bool OptionMenuFlag = false;

    [SerializeField]
    [Header("�f�[�^�폜�m�F")]
    private GameObject CheckMenu;
    private bool CheckMenuFlag = false;

    [Header("�Q�[���}�l�[�W���I�u�W�F�N�g")]
    GameObject ManagerObj;

    void Awake()
    {
        //InputActions = new Myproject();
        //InputActions.Enable();
        //InputActions.UI.Up.performed += context => OnUp();
        //InputActions.UI.Down.performed += context => OnDown();
        //InputActions.UI.Select.performed += context => OnSelect();
    }

    // Start is called before the first frame update
    void Start()
    {
        // �}�l�[�W���I�u�W�F�N�g�擾
        ManagerObj = GameObject.Find("GameManager");

        // ���j���[��(�^�C�g�����S�������Ă邽��-1
        ItemMax = transform.childCount; //- 1;  �O���܂���

        OptionMenuFlag = false;
        CheckMenuFlag = false;

        // �����J�[�\���ʒu�ݒ�
        Selected = 0;
        // �����I���𔒂�
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {        
        // �I�v�V������ʂ�����ꂽ�ꍇ�A�ēx���͂��󂯕t����
        if(OptionMenuFlag == true)
        {
            if (OptionMenu.activeSelf == false)
            {
                title.SetActive(true);
                //nputActions.Enable();
                OptionMenuFlag = false;
            }
        }

        // �f�[�^�폜�m�F��ʂ�����ꂽ�ꍇ�A�ēx���͂��󂯕t����
        if (CheckMenuFlag == true)
        {
            if (CheckMenu.activeSelf == false)
            {
                //InputActions.Enable();
                CheckMenuFlag = false;
            }
        }


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

        if(InputManager.instance.OnSelect())
        {
            // �I�����ꂽ���j���[�ɂ���ď�����ς���
            switch (Selected)
            {
                case (0):
                    // NEW GAME
                    if (ManagerObj.GetComponent<GManager>().GetFirstTime())
                    {
                        // �X�e�[�W�P��
                        InputManager.instance.UI_Disable();
                        //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Prologue");
                        fade.GetComponent<Fade>().StartCoroutine(
                                "Color_FadeOut_Title",
                                SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE);
                    }
                    else
                    {
                        //�f�[�^�폜�m�F
                        CheckMenu.SetActive(true);
                        CheckMenuFlag = true;
                        this.gameObject.SetActive(false);
                    }
                    GManager.instance.GetContinueFlg(false);
                    SoundManager.instance.PlaySE("Decision");
                    break;
                case (1):
                    // CONTINUE
                    int num = ManagerObj.GetComponent<GManager>().GetNowStage();
                    switch (num)
                    {
                        case (0):
                            // �X�e�[�W1
                            //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Prologue");
                            // UI�̓��͂��󂯕t���Ȃ�
                            InputManager.instance.UI_Disable();
                            // �R���e�B�j���[�t���O�𗧂Ă�
                            GManager.instance.GetContinueFlg(true);
                            fade.GetComponent<Fade>().StartCoroutine(
                                "Color_FadeOut_Title",
                                SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE);
                            break;
                        case (1):
                            // �X�e�[�W2
                            //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2Event");
                            // UI�̓��͂��󂯕t���Ȃ�
                            InputManager.instance.UI_Disable();
                            // �R���e�B�j���[�t���O�𗧂Ă�
                            GManager.instance.GetContinueFlg(true);
                            fade.GetComponent<Fade>().StartCoroutine(
                                "Color_FadeOut_Title",
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT);
                            break;
                        case (2):
                            // �X�e�[�W3
                            //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage3Event");
                            // UI�̓��͂��󂯕t���Ȃ�
                            InputManager.instance.UI_Disable();
                            // �R���e�B�j���[�t���O�𗧂Ă�
                            GManager.instance.GetContinueFlg(true);
                            fade.GetComponent<Fade>().StartCoroutine(
                                "Color_FadeOut_Title",
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT);
                            break;
                        default:
                            break;
                    }

                    // SE�Đ�
                    SoundManager.instance.PlaySE("Decision");

                    break;
                case (2):
                    // OPTION
                    title.SetActive(false);
                    OptionMenu.SetActive(true);
                    OptionMenuFlag = true;
                    this.gameObject.SetActive(false);
                    SoundManager.instance.PlaySE("Decision");
                    break;
                case (3):
                    // EXIT
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
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
