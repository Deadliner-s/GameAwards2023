using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor : MonoBehaviour
{
    public enum Stage
    {
        STAGE_1,
        STAGE_2,
        STAGE_3
    }

    private Myproject InputActions;
    
    private int Selected;
    private int ItemMax;

    [SerializeField]
    private GameObject fade;                // �t�F�[�h�I�u�W�F�N�g

    [SerializeField]
    [Header("�I�v�V�������j���[")]
    private GameObject OptionMenu;
    private bool OptionMenuFlag = false;

    [Header("�Q�[���}�l�[�W���I�u�W�F�N�g")]
    GameObject ManagerObj;

    [SerializeField]
    [Header("Debug�p�����X�e�[�W�I��")]
    public Stage stage;

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
        ItemMax = transform.childCount - 1;

        OptionMenuFlag = false;
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
                InputActions.Enable();
                OptionMenuFlag = false;
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
                InputActions.Disable();

                ManagerObj.GetComponent<GManager>().ReSetData();

                // �f�o�b�O�p?
                if (stage == Stage.STAGE_1)
                {
                    //SceneManager.LoadScene("Stage1");
                    fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut");
                }
                if(stage == Stage.STAGE_2)
                {
                    SceneManager.LoadScene("Stage2");
                }
                if (stage == Stage.STAGE_3)
                {
                    SceneManager.LoadScene("merge_2");
                }

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
                        SceneManager.LoadScene("Stage1");
                        break;
                    case (1):
                        // �X�e�[�W2
                        InputActions.Disable();
                        SceneManager.LoadScene("Stage2");
                        break;
                    case (2):
                        // �X�e�[�W3
                        InputActions.Disable();
                        SceneManager.LoadScene("merge_2");
                        break;
                }
                SoundManager.instance.PlaySE("Decision");
                break;
            case (2):
                OptionMenu.SetActive(true);
                OptionMenuFlag = true;
                InputActions.Disable();             // �I�v�V�������j���[���J���Ă���Ƃ��͓��͂��󂯕t���Ȃ�
                this.gameObject.SetActive(false);
                SoundManager.instance.PlaySE("Decision");
                break;

        }
    }
}
