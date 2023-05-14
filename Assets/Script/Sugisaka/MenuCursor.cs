using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor : MonoBehaviour
{
    private Myproject InputActions;
    
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
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.UI.Up.performed += context => OnUp();
        InputActions.UI.Down.performed += context => OnDown();
        InputActions.UI.Select.performed += context => OnSelect();
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
                InputActions.Enable();
                OptionMenuFlag = false;
            }
        }

        // �I�v�V������ʂ�����ꂽ�ꍇ�A�ēx���͂��󂯕t����
        if (CheckMenuFlag == true)
        {
            if (CheckMenu.activeSelf == false)
            {
                title.SetActive(true);
                InputActions.Enable();
                CheckMenuFlag = false;
            }
        }
    }

    private void OnUp()
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

    private void OnDown()
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

    private void OnSelect()
    {
        // �I�����ꂽ���j���[�ɂ���ď�����ς���
        switch (Selected)
        {
            case (0):
                // NEW GAME
                
                //�f�[�^�폜�m�F
                title.SetActive(false);
                CheckMenu.SetActive(true);
                CheckMenuFlag = true;
                InputActions.Disable();             // �I�v�V�������j���[���J���Ă���Ƃ��͓��͂��󂯕t���Ȃ�
                this.gameObject.SetActive(false);
                SoundManager.instance.PlaySE("Decision");
                break;

            case (1):
                // CONTINUE
                int num = ManagerObj.GetComponent<GManager>().GetNowStage();
                switch (num)
                {
                    case (0):
                        // �X�e�[�W1
                        InputActions.Disable();
                        fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Prologue");
                        break;
                    case (1):
                        // �X�e�[�W2
                        InputActions.Disable();
                        fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2Event");
                        break;
                    case (2):
                        // �X�e�[�W3
                        InputActions.Disable();
                        fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage3Event");
                        break;
                }
                SoundManager.instance.PlaySE("Decision");
                break;

            case (2):
                // OPTION
                title.SetActive(false);
                OptionMenu.SetActive(true);
                OptionMenuFlag = true;
                InputActions.Disable();             // �I�v�V�������j���[���J���Ă���Ƃ��͓��͂��󂯕t���Ȃ�
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
