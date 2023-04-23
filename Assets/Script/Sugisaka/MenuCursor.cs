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

    private GameObject NewGame_text;        // NewGame�̃e�L�X�g
    private GameObject Continue_text;       // Continue�̃e�L�X�g
    private GameObject Option_text;         // Option�̃e�L�X�g

    [SerializeField]
    private GameObject fade;                // �t�F�[�h�I�u�W�F�N�g

    [SerializeField]
    [Header("�I�v�V�������j���[")]
    private GameObject OptionMenu;
    private bool OptionMenuFlag = false;

    [Header("�Q�[���}�l�[�W���I�u�W�F�N�g")]
    GameObject ManagerObj;

    private const int MAX_MENU = 3;         // ���j���[�̐�

    private int Selected = 0;

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
        ManagerObj = GameObject.Find("GameManager");

        // �e�L�X�g���擾
        NewGame_text = transform.GetChild(0).gameObject;
        Continue_text = transform.GetChild(1).gameObject;
        Option_text = transform.GetChild(2).gameObject;

        OptionMenuFlag = false;
        Selected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Selected = (Selected + MAX_MENU) % MAX_MENU;

        // �I�𒆂̃��j���[�ɂ���ď�����ς���
        switch (Selected)
        {
            case (0):
                NewGame_text.GetComponent<TextMeshProUGUI>().color = Color.white;
                Continue_text.GetComponent<TextMeshProUGUI>().color = Color.black;
                Option_text.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;
            case (1):
                NewGame_text.GetComponent<TextMeshProUGUI>().color = Color.black;
                Continue_text.GetComponent<TextMeshProUGUI>().color = Color.white;
                Option_text.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;
            case (2):
                NewGame_text.GetComponent<TextMeshProUGUI>().color = Color.black;
                Continue_text.GetComponent<TextMeshProUGUI>().color = Color.black;
                Option_text.GetComponent<TextMeshProUGUI>().color = Color.white;
                break;
        }

        
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
        Selected--;
    }

    private void OnDown()
    {
        Selected++;
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
                break;
            case (2):
                OptionMenu.SetActive(true);
                OptionMenuFlag = true;
                InputActions.Disable();             // �I�v�V�������j���[���J���Ă���Ƃ��͓��͂��󂯕t���Ȃ�
                this.gameObject.SetActive(false);
                break;

        }
    }
}
